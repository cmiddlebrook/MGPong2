using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MGPong2;

public class PlayScene : GameScene
{
    private enum GameState
    {
        NewGame,
        NewBall,
        Serve,
        InPlay,
        Paused,
        Win,
    }

    private Rectangle _playArea;
    private Texture2D _board;
    private SoundEffect _winFx;
    private SoundEffect _loseFx;
    private SoundEffect _powerUpFx;
    private Song _playMusic;
    private TextObject _winText;

    private GameState _state = GameState.NewGame;
    private ScoreBar _scoreBar;
    private PlayerPaddle _playerPaddle;
    private AIPaddle _aiPaddle;
    private Ball _ball;
    public int WindowWidth => _playArea.Width;
    public int WindowHeight => _playArea.Height + _scoreBar.Height;

    public PlayScene(SceneManager sm, AssetManager am, InputHelper ih) 
        : base(sm, am, ih)
    {
        _name = "play";
        _clearColour = new Color(0x10, 0x10, 0x10);
    }

    public override void LoadContent()
    {
        _winFx = _am.LoadSoundFx("WinPoint");
        _loseFx = _am.LoadSoundFx("LosePoint");
        _powerUpFx = _am.LoadSoundFx("PowerUp");
        _playMusic = _am.LoadMusic("IcecapMountains");
        _winText = new TextObject(_am.LoadFont("Win"));
        _winText.Colour = new Color(199, 120, 255);
        _winText.Viewport = _playArea;
        _winText.CenterBoth();

        _board = _am.LoadTexture("Board3");
        _scoreBar = new ScoreBar(_am.LoadTexture("ScoreBar"), _am.LoadFont("Score"), _board.Width);
        _playArea = new Rectangle(0, _scoreBar.Height, _board.Width, _board.Height);
        _playerPaddle = new PlayerPaddle(_playArea, _am.LoadTexture("LeftPaddle"));
        _aiPaddle = new AIPaddle(_playArea, _am.LoadTexture("RightPaddle"));
        _ball = new Ball(_playArea, _am.LoadTexture("WhiteBall"), _am.LoadSoundFx("WallHit"), _am.LoadSoundFx("PaddleHit"));
    }

    public override void Enter()
    {
        MediaPlayer.Volume = 0.2f;
        MediaPlayer.Play(_playMusic);
        _state = GameState.NewGame;
    }
    public override void HandleInput(GameTime gt)
    {
        switch (_state)
        {
            case GameState.NewGame:
            case GameState.Serve:
            {
                if (_ih.KeyPressed(Keys.Space))
                {
                    _state = GameState.NewBall;
                }
                break;
            }
            case GameState.InPlay:
            {
                if (_ih.KeyDown(Keys.W))
                {
                    _playerPaddle.MoveUp();
                }
                else if (_ih.KeyDown(Keys.S))
                {
                    _playerPaddle.MoveDown();
                }
                else if (_ih.KeyPressed(Keys.P))
                {
                    _state = GameState.Paused;
                }
                break;
            }
            case GameState.Win:
                if (_ih.KeyPressed(Keys.Space))
                {
                    _state = GameState.NewGame;
                }
                break;
            case GameState.Paused:
                if (_ih.KeyPressed(Keys.P))
                {
                    _state = GameState.InPlay;
                }
                break;
            default:
                {
                    break;
                }
        }

        if (_ih.KeyPressed(Keys.Escape))
        {
            _sm.SwitchScene("title");
        }

    }

    public override void Update(GameTime gt)
    {
        switch (_state)
        {
            case GameState.NewGame:
                {
                    _playerPaddle.Reset();
                    _aiPaddle.Reset();
                    _ball.Reset();
                    _state = GameState.Serve;
                    break;
                }
            case GameState.NewBall:
                {
                    _playerPaddle.NewBall();
                    _aiPaddle.NewBall();
                    _ball.NewBall();
                    _state = GameState.InPlay;
                    break;
                }
            case GameState.InPlay:
                {
                    if (HandleBallPaddleCollisions(_playerPaddle))
                    {
                        _aiPaddle.StartTrackingTimer();
                    }
                    else
                    {
                        HandleBallPaddleCollisions(_aiPaddle);
                        _aiPaddle.TrackBall(_ball.Bounds);
                        _aiPaddle.Update(gt);
                    }

                    CheckPointScored();

                    _playerPaddle.Update(gt);
                    _ball.Update(gt);
                    break;
                }

            case GameState.Serve:
            case GameState.Win:
            case GameState.Paused:
            default:
                break;
        }

        _winText.Update(gt);

        base.Update(gt);
        HandleInput(gt);
    }

    public override void Draw(SpriteBatch sb)
    {
        _scoreBar.Draw(sb, _playerPaddle.Score, _aiPaddle.Score);
        sb.Draw(_board, _playArea, Color.White);
        _playerPaddle.Draw(sb);
        _aiPaddle.Draw(sb);
        _ball.Draw(sb);

        if (_state == GameState.Win)
        {
            _winText.Draw(sb);
        }
    }


    private bool HandleBallPaddleCollisions(Paddle paddle)
    {
        var (colliding, shift) = IsCollision(_ball.Bounds, paddle.Bounds);
        if (colliding)
        {
            _ball.BouncePaddle(shift);
            return true;
        }
        return false;
    }

    private (bool colliding, Vector2 shift) IsCollision(Rectangle a, Rectangle b)
    {
        bool colliding = false;
        Vector2 shift = Vector2.Zero;
        if (a.Intersects(b))
        {
            colliding = true;
            shift.X = (a.Center.X < b.Center.X) ? a.Right - b.Left : a.Left - b.Right;
            shift.Y = (a.Center.Y < b.Center.Y) ? a.Bottom - b.Top : a.Top - b.Bottom;
        }

        return (colliding, shift);
    }

    protected void CheckPointScored()
    {
        Rectangle ballRect = _ball.Bounds;
        if (ballRect.X < 0)
        {
            _aiPaddle.Score++;
            _scoreBar.RightScore = _aiPaddle.Score;
            _loseFx.Play();
            if (_aiPaddle.Score >= 3)
            {
                _winText.Text = "You Lose!";
                _state = GameState.Win;
            }
            else
            {
                _state = GameState.NewBall;
            }
        }
        else if (ballRect.X + ballRect.Width > _playArea.Width)
        {
            _playerPaddle.Score++;
            _scoreBar.LeftScore = _playerPaddle.Score;
            _winFx.Play();
            if (_playerPaddle.Score >= 3)
            {
                _winText.Text = "You Win!";
                _state = GameState.Win;
            }
            else
            {
                _state = GameState.NewBall;
            }
        }

    }
}

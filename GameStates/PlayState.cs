using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace MGPong2;

public class PlayState : GameState
{
    private Rectangle _playArea;
    private Texture2D _board;
    private ScoreBar _scoreBar;
    private PlayerPaddle _playerPaddle;
    private AIPaddle _aiPaddle;
    private Ball _ball;
    public int WindowWidth => _playArea.Width;
    public int WindowHeight => _playArea.Height + _scoreBar.Height;

    public PlayState(StateManager sm, AssetManager am, InputHelper ih) 
        : base(sm, am, ih)
    {
        _name = "play";
        _clearColour = new Color(0x10, 0x10, 0x10);
    }

    public override void LoadContent()
    {
        _board = _am.LoadTexture("Board3");
        _scoreBar = new ScoreBar(_am.LoadTexture("ScoreBar"), _am.LoadFont("Score"), _board.Width);
        _playArea = new Rectangle(0, _scoreBar.Height, _board.Width, _board.Height);
        _playerPaddle = new PlayerPaddle(_playArea, _am.LoadTexture("LeftPaddle"));
        _aiPaddle = new AIPaddle(_playArea, _am.LoadTexture("RightPaddle"));
        _ball = new Ball(_playArea, _am.LoadTexture("WhiteBall"), _am.LoadSoundFx("WallHit"), _am.LoadSoundFx("PaddleHit"));
    }

    public override void Enter()
    {
        StartNewGame();
    }
    public override void HandleInput(GameTime gt)
    {
        if (_ih.KeyPressed(Keys.Escape))
        {
            Debug.WriteLine("PlayState HandleInput: ESCAPE");
            _sm.SwitchState("title");
        }
        else if (_ih.KeyPressed(Keys.Divide))
        {
            _sm.SwitchState("win");
        }
        else if (_ih.KeyDown(Keys.W))
        {
            _playerPaddle.MoveUp();
        }
        else if (_ih.KeyDown(Keys.S))
        {
            _playerPaddle.MoveDown();
        }
    }

    public override void Update(GameTime gt)
    {
        base.Update(gt);
        HandleCollisions();
        _playerPaddle.Update(gt);
        _aiPaddle.Update(gt);
        _ball.Update(gt);
        HandleInput(gt);
    }

    public override void Draw(SpriteBatch sb)
    {
        _scoreBar.Draw(sb, 0, 0);
        sb.Draw(_board, _playArea, Color.White);
        _playerPaddle.Draw(sb);
        _aiPaddle.Draw(sb);
        _ball.Draw(sb);
    }

    private void StartNewGame()
    {
        _playerPaddle.Reset();
        _aiPaddle.Reset();
        _ball.Reset();
    }

    private void HandleCollisions()
    {
        Rectangle ballRect = _ball.Bounds;
        if (ballRect.Intersects(_playerPaddle.Bounds))
        {
            _ball.BouncePaddle();
        }
    }
}

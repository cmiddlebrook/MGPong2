using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace MGPong2;

public class PlayState : GameState
{
    private Rectangle _playArea;
    private Texture2D _board;
    private Vector2 _boardPosition;
    private ScoreBar _scoreBar;
    private Paddle _leftPaddle;
    public int WindowWidth => _playArea.Width;
    public int WindowHeight => _playArea.Height;

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
        _boardPosition = new Vector2(0, _scoreBar.Height);
        _leftPaddle = new Paddle(_playArea, _am.LoadTexture("LeftPaddle"), 4);
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
    }

    public override void Update(GameTime gt)
    {
        base.Update(gt);
        _leftPaddle.Update(gt);
        HandleInput(gt);
    }

    public override void Draw(SpriteBatch sb)
    {
        _scoreBar.Draw(sb, 0, 0);
        sb.Draw(_board, _boardPosition, Color.White);
        _leftPaddle.Draw(sb);
    }
}

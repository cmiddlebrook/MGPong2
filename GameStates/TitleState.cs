
using CALIMOE;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MGPong2;

public class TitleState : GameState
{
    private TextObject _titleText;
    private TextObject _instructions;
    private string _instructionsText;
    private Song _titleMusic;

    public TitleState(StateManager sm, AssetManager am, InputHelper ih) 
        : base(sm, am, ih)
    {
        _name = "title";
        _clearColour = new Color(0x10, 0x10, 0x10);
    }

    public override void LoadContent()
    {
        _titleMusic = _am.LoadMusic("NaturalLife");

        _titleText = new TextObject(_am.LoadFont("Title"));
        _instructions = new TextObject(_am.LoadFont("Instructions"));
        _instructionsText += "You control the left paddle, the AI paddle starts weak,";
        _instructionsText += "\n       but gets stronger with each point scored";
        _instructionsText += "\n\n                      W - Move paddle up";
        _instructionsText += "\n                      S - Move paddle down";
        _instructionsText += "\n                      P - Pause the game";
        _instructionsText += "\n\n                     ESCAPE - Quit to title";
        _instructionsText += "\n                  SPACEBAR - Start new game";
    }

    public override void Enter()
    {
        MediaPlayer.Volume = 0.3f;
        MediaPlayer.Play(_titleMusic);
        base.Enter();
    }

    public override void HandleInput(GameTime gt)
    {
        if (_ih.KeyPressed(Keys.Space))
        {
            Debug.WriteLine("TitleState HandleInput: SPACE");
            _sm.SwitchState("play");
        }
    }

    public override void Update(GameTime gt)
    {
        base.Update(gt);
        HandleInput(gt);
    }

    public override void Draw(SpriteBatch sb)
    {
        _titleText.DrawText(sb, "MG Pong", TextObject.CenterText.Horizontal, 30);
        _instructions.DrawText(sb, _instructionsText, TextObject.CenterText.Horizontal, 100);
    }
}

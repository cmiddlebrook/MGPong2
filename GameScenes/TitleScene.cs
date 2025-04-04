
using CALIMOE;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MGPong2;

public class TitleScene : GameScene
{
    private TextObject _titleText;
    private TextObject _instructions;
    private Song _titleMusic;

    public TitleScene(SceneManager sm, AssetManager am, InputHelper ih) 
        : base(sm, am, ih)
    {
        _name = "title";
        _clearColour = new Color(36, 36, 36);
    }

    public override void LoadContent()
    {
        _titleMusic = _am.LoadMusic("NaturalLife");

        _titleText = new TextObject(_am.LoadFont("Title"), "MG Pong");
        _titleText.CenterHorizontal(30);
        var instructions = string.Empty;
        instructions += "You control the left paddle, the AI paddle starts weak,";
        instructions += "\n       but gets stronger with each point scored";
        instructions += "\n\n                      W - Move paddle up";
        instructions += "\n                      S - Move paddle down";
        instructions += "\n                      P - Pause the game";
        instructions += "\n\n                     ESCAPE - Quit to title";
        instructions += "\n              SPACEBAR - New Game / Serve Ball";
        _instructions = new TextObject(_am.LoadFont("Instructions"), instructions);
        _instructions.CenterHorizontal(100);

    }

    public override void Enter()
    {
        MediaPlayer.Volume = 0.2f;
        MediaPlayer.Play(_titleMusic);
    }

    public override void HandleInput(GameTime gt)
    {
        if (_ih.KeyPressed(Keys.Space))
        {
            Debug.WriteLine("TitleState HandleInput: SPACE");
            _sm.SwitchScene("play");
        }
    }

    public override void Update(GameTime gt)
    {
        _titleText.Update(gt);
        _instructions.Update(gt);

        base.Update(gt);
        HandleInput(gt);
    }

    public override void Draw(SpriteBatch sb)
    {
        _titleText.Draw(sb);
        _instructions.Draw(sb);
    }
}

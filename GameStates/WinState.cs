
using CALIMOE;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MGPong2;

public class WinState : GameState
{
    private TextObject _winText;

    public WinState(StateManager sm, AssetManager am, InputHelper ih) 
        : base(sm, am, ih)
    {
        _name = "win";
    }

    public override void LoadContent()
    {
        _winText = new TextObject(_am.LoadFont("Title"));
    }

    public override void HandleInput(GameTime gt)
    {
        if (_ih.KeyPressed(Keys.Escape))
        {
            Debug.WriteLine("WinState HandleInput: ESCAPE");
            _sm.SwitchState("title");
        }
    }

    public override void Update(GameTime gt)
    {
        base.Update(gt);
        HandleInput(gt);
    }

    public override void Draw(SpriteBatch sb)
    {
        _winText.DrawText(sb, "WINNER!", TextObject.CenterText.Both, 30);
    }
}

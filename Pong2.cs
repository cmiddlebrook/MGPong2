using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CALIMOE;
using System.Diagnostics;

namespace MGPong2;

public class Pong2 : Calimoe
{

    public Pong2()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _showFPS = true;
        _fallbackTextureSize = 64;
    }

    //protected override void Initialize()
    //{
    //    base.Initialize();

    //}

    protected override void LoadContent()
    {
        base.LoadContent(); // ensure the AssetManager gets loaded

        var playState = new PlayScene(_sm, _am, _ih);

        _sm.AddState(playState);
        _sm.AddState(new TitleScene(_sm, _am, _ih));
        _sm.SwitchState("title");

        _graphics.PreferredBackBufferWidth = playState.WindowWidth;
        _graphics.PreferredBackBufferHeight = playState.WindowHeight;
        _graphics.ApplyChanges();

    }

    protected override void Update(GameTime gt)
    {
        _sm.Update(gt);
        base.Update(gt);

        //if (_sm.Current == "title" && _ih.KeyPressed(Keys.Escape))
        //{
        //    Debug.WriteLine("Pong2 Escape detected - Exit triggered");
        //    Exit();
        //}
    }

    protected override void Draw(GameTime gt)
    {
        if (ClearColour != Color.Transparent)
        {
            GraphicsDevice.Clear(ClearColour);
        }

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
        _sm.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gt);

    }
}

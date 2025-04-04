using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CALIMOE;

namespace MGPong2;

public class Pong2 : Calimoe
{

    public Pong2()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _showFPS = false;
        _fallbackTextureSize = 64;
    }


    protected override void LoadContent()
    {
        base.LoadContent(); // ensure the AssetManager gets loaded

        var playScene = new PlayScene(_sm, _am, _ih);

        _sm.AddScene(playScene);
        _sm.AddScene(new TitleScene(_sm, _am, _ih));
        _sm.SwitchScene("title");

        _graphics.PreferredBackBufferWidth = playScene.WindowWidth;
        _graphics.PreferredBackBufferHeight = playScene.WindowHeight;
        _graphics.ApplyChanges();

    }

    protected override void Update(GameTime gt)
    {
        _sm.Update(gt);
        base.Update(gt);
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


using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGPong2;
public class AIPaddle
{
    private SpriteObject _sprite;
    private Rectangle _playArea;
    private Vector2 _startPosition;
    private float _speed = 150f;

    public Rectangle Bounds => _sprite.Bounds;

    public float CenterY => _sprite.Center.Y;

    public AIPaddle(Rectangle playArea, Texture2D texture)
    {
        _playArea = playArea;
        int startY = (playArea.Height - (texture.Height / 2)) / 2;
        _startPosition = new Vector2(playArea.Width - texture.Width - 4, startY);
        _sprite = new SpriteObject(texture, _startPosition, Vector2.Zero, Vector2.One);
    }

    public void Update(GameTime gt)
    {

    }

    public void Draw(SpriteBatch sb)
    {
        _sprite.Draw(sb);
    }

    public void Reset()
    {
        _sprite.Reset();
    }

}

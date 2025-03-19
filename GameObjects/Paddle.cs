using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGPong2;
public class Paddle
{
    private SpriteObject _sprite;
    private Rectangle _playArea;
    private Vector2 _startPosition = Vector2.Zero;

    public Rectangle Bounds => _sprite.Bounds;

    public float CenterY => _sprite.Center.Y;

    public Paddle(Rectangle playArea, Texture2D texture, int xPosition)
    {
        _playArea = playArea;
        int startY = (playArea.Height - (texture.Height / 2)) / 2;
        _startPosition = new Vector2(xPosition, startY);
        _sprite = new SpriteObject(texture, _startPosition, Vector2.Zero, Vector2.One);
    }

    public void Reset()
    {
        _sprite.Reset();
    }

    public void Update(GameTime gt)
    {

    }

    public void Draw(SpriteBatch sb)
    {
        _sprite.Draw(sb);
    }
}

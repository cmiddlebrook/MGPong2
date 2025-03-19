using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MGPong2;
public class PlayerPaddle
{
    private SpriteObject _sprite;
    private Rectangle _playArea;
    private Vector2 _startPosition;
    private float _speed = 200f;

    public Rectangle Bounds => _sprite.Bounds;

    public float CenterY => _sprite.Center.Y;

    public PlayerPaddle(Rectangle playArea, Texture2D texture)
    {
        _playArea = playArea;
        int startY = (playArea.Height - (texture.Height / 2)) / 2;
        _startPosition = new Vector2(4, startY);
        _sprite = new SpriteObject(texture, _startPosition, Vector2.Zero, Vector2.One);
    }


    public void HandleInput(InputHelper ih, GameTime gt)
    {
        if (ih.KeyDown(Keys.W))
        {
            float newY = (float)(_sprite.Position.Y - _speed * gt.ElapsedGameTime.TotalSeconds);
            _sprite.Position = new Vector2(_sprite.Position.X, Math.Max(newY, _playArea.Top));
        }
        else if (ih.KeyDown(Keys.S))
        {
            float newY = (float)(_sprite.Position.Y + _speed * gt.ElapsedGameTime.TotalSeconds);
            _sprite.Position = new Vector2(_sprite.Position.X, Math.Min(newY, _playArea.Bottom - _sprite.Bounds.Height));
        }
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

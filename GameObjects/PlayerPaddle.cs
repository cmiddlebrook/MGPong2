using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MGPong2;
public class PlayerPaddle
{
    private enum PaddleState
    {
        MovingUp,
        MovingDown,
        Stopped
    }

    private SpriteObject _sprite;
    private Rectangle _playArea;
    private Vector2 _startPosition;
    private float _speed = 250f;
    private PaddleState _state = PaddleState.Stopped;


    public Rectangle Bounds => _sprite.Bounds;

    public float CenterY => _sprite.Center.Y;


    public PlayerPaddle(Rectangle playArea, Texture2D texture)
    {
        _playArea = playArea;
        int startY = (playArea.Height - (texture.Height / 2)) / 2;
        _startPosition = new Vector2(4, startY);
        _sprite = new SpriteObject(texture, _startPosition, Vector2.Zero, Vector2.One);
    }

    public void Update(GameTime gt)
    {
        switch (_state)
        {
            case PaddleState.MovingUp:
                {
                    float newY = (float)(_sprite.Position.Y - _speed * gt.ElapsedGameTime.TotalSeconds);
                    _sprite.Position = new Vector2(_sprite.Position.X, Math.Max(newY, _playArea.Top));
                    _state = PaddleState.Stopped;
                    break;
                }

            case PaddleState.MovingDown:
                {
                    float newY = (float)(_sprite.Position.Y + _speed * gt.ElapsedGameTime.TotalSeconds);
                    _sprite.Position = new Vector2(_sprite.Position.X, Math.Min(newY, _playArea.Bottom - _sprite.Bounds.Height));
                    _state = PaddleState.Stopped;
                    break;
                }

            case PaddleState.Stopped:
            default:
                {
                    break;
                }
        }
    }

    public void Draw(SpriteBatch sb)
    {
        _sprite.Draw(sb);
    }

    public void Reset()
    {
        _sprite.Reset();
    }

    public void MoveUp()
    {
        _state = PaddleState.MovingUp;
    }

    public void MoveDown()
    {
        _state = PaddleState.MovingDown;
    }
}

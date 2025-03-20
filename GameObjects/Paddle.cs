using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGPong2;
public abstract class Paddle
{
    protected enum PaddleState
    {
        MovingUp,
        MovingDown,
        Stopped
    }

    protected PaddleState _state = PaddleState.Stopped;
    protected SpriteObject _sprite;
    protected Rectangle _playArea;
    protected Vector2 _startPosition;
    
    protected abstract float Speed { get; set; }

    public int Score { get; set; }

    public Rectangle Bounds => _sprite.Bounds;

    public float CenterY => _sprite.Center.Y;

    public Paddle(Rectangle playArea, Texture2D texture)
    {
        _playArea = playArea;
        int startY = playArea.Top + ((playArea.Height - texture.Height) / 2);
        _startPosition = new Vector2(4, startY);
        _sprite = new SpriteObject(texture, _startPosition, Vector2.Zero, Vector2.One);
    }

    public virtual void Update(GameTime gt)
    {
        switch (_state)
        {
            case PaddleState.MovingUp:
                {
                    float newY = (float)(_sprite.Position.Y - Speed * gt.ElapsedGameTime.TotalSeconds);
                    _sprite.Position = new Vector2(_sprite.Position.X, Math.Max(newY, _playArea.Top));
                    _state = PaddleState.Stopped;
                    break;
                }

            case PaddleState.MovingDown:
                {
                    float newY = (float)(_sprite.Position.Y + Speed * gt.ElapsedGameTime.TotalSeconds);
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

    public virtual void Draw(SpriteBatch sb)
    {
        _sprite.Draw(sb);
    }

    public virtual void Reset()
    {
        Score = 0;
        NewBall();
    }

    public virtual void NewBall()
    {
        _sprite.Reset();
        _state = PaddleState.Stopped;
    }

    public virtual void MoveUp()
    {
        _state = PaddleState.MovingUp;
    }

    public virtual void MoveDown()
    {
        _state = PaddleState.MovingDown;
    }
}


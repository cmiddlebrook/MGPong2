using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGPong2;
public class Ball
{
    private enum BallState
    {
        Stop,
        Move,
        BouncePaddle,
        BounceWall,
    }

    private SpriteObject _sprite;
    private Rectangle _playArea;
    private Vector2 _shift;
    private int _size;
    private float _speed = 350f;
    private BallState _state = BallState.Stop;
    protected SoundEffect _wallHitFx;
    protected SoundEffect _paddleHitFx;
    protected readonly Random _rand = new Random();


    public Rectangle Bounds => _sprite.Bounds;

    public float CenterY => _sprite.Center.Y;


    public Ball(Rectangle playArea, Texture2D texture, SoundEffect wallHit, SoundEffect paddleHit)
    {
        _playArea = playArea;
        _size = texture.Width;
        _sprite = new SpriteObject(texture, GetStartPosition(), GetStartVelocity(), Vector2.One);
        _wallHitFx = wallHit;
        _paddleHitFx = paddleHit;
    }

    public void Update(GameTime gt)
    {
        if (_sprite.Position.Y < _playArea.Top || _sprite.Position.Y > _playArea.Bottom - _size)
        {
            _state = BallState.BounceWall;
        }

        switch (_state)
        {
            case BallState.BouncePaddle:
            {
                _sprite.Position -= _shift;
                _sprite.ReverseXDirection();
                _paddleHitFx.Play();
                _state = BallState.Move;
                break;
            }

            case BallState.BounceWall:
            {
                _sprite.ReverseYDirection();
                _wallHitFx.Play();
                _state = BallState.Move;
                break;
            }
        }

        if (_state == BallState.Move)
        {
            _sprite.Update(gt);
        }
    }

    public void Draw(SpriteBatch sb)
    {
        _sprite.Draw(sb);
    }

    public void Reset()
    {
        _state = BallState.Move;
        _sprite.Reset();
        _sprite.Position = GetStartPosition();
        _sprite.Velocity = GetStartVelocity();
    }

    public void BouncePaddle(Vector2 shift)
    {
        _shift = shift;
        _shift.Y = 0;
        _state = BallState.BouncePaddle;
    }

    protected Vector2 GetStartPosition()
    {
        int startX = (_playArea.Width - _size) / 2;
        int startY = _playArea.Top + ((_playArea.Height - _size) / 2);
        return new Vector2(startX, startY);
    }
    protected Vector2 GetStartVelocity()
    {
        Vector2 randomVelocity = new Vector2(_rand.Next(2) == 0 ? -100f : 100f, (_rand.Next(5, 60)));
        randomVelocity.Y *= _rand.Next(2) == 0 ? 1 : -1;
        //Vector2 randomVelocity = new Vector2(100f, 3f);
        randomVelocity.Normalize();
        randomVelocity *= _speed;
        //randomVelocity *= 100f;

        return randomVelocity;
    }
}


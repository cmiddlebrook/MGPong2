
using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGPong2;
public class AIPaddle : Paddle
{
    protected override float Speed { get; set; } = 10f;

    private float _trackingDelay = 0.7f;
    private TimeSpan _trackingTimer;
    public AIPaddle(Rectangle playArea, Texture2D texture)
        : base(playArea, texture)
    {
        _playArea = playArea;
        int startY = playArea.Top + ((playArea.Height - texture.Height) / 2);
        _startPosition = new Vector2(playArea.Width - texture.Width - 4, startY);
        _sprite = new SpriteObject(texture, _startPosition, Vector2.Zero, Vector2.One);
    }

    public override void Update(GameTime gt)
    {
        _trackingTimer += gt.ElapsedGameTime;
        base.Update(gt);
    }

    public override void Reset()
    {
        base.Reset();
        _trackingTimer = TimeSpan.Zero;
    }

    public void TrackBall(Rectangle ballBounds)
    {
        if (_trackingTimer.TotalSeconds >= _trackingDelay)
        {
            Point ballPos = ballBounds.Center;
            Point paddlePos = _sprite.Bounds.Center;
            int distance = Math.Abs(ballPos.Y - paddlePos.Y);
            if (distance > 4)
            {
                if (ballPos.Y < paddlePos.Y)
                {
                    _state = PaddleState.MovingUp;
                }
                else if (ballPos.Y > paddlePos.Y)
                {
                    _state = PaddleState.MovingDown;
                }
            }
        }
    }

    public void StartTrackingTimer()
    {
        _trackingTimer = TimeSpan.Zero;
    }



}

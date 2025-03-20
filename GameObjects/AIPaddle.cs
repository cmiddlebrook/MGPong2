
using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGPong2;
public class AIPaddle : Paddle
{
    public AIPaddle(Rectangle playArea, Texture2D texture)
        : base(playArea, texture)
    {
        _playArea = playArea;
        int startY = playArea.Top + ((playArea.Height - texture.Height) / 2);
        _startPosition = new Vector2(playArea.Width - texture.Width - 4, startY);
        _sprite = new SpriteObject(texture, _startPosition, Vector2.Zero, Vector2.One);
    }

    public void TrackBall(Rectangle ballBounds)
    {
        Point ballPos = ballBounds.Center;
        Point paddlePos = _sprite.Bounds.Center;
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

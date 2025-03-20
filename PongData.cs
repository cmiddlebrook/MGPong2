
using System.Numerics;

namespace MGPong2;

public class PongData
{
    private PlayState _controller;

    private Vector2 _ballPosition;
    public Vector2 BallPosition
    {
        get => _ballPosition;
        set
        {
            
        }
    }

    public PongData(PlayState controller)
    {
        _controller = controller;
    }
}

using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGPong2;
public class PlayerPaddle : Paddle
{
    protected override float Speed { get; set; } = 300f;
    public PlayerPaddle(Rectangle playArea, Texture2D texture)
        : base(playArea, texture)
    {
    }

}

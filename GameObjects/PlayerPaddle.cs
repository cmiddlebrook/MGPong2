using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGPong2;
public class PlayerPaddle : Paddle
{
    public PlayerPaddle(Rectangle playArea, Texture2D texture)
        : base(playArea, texture)
    {
    }

}

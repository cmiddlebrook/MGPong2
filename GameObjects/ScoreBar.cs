using CALIMOE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGPong2;

class ScoreBar
{
    private TextObject _leftScore;
    private SpriteObject _leftSprite;
    private TextObject _rightScore;
    private SpriteObject _rightSprite;

    public int LeftScore
    {
        set { _leftScore.Text = value.ToString(); }
    }

    public int RightScore
    {
        set { _rightScore.Text = value.ToString(); }
    }

    public int Height => _leftSprite.Bounds.Height;

    public ScoreBar(Texture2D texture, SpriteFont font, int boardWidth)
    {
        _leftScore = new TextObject(font);
        _leftScore.Position = new Vector2((boardWidth / 2) - 45, 0);
        _rightScore = new TextObject(font);
        _rightScore.Position = new Vector2((boardWidth / 2) + 40, 0);
        _leftSprite = new SpriteObject(texture, Vector2.Zero, Vector2.Zero, 1.0f);
        _rightSprite = new SpriteObject(texture, new Vector2(boardWidth - texture.Width, 0), Vector2.Zero, 1.0f);
    }

    public void Draw(SpriteBatch sb, int leftScore, int rightScore)
    {
        _leftSprite.Draw(sb);
        _rightSprite.DrawFlippedHorizontally(sb);
        _leftScore.Draw(sb);
        _rightScore.Draw(sb);
    }
}

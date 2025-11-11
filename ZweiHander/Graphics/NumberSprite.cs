using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Graphics;

public class NumberSprite : AbstractSprite
{
    private string _number;
    private Dictionary<char, ISprite> _sprites;
    private List<Vector2> _relativePositions;
    public int CumWidth;
    public int CumHeight { get => _sprites['0'].Height; }
    public NumberSprite(int number, SpriteBatch spriteBatch, HUDSprites hudSprites, bool centered = true)
    {
        _spriteBatch = spriteBatch;
        _number = number.ToString();
        _sprites = [];
        for (int i = 0; i < 10; i++)
        {
            _sprites[i.ToString()[0]] = hudSprites.Digit(i);
        }
        foreach(char c in _number)
        {
            CumWidth += _sprites[c].Width;
        }
        int startWidth = centered ? (_sprites['0'].Width - CumWidth) : 0;
        _relativePositions = [];
        foreach (char c in _number)
        {
            _relativePositions.Add(new(startWidth, 0));
            startWidth += _sprites['0'].Width * 2;
        }
    }

    public override void Draw(Vector2 position)
    {
        for (int i = 0; i < _number.Length; i++)
        {
            _sprites[_number[i]].Draw(position + _relativePositions[i]);
        }
    }
}
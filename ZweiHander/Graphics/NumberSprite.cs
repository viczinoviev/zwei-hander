using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Graphics;

public class NumberSprite : AbstractSprite
{
    private string _number;
    private readonly Dictionary<char, ISprite> _sprites;
    private List<Vector2> _relativePositions;
    public int CumWidth;
    public int CumHeight { get => _sprites['0'].Height; }
    private readonly int Digits;
    public NumberSprite(int number, HUDSprites hudSprites, int digits = -1, bool centered = true)
    {
        _sprites = [];
        Digits = digits;
        for (int i = 0; i < 10; i++)
        {
            _sprites[i.ToString()[0]] = hudSprites.Digit(i);
        }
        SetNumber(number, centered);
    }

    public void SetNumber(int number, bool centered = true)
    {
        _number = number.ToString();
        if (_number.Length < Digits)
        {
            _number = _number.PadLeft(Digits, '0');
        }
        CumWidth = 0;
        foreach (char c in _number)
        {
            CumWidth += _sprites[c].Width;
        }
        int startWidth = centered ? (_sprites['0'].Width - CumWidth) / 2 : 0;
        _relativePositions = [];
        foreach (char c in _number)
        {
            _relativePositions.Add(new(startWidth, 0));
            startWidth += _sprites['0'].Width;
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
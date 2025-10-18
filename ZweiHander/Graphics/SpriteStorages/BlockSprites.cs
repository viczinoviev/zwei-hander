using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;
public class BlockSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/BlockDefinition.xml";
    readonly SpriteBatch _spriteBatch;
    public BlockSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }
    public ISprite SolidCyanTile() => new IdleSprite(_regions["solid-cyan"], _spriteBatch);
    public ISprite BlockTile() => new IdleSprite(_regions["block-tile"], _spriteBatch);
    public ISprite StatueTile1() => new IdleSprite(_regions["statue-tile-1"], _spriteBatch);
    public ISprite StatueTile2() => new IdleSprite(_regions["statue-tile-2"], _spriteBatch);
    public ISprite SolidBlackTile() => new IdleSprite(_regions["solid-black"], _spriteBatch);
    public ISprite TexturedTile() => new IdleSprite(_regions["textured-tile"], _spriteBatch);
    public ISprite StairTile() => new IdleSprite(_regions["stair-tile"], _spriteBatch);
    public ISprite BrickTile() => new IdleSprite(_regions["brick-tile"], _spriteBatch);
    public ISprite WhitePatternTile() => new IdleSprite(_regions["white-pattern-tile"], _spriteBatch);

    public ISprite WallNorthLeft() => new IdleSprite(_regions["wall-north-left"], _spriteBatch);
    public ISprite WallNorthRight() => new IdleSprite(_regions["wall-north-right"], _spriteBatch);
    public ISprite WallSouthLeft() => new IdleSprite(_regions["wall-south-left"], _spriteBatch);
    public ISprite WallSouthRight() => new IdleSprite(_regions["wall-south-right"], _spriteBatch);
    public ISprite WallWestTop() => new IdleSprite(_regions["wall-west-top"], _spriteBatch);
    public ISprite WallWestBottom() => new IdleSprite(_regions["wall-west-bottom"], _spriteBatch);
    public ISprite WallEastTop() => new IdleSprite(_regions["wall-east-top"], _spriteBatch);
    public ISprite WallEastBottom() => new IdleSprite(_regions["wall-east-bottom"], _spriteBatch);

}



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

    // Wall tile (center) variations
    public ISprite WallTileNorth() => new IdleSprite(_regions["wall-tile-north"], _spriteBatch);
    public ISprite WallTileWest() => new IdleSprite(_regions["wall-tile-west"], _spriteBatch);
    public ISprite WallTileEast() => new IdleSprite(_regions["wall-tile-east"], _spriteBatch);
    public ISprite WallTileSouth() => new IdleSprite(_regions["wall-tile-south"], _spriteBatch);

    // Entrance tiles (note: XML key is "enterance-*")
    public ISprite EntranceTileNorth() => new IdleSprite(_regions["enterance-tile-north"], _spriteBatch);
    public ISprite EntranceTileWest() => new IdleSprite(_regions["enterance-tile-west"], _spriteBatch);
    public ISprite EntranceTileEast() => new IdleSprite(_regions["enterance-tile-east"], _spriteBatch);
    public ISprite EntranceTileSouth() => new IdleSprite(_regions["enterance-tile-south"], _spriteBatch);

    // Locked door tiles
    public ISprite LockedDoorTileNorth() => new IdleSprite(_regions["locked-door-tile-north"], _spriteBatch);
    public ISprite LockedDoorTileWest() => new IdleSprite(_regions["locked-door-tile-west"], _spriteBatch);
    public ISprite LockedDoorTileEast() => new IdleSprite(_regions["locked-door-tile-east"], _spriteBatch);
    public ISprite LockedDoorTileSouth() => new IdleSprite(_regions["locked-door-tile-south"], _spriteBatch);

    // Door tiles
    public ISprite DoorTileNorth() => new IdleSprite(_regions["door-tile-north"], _spriteBatch);
    public ISprite DoorTileWest() => new IdleSprite(_regions["door-tile-west"], _spriteBatch);
    public ISprite DoorTileEast() => new IdleSprite(_regions["door-tile-east"], _spriteBatch);
    public ISprite DoorTileSouth() => new IdleSprite(_regions["door-tile-south"], _spriteBatch);

    // Hole-in-wall tiles
    public ISprite HoleInWallNorth() => new IdleSprite(_regions["hole-in-wall-north"], _spriteBatch);
    public ISprite HoleInWallWest() => new IdleSprite(_regions["hole-in-wall-west"], _spriteBatch);
    public ISprite HoleInWallEast() => new IdleSprite(_regions["hole-in-wall-east"], _spriteBatch);
    public ISprite HoleInWallSouth() => new IdleSprite(_regions["hole-in-wall-south"], _spriteBatch);

    // Inside corner tiles
    public ISprite InsideCornerNortheast() => new IdleSprite(_regions["inside-corner-tile-northeast"], _spriteBatch);
    public ISprite InsideCornerSoutheast() => new IdleSprite(_regions["inside-corner-tile-southeast"], _spriteBatch);
    public ISprite InsideCornerSouthwest() => new IdleSprite(_regions["inside-corner-tile-southwest"], _spriteBatch);
    public ISprite InsideCornerNorthwest() => new IdleSprite(_regions["inside-corner-tile-northwest"], _spriteBatch);

    // Outside corner tiles
    public ISprite OutsideCornerSouthwest() => new IdleSprite(_regions["outside-corner-tile-southwest"], _spriteBatch);
    public ISprite OutsideCornerNorthwest() => new IdleSprite(_regions["outside-corner-tile-northwest"], _spriteBatch);
    public ISprite OutsideCornerNortheast() => new IdleSprite(_regions["outside-corner-tile-northeast"], _spriteBatch);
    public ISprite OutsideCornerSoutheast() => new IdleSprite(_regions["outside-corner-tile-southeast"], _spriteBatch);
}


using System;
using System.Collections.Generic;
using ZweiHander.Items;

namespace ZweiHander.Environment
{
	public static class AreaDictionaries
	{
		public static readonly Dictionary<int, BlockName> idToBlockName = new()
		{
			{ 0, BlockName.SolidCyanTile },
			{ 1, BlockName.SolidBlackTile },
			{ 2, BlockName.BlockTile },
			{ 3, BlockName.BrickTile },
			{ 4, BlockName.StatueTile1 },
			{ 5, BlockName.StatueTile2 },
			{ 6, BlockName.TexturedTile },
			{ 7, BlockName.StairTile },
			{ 8, BlockName.WhitePatternTile },
			{ 9, BlockName.FireTile },
			{ 10, BlockName.LadderTile }
		};

		// Useless, but keeping for consistency and futureproofing
		public static readonly Dictionary<String, String> enemyNameToEnemyName = new()
		{
			{ "Darknut", "Darknut" },
			{ "Gel", "Gel" },
			{ "Goriya", "Goriya" },
			{ "Keese", "Keese" },
			{ "Stalfos", "Stalfos" },
			{ "Rope", "Rope" },
			{ "Wallmaster", "Wallmaster" },
			{ "Zol", "Zol" },
			{ "Dodongo", "Dodongo" },
			{ "Aquamentus", "Aquamentus" },
			{ "BladeTrap", "BladeTrap" },
			{ "OldMan", "OldMan" },
			{ "MovingBlock", "MovingBlock" }
		};

		public static readonly Dictionary<String, ItemType> itemNameToItemType = new()
		{
			{ "Heart", ItemType.Heart },
			{ "Bomb", ItemType.Bomb },
			{ "Arrow", ItemType.Arrow },
			{ "Key", ItemType.Key },
			{ "Compass", ItemType.Compass },
			{ "Map", ItemType.Map },
			{ "HeartContainer", ItemType.HeartContainer },
			{ "TriforcePiece", ItemType.TriforcePiece },
			{ "Boomerang", ItemType.Boomerang },
			{ "Bow", ItemType.Bow },
			{ "Clock", ItemType.Clock },
			{ "Fairy", ItemType.Fairy }
		};
	}
}
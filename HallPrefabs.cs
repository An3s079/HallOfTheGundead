using Dungeonator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HallOfGundead
{
	class HallPrefabs
	{
		public static AssetBundle shared_auto_002;
		public static AssetBundle shared_auto_001;
		public static AssetBundle HallAssets;
		public static AssetBundle braveResources;

		private static Dungeon TutorialDungeonPrefab;
		private static Dungeon SewerDungeonPrefab;
		private static Dungeon MinesDungeonPrefab;
		private static Dungeon ratDungeon;
		private static Dungeon CathedralDungeonPrefab;
		private static Dungeon BulletHellDungeonPrefab;
		private static Dungeon ForgeDungeonPrefab;
		private static Dungeon CatacombsDungeonPrefab;
		private static Dungeon NakatomiDungeonPrefab;

		public static Texture2D ENV_Tileset_Hall_Texture;

		public static GameObject Hall_BigPomp;
		public static GameObject BigPomp_Icon;

		public static PrototypeDungeonRoom reward_room;
		public static PrototypeDungeonRoom gungeon_rewardroom_1;
		public static PrototypeDungeonRoom shop02;
		public static PrototypeDungeonRoom doublebeholsterroom01;

		public static GenericRoomTable shop_room_table;
		public static GenericRoomTable boss_foyertable;
		public static GenericRoomTable HallRoomTable;
		public static GenericRoomTable SecretRoomTable;

		public static GenericRoomTable CastleRoomTable;
		public static GenericRoomTable Gungeon_RoomTable;
		public static GenericRoomTable SewersRoomTable;
		public static GenericRoomTable AbbeyRoomTable;
		public static GenericRoomTable MinesRoomTable;
		public static GenericRoomTable CatacombsRoomTable;
		public static GenericRoomTable ForgeRoomTable;
		public static GenericRoomTable BulletHellRoomTable;

		public static void InitCustomPrefabs()
		{
			HallAssets = AssetBundleLoader.LoadAssetBundleFromLiterallyAnywhere("hallassets");
			AssetBundle assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
			AssetBundle assetBundle2 = ResourceManager.LoadAssetBundle("shared_auto_002");
			shared_auto_001 = assetBundle;
			shared_auto_002 = assetBundle2;
			braveResources = ResourceManager.LoadAssetBundle("brave_resources_001");
			if(HallAssets == null)
			{
				ETGModConsole.Log("HallAssets is null!");
			}
			TutorialDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Tutorial");
			SewerDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Sewer");
			MinesDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Mines");
			ratDungeon = DungeonDatabase.GetOrLoadByName("base_resourcefulrat");
			CathedralDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Cathedral");
			BulletHellDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_BulletHell");
			ForgeDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Forge");
			CatacombsDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Catacombs");
			NakatomiDungeonPrefab = DungeonDatabase.GetOrLoadByName("base_nakatomi");


			ENV_Tileset_Hall_Texture = HallAssets.LoadAsset<Texture2D>("ENV_Tileset_Hall");
			if (ENV_Tileset_Hall_Texture is null)
			{
				ETGModConsole.Log("ENV_Tileset_Hall_Texture is null!");
			}
			Hall_BigPomp = HallAssets.LoadAsset<GameObject>("Hall_BigPomp");
			if (Hall_BigPomp is null)
			{
				ETGModConsole.Log("Hall_BigPomp is null!");
			}
			var frames = new List<string> { "HallOfGundead/Resources/HallEntrance_001", "HallOfGundead/Resources/HallEntrance_002", "HallOfGundead/Resources/HallEntrance_003", "HallOfGundead/Resources/HallEntrance_004", "HallOfGundead/Resources/HallEntrance_005", "HallOfGundead/Resources/HallEntrance_006" };
			GameObject prefab = ItemAPI.ItemBuilder.AddSpriteToObject("Hall_BigPomp", "HallOfGundead/Resources/HallEntrance_001", Hall_BigPomp, false, false);
			UnityEngine.Object.DontDestroyOnLoad(prefab);
			tk2dSpriteAnimator animator = prefab.AddComponent<tk2dSpriteAnimator>();
			tk2dSpriteAnimationClip animationClip = new tk2dSpriteAnimationClip();
			animationClip.fps = 12;
			animationClip.wrapMode = tk2dSpriteAnimationClip.WrapMode.Loop;
			animationClip.name = "start";

			GameObject spriteObject = new GameObject("spriteObject");
			ItemAPI.ItemBuilder.AddSpriteToObject("spriteObject", $"HallOfGundead/Resources/HallEntrance_001", spriteObject);
			tk2dSpriteAnimationFrame starterFrame = new tk2dSpriteAnimationFrame();
			starterFrame.spriteId = spriteObject.GetComponent<tk2dSprite>().spriteId;
			starterFrame.spriteCollection = spriteObject.GetComponent<tk2dSprite>().Collection;
			tk2dSpriteAnimationFrame[] frameArray = new tk2dSpriteAnimationFrame[]
			{
				starterFrame
			};
			animationClip.frames = frameArray;
			for (int i = 2; i < 6; i++)
			{
				GameObject spriteForObject = new GameObject("spriteForObject");
				ItemAPI.ItemBuilder.AddSpriteToObject("spriteForObject", $"HallOfGundead/Resources/HallEntrance_00{i}", spriteForObject);
				tk2dSpriteAnimationFrame frame = new tk2dSpriteAnimationFrame();
				frame.spriteId = spriteForObject.GetComponent<tk2dBaseSprite>().spriteId;
				frame.spriteCollection = spriteForObject.GetComponent<tk2dBaseSprite>().Collection;
				animationClip.frames = animationClip.frames.Concat(new tk2dSpriteAnimationFrame[] { frame }).ToArray();
			}
			animator.Library = animator.gameObject.AddComponent<tk2dSpriteAnimation>();
			animator.Library.clips = new tk2dSpriteAnimationClip[] { animationClip };
			animator.DefaultClipId = animator.GetClipIdByName("start");
			animator.playAutomatically = true;
			//Hall_BigPomp.GetComponent<tk2dSprite>().
			
			//Hall_BigPompShadow = HallAssets.LoadAsset<GameObject>("Hall_BigPompShadow");
			//ItemAPI.ItemBuilder.AddSpriteToObject(Hall_BigPompShadow, "HallOfGundead/Resources/HallEntrance_shadow", false, false);
			
			BigPomp_Icon = HallAssets.LoadAsset<GameObject>("BigPomp_Icon");
			if (BigPomp_Icon is null)
			{
				ETGModConsole.Log("BigPomp_Icon is null!");
			}
			var icon = HallAssets.LoadAsset<Texture2D>("BigPomp_Icon");
			if (icon is null)
			{
				ETGModConsole.Log("icon is null!");
			}
			ItemAPI.ItemBuilder.AddSpriteToObject(BigPomp_Icon, icon, false, false);

			Hall_BigPomp.AddComponent<BigPompEntranceController>();

			reward_room = shared_auto_002.LoadAsset<PrototypeDungeonRoom>("reward room");
			gungeon_rewardroom_1 = shared_auto_002.LoadAsset<PrototypeDungeonRoom>("gungeon_rewardroom_1");
			shop_room_table = shared_auto_002.LoadAsset<GenericRoomTable>("Shop Room Table");
			shop02 = shared_auto_002.LoadAsset<PrototypeDungeonRoom>("shop02");
			boss_foyertable = shared_auto_002.LoadAsset<GenericRoomTable>("Boss Foyers");

			HallRoomTable = ScriptableObject.CreateInstance<GenericRoomTable>();
			HallRoomTable.includedRooms = new WeightedRoomCollection();
			HallRoomTable.includedRooms.elements = new List<WeightedRoom>();
			HallRoomTable.includedRoomTables = new List<GenericRoomTable>(0);

			SecretRoomTable = shared_auto_002.LoadAsset<GenericRoomTable>("secret_room_table_01");

			CastleRoomTable = shared_auto_002.LoadAsset<GenericRoomTable>("Castle_RoomTable");
			Gungeon_RoomTable = shared_auto_002.LoadAsset<GenericRoomTable>("Gungeon_RoomTable");
			SewersRoomTable = SewerDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable;
			AbbeyRoomTable = CathedralDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable;
			MinesRoomTable = MinesDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable;
			CatacombsRoomTable = CatacombsDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable;
			ForgeRoomTable = ForgeDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable;
			BulletHellRoomTable = BulletHellDungeonPrefab.PatternSettings.flows[0].fallbackRoomTable;

			doublebeholsterroom01 = HallDungeonFlows.LoadOfficialFlow("Secret_DoubleBeholster_Flow").AllNodes[2].overrideExactRoom;

		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;
using ItemAPI;
using HallOfGundead.Weapons;
using EnemyBulletBuilder;
using System.Reflection;
using ChestAPI;
using System.Collections;
using InControl;
using ChallengeAPI;
using CursorAPI;
using Gungeon;
using SGUI;
using MonoMod.RuntimeDetour;
using UnityEngine.SceneManagement;
using EnemyAPI;
using System.IO;

namespace HallOfGundead
{

    public class FLoorModModule : ETGModule
    {
        public static string ZipFilePath;
        public static string FilePath;
        public static ETGModuleMetadata metadata = new ETGModuleMetadata();


        public static AdvancedStringDB Strings;
        List<int> ItemIds = new List<int>();
        public static List<ItemAndWeight> itemandWeight = new List<ItemAndWeight>();

        public static readonly string MOD_NAME = "Hall of The Gundead";
        public static readonly string VERSION = "0.0.0";
        public static readonly string TEXT_COLOR = "#00FFFF";
        public override void Start()
        {
            try
            {
                ETGModConsole.Log(Application.version);
                ETGMod.AIActor.OnPreStart += this.AddYoloComponent;
                //ETGModMainBehaviour.Instance.gameObject.AddComponent<CHeckPos>();
                /*
                 setup
                */
                // AdvancedLogging.Init();
                metadata = this.Metadata;
                ZipFilePath = this.Metadata.Archive;
                FilePath = this.Metadata.Directory + "";
                EnemyTools.Init();
                AudioResourceLoader.InitAudio();

                ItemAPI.Tools.Init();

                BossBuilder.Init();
                BulletBuilder.Init();
                EnemyBuilder.Init();
                FakePrefabHooks.Init();
                ItemBuilder.Init();
                ChallengeBuilder.Init();
                
                CursorMaker.Init();
                ChallengeBuilder.BuildChallenge<DizzyChallenge>("HallOfGundead/Resources/DizzyChallengeFrame.png", "Dizzy", true, null, null, null, true, true);
                CursorMaker.BuildCursor("HallOfGundead/Resources/gtcktpCursor_Cursor.png");
                CursorMaker.BuildCursor("HallOfGundead/Resources/Skull_Cursor.png");
                CursorMaker.BuildCursor("HallOfGundead/Resources/Pomp_Cursor.png");
                Strings = new AdvancedStringDB();
                //GungeonAPI.GungeonAP.Init();

                /*
                 Enemies
                */
                //Pumpking.Init();

                PumpKin.Init();
                succubus.Init();
                Trankenstine.Init();
                Witch_kin.Init();
                VampireMantis.Init();
                /*
                Items
               */
                NonBossSoul.Init();
                BossSoul.Init();
                ItemIds.Add(BloodiedChamber.Init());

                /*
                Weapons
               */
                ItemIds.Add(SoulEater.Add());
                SoulEaterEvolution.Add();
                spapiGun.Add();

                foreach (int i in ItemIds)
                {
                    itemandWeight.Add(new ItemAndWeight
                    {

                        itemID = i,
                        itemWeight = 1

                    }

                    );
                    //Log("added" + i);
                }

                

                HalloweenChest.Init(); //has to be at bottom
                var Go = new GameObject();
                ETGModConsole.Commands.AddGroup("hall");
                ETGModConsole.Commands.GetGroup("hall").AddUnit("load", new Action<string[]>(this.LoadHall));
                ETGModConsole.Commands.GetGroup("hall").AddUnit("chest", new Action<string[]>(this.SpawnChest));

                //init floor
                HallPrefabs.InitCustomPrefabs();
                HallRoomPrefabs.InitCustomRooms();
                HallDungeonFlows.InitDungeonFlows();
                HallDungeon.InitCustomDungeon();

                Hook hook = new Hook(
                   typeof(GameManager).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance),
                   typeof(FLoorModModule).GetMethod("GameManager_Awake", BindingFlags.NonPublic | BindingFlags.Instance),
                   typeof(GameManager)
               );
                AdvancedLogging.Log($"{MOD_NAME} v{VERSION} started successfully.", new Color32(23, 235, 196, 255), false, true);
                
            }
            catch (Exception e)
            {
                AdvancedLogging.LogPlain(e, Color.red);
            }
        }

        private void GameManager_Awake(Action<GameManager> orig, GameManager self)
        {
            orig(self);
            HallDungeon.InitCustomDungeon();
        }

        private void AddYoloComponent(AIActor obj)
		{
            var y = obj.gameObject.AddComponent<YoloComponent>();

            y.actor = obj;
		}
       
        private void SpawnChest(string[] obj)
        {
            var c = Chest.Spawn(HalloweenChest.PompChest, GameManager.Instance.PrimaryPlayer.CurrentRoom.GetBestRewardLocation(IntVector2.One * 3));
            //Log("spritePos" + c.sprite.transform.position.ToString(), "\n SpecEigidBody pos" + c.specRigidbody.transform.position.ToString());
        }
        private void LoadHall(string[] arg)
        {
            try
            {
                GameManager.Instance.LoadCustomLevel("tt_hall");
            }
            catch (Exception e)
            {
                ETGModConsole.Log($"{e}");
            }
        }

        public static void Log(string text, string color = "#FFFFFF")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }
        public override void Exit() { }
        public override void Init() { }
        public static List<GameUIAmmoType> addedAmmoTypes = new List<GameUIAmmoType>();


		
	}

    class YoloComponent : MonoBehaviour
	{
       public AIActor actor;
        bool TextShown = false;
        void Update()
		{
            if(actor.IsOverPit == true && actor.IsFlying == false && TextShown == false)
			{
                if (UnityEngine.Random.value <= 0.1)
                {
                    TextBoxManager.ShowTextBox(actor.transform.position + new Vector3(0, 3), actor.transform, 7, "YOlO!", string.Empty, false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, false, false);
                    TextShown = true;
                }
            }
        }
	}


}

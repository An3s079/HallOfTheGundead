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
using EnemyAPI;
using System.IO;
using BepInEx;
using BindingAPI;

namespace HallOfGundead
{
    [BepInDependency("etgmodding.etg.mtgapi")]
    [BepInPlugin("an3s.etg.hotg", MOD_NAME, VERSION)]
    public class FloorModModule : BaseUnityPlugin
    {
        public static FloorModModule Instance;
        public static AdvancedStringDB Strings;
        List<int> ItemIds = new List<int>();
        public static List<ItemAndWeight> itemandWeight = new List<ItemAndWeight>();

        public const string MOD_NAME = "Hall of The Gundead";
        public const string VERSION = "1.0.0";
        public static readonly string TEXT_COLOR = "#00FFFF";
        public void Start()
        {
            Instance = this;
            ETGModMainBehaviour.WaitForGameManagerStart(postStart);               
        }

        private void postStart(GameManager obj)
        {
            try
            {
                ETGMod.AIActor.OnPreStart += this.AddYoloComponent;
                //ETGModMainBehaviour.Instance.gameObject.AddComponent<CHeckPos>();
                /*
                 setup
                */
                // AdvancedLogging.Init();
                EnemyTools.Init();
                AudioResourceLoader.InitAudio();

                ItemAPI.Tools.Init();
                EnemyTools.Init();
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
                BloodiedChamber.Init();
                //IncubiChiarm.Init();
                /*
                Weapons
               */
                SoulEater.Add();
                SoulEaterEvolution.Add();
                spapiGun.Add();

                //foreach (int i in ItemIds)
                //{
                //    itemandWeight.Add(new ItemAndWeight
                //    {

                //        itemID = i,
                //        itemWeight = 1

                //    }

                //    );
                //    //Log("added" + i);
                //}



                //HalloweenChest.Init(); //has to be at bottom
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
                   typeof(FloorModModule).GetMethod("GameManager_Awake", BindingFlags.NonPublic | BindingFlags.Instance),
                   typeof(GameManager)
               );
               ETGModConsole.Log($"{MOD_NAME} v{VERSION} started successfully.").Colors[0] = new Color32(23, 235, 196, 255);
            }
            catch (Exception e)
            {
                ETGModConsole.Log(e).Colors[0] = Color.red;
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

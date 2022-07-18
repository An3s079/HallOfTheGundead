using System;
using System.Collections.Generic;
using Gungeon;
using ItemAPI;
using UnityEngine;
using DirectionType = DirectionalAnimation.DirectionType;
using AnimationType = ItemAPI.EnemyBuilder.AnimationType;
using System.Collections;
using Dungeonator;
using System.Linq;
using Brave.BulletScript;
using GungeonAPI;
using static DirectionalAnimation;
using EnemyAPI;

namespace HallOfGundead
{
    class PumpKin : AIActor
    {
		public static GameObject prefab;
		public static readonly string guid = "pumpkin";
		private static tk2dSpriteCollectionData PumpKinCollection;
		public static GameObject shootpoint;
		public static void Init()
		{
			PumpKin.BuildPrefab();
		}

        private static string[] spritePaths = new string[]
        {
			
			//idles
			"HallOfGundead/Resources/pumpKin/pomp_idle_001", 
            "HallOfGundead/Resources/pumpKin/pomp_idle_002", 
            "HallOfGundead/Resources/pumpKin/pomp_idle_003", 
            "HallOfGundead/Resources/pumpKin/pomp_idle_004", 
			//die
			"HallOfGundead/Resources/pumpKin/pomp_die_001",
            "HallOfGundead/Resources/pumpKin/pomp_die_002",
            "HallOfGundead/Resources/pumpKin/pomp_die_003",
            "HallOfGundead/Resources/pumpKin/pomp_die_004",
            "HallOfGundead/Resources/pumpKin/pomp_die_005",
            "HallOfGundead/Resources/pumpKin/pomp_die_006",
            //hit
            "HallOfGundead/Resources/pumpKin/pomp_hit_001",
        };
        public static void BuildPrefab()
		{
			//
			bool exists = prefab != null || EnemyBuilder.Dictionary.ContainsKey(guid);
            if (!exists)
            {

                prefab = EnemyBuilder.BuildPrefab("pumpkin", guid, spritePaths[0], new IntVector2(4, 3), new IntVector2(9, 8), false);
                Toolbox.GenerateOrAddToRigidBody(prefab, CollisionLayer.EnemyHitBox, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(20, 23), offset: new IntVector2(5,0));
                var companion = prefab.AddComponent<EnemyBehavior>();
                companion.aiActor.SetIsFlying(false, "no wings bish");
                companion.aiActor.knockbackDoer.weight = 80;
                companion.aiActor.MovementSpeed = 7f;
                companion.aiActor.healthHaver.PreventAllDamage = false;
                companion.aiActor.CollisionDamage = 1f;
                companion.aiActor.HasShadow = false;
                companion.aiActor.IgnoreForRoomClear = false;
                companion.aiActor.aiAnimator.HitReactChance = 0f;
                companion.aiActor.specRigidbody.CollideWithOthers = true;
                companion.aiActor.specRigidbody.CollideWithTileMap = true;
                companion.aiActor.PreventFallingInPitsEver = false;
                companion.aiActor.CollisionKnockbackStrength = 5f;
                companion.aiActor.healthHaver.SetHealthMaximum(80f, null, false);
                AIAnimator aiAnimator = companion.aiAnimator;
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "idle"
                    }
                };
                aiAnimator.HitAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "hit"
                    }
                };
                aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
                {
                    new AIAnimator.NamedDirectionalAnimation
                    {
                    name = "die",
                    anim = new DirectionalAnimation
                        {
                            Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                            Flipped = new DirectionalAnimation.FlipType[2],
                            AnimNames = new string[]
                            {

                       "die",
                            }

                        }
                    }
                };

                bool flag3 = PumpKinCollection == null;
                if (flag3)
                {
                    PumpKinCollection = SpriteBuilder.ConstructCollection(prefab, "PumpKin_Collection");
                    UnityEngine.Object.DontDestroyOnLoad(PumpKinCollection);
                    for (int i = 0; i < spritePaths.Length; i++)
                    {
                        SpriteBuilder.AddSpriteToCollection(spritePaths[i], PumpKinCollection);
                    }
                    SpriteBuilder.AddAnimation(companion.spriteAnimator, PumpKinCollection, new List<int>
                    {

                    0,
                    1,
                    2,
                    3,
                    }, "idle", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 10f;
                    SpriteBuilder.AddAnimation(companion.spriteAnimator, PumpKinCollection, new List<int>
                    {

                    4,
                    5,
                    6,
                    7,
                    8,
                    9
                    }, "die", tk2dSpriteAnimationClip.WrapMode.Once).fps = 7f;
                    SpriteBuilder.AddAnimation(companion.spriteAnimator, PumpKinCollection, new List<int>
                    {
                    10,
                    10,
                    10
                    }, "hit", tk2dSpriteAnimationClip.WrapMode.Once).fps = 1f;

                }
                var bs = prefab.GetComponent<BehaviorSpeculator>();

                prefab.GetOrAddComponent<ObjectVisibilityManager>();
                BehaviorSpeculator behaviorSpeculator = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").behaviorSpeculator;
                bs.OverrideBehaviors = behaviorSpeculator.OverrideBehaviors;
                bs.OtherBehaviors = behaviorSpeculator.OtherBehaviors;
                shootpoint = new GameObject("shoot");
                shootpoint.transform.parent = companion.transform;
                shootpoint.transform.position = companion.sprite.WorldCenter;
                bs.TargetBehaviors = EnemyDatabase.GetOrLoadByGuid("042edb1dfb614dc385d5ad1b010f2ee3").behaviorSpeculator.TargetBehaviors;
                bs.MovementBehaviors = EnemyDatabase.GetOrLoadByGuid("042edb1dfb614dc385d5ad1b010f2ee3").behaviorSpeculator.MovementBehaviors;
                if (EnemyDatabase.GetOrLoadByGuid("042edb1dfb614dc385d5ad1b010f2ee3").behaviorSpeculator.TargetBehaviors == null)
                {
                    ETGModConsole.Log("TargetBehaviours null");
                }
                if (EnemyDatabase.GetOrLoadByGuid("042edb1dfb614dc385d5ad1b010f2ee3").behaviorSpeculator.MovementBehaviors == null)
                {
                    ETGModConsole.Log("MovementBehaviors null");
                }
                bs.InstantFirstTick = behaviorSpeculator.InstantFirstTick;
                bs.TickInterval = behaviorSpeculator.TickInterval;
                bs.PostAwakenDelay = behaviorSpeculator.PostAwakenDelay;
                bs.RemoveDelayOnReinforce = behaviorSpeculator.RemoveDelayOnReinforce;
                bs.OverrideStartingFacingDirection = behaviorSpeculator.OverrideStartingFacingDirection;
                bs.StartingFacingDirection = behaviorSpeculator.StartingFacingDirection;
                bs.SkipTimingDifferentiator = behaviorSpeculator.SkipTimingDifferentiator;
                Game.Enemies.Add("hotg:pompbulon", companion.aiActor);
                SpriteBuilder.AddSpriteToCollection("HallOfGundead/Resources/pumpKin/pomp_idle_001", SpriteBuilder.ammonomiconCollection);
                if (companion.GetComponent<EncounterTrackable>() != null)
                {
                    UnityEngine.Object.Destroy(companion.GetComponent<EncounterTrackable>());
                }
                companion.encounterTrackable = companion.gameObject.AddComponent<EncounterTrackable>();
                companion.encounterTrackable.journalData = new JournalEntry();
                companion.encounterTrackable.EncounterGuid = "hotg:pompbulon";
                companion.encounterTrackable.prerequisites = new DungeonPrerequisite[0];
                companion.encounterTrackable.journalData.SuppressKnownState = false;
                companion.encounterTrackable.journalData.IsEnemy = true;
                companion.encounterTrackable.journalData.SuppressInAmmonomicon = false;
                companion.encounterTrackable.ProxyEncounterGuid = "";
                companion.encounterTrackable.journalData.AmmonomiconSprite = "HallOfGundead/Resources/pumpKin/pomp_idle_001";
                companion.encounterTrackable.journalData.enemyPortraitSprite = ItemAPI.ResourceExtractor.GetTextureFromResource("HallOfGundead/Resources/pumpKin/PompbulonAmmonomicon.png");
                companion.encounterTrackable.journalData.PrimaryDisplayName = "Pompbulon";
                companion.encounterTrackable.journalData.NotificationPanelDescription = "Farm Grown";
                companion.encounterTrackable.journalData.AmmonomiconFullEntry = "A mutated pumpkin that has been enlisted into the Blobulon ranks.";
                EnemyBuilder.AddEnemyToDatabase(companion.gameObject, "hotg:pompbulon");
                EnemyDatabase.GetEntry("hotg:pompbulon").isInBossTab = false;
                EnemyDatabase.GetEntry("hotg:pompbulon").isNormalEnemy = true;
            }
        }
            



		public class EnemyBehavior : BraveBehaviour
		{

			private RoomHandler m_StartRoom;
			private void Update()
			{
				if (!base.aiActor.HasBeenEngaged) { CheckPlayerRoom(); }
                DeadlyDeadlyGoopManager goopManagerForGoopType = DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(GoopLibrary.PumpkinGuts);
                goopManagerForGoopType.AddGoopCircle(this.aiActor.specRigidbody.UnitCenter, 0.8f, -1, false, -1);

            }

			private void CheckPlayerRoom()
			{

				if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != null && GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() == m_StartRoom)
				{
					GameManager.Instance.StartCoroutine(LateEngage());
				}

			}

			private IEnumerator LateEngage()
			{
				yield return new WaitForSeconds(0.5f);
				base.aiActor.HasBeenEngaged = true;
				yield break;
			}

			private void Start()
			{
				m_StartRoom = aiActor.GetAbsoluteParentRoom();
				//base.aiActor.HasBeenEngaged = true;
				base.aiActor.healthHaver.OnPreDeath += (obj) =>
				{
					AkSoundEngine.PostEvent("Play_OBJ_stone_crumble_01", base.aiActor.gameObject);
				};
                
			}
		}

	}
}

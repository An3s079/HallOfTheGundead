﻿using System;
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

        };
        public static void BuildPrefab()
		{
			//
			bool exists = prefab != null || EnemyBuilder.Dictionary.ContainsKey(guid);
			if (!exists)
			{

				prefab = EnemyBuilder.BuildPrefab("pumpkin", guid, spritePaths[0], new IntVector2(0, 0), new IntVector2(8, 9), false);
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

                }
                var bs = prefab.GetComponent<BehaviorSpeculator>();
                
                prefab.GetOrAddComponent<ObjectVisibilityManager>();
                BehaviorSpeculator behaviorSpeculator = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").behaviorSpeculator;
                bs.OverrideBehaviors = behaviorSpeculator.OverrideBehaviors;
                bs.OtherBehaviors = behaviorSpeculator.OtherBehaviors;
                shootpoint = new GameObject("fuck");
                shootpoint.transform.parent = companion.transform;
                shootpoint.transform.position = companion.sprite.WorldCenter;
                GameObject m_CachedGunAttachPoint = companion.transform.Find("fuck").gameObject;
                bs.TargetBehaviors = new List<TargetBehaviorBase>
            {
                new TargetPlayerBehavior
                {
                    Radius = 35f,
                    LineOfSight = false,
                    ObjectPermanence = true,
                    SearchInterval = 0f,
                    PauseOnTargetSwitch = false,
                    PauseTime = 0f,
                    
                }
            };
                bs.AttackBehaviors = new List<AttackBehaviorBase>() {
                new ShootBehavior() {
                    ShootPoint = m_CachedGunAttachPoint,
                    BulletScript = new CustomBulletScriptSelector(typeof(EmptyScript)),
                    LeadAmount = 0f,
                    AttackCooldown = 100f,
                    RequiresLineOfSight = false,
                    Uninterruptible = true
                }
                };
                bs.MovementBehaviors = new List<MovementBehaviorBase>
            {
                new SeekTargetBehavior
                {
                    StopWhenInRange = false,
                    CustomRange = 0f,
                    LineOfSight = false,
                    ReturnToSpawn = false,
                    SpawnTetherDistance = 0f,
                    PathInterval = 0.5f,
                    SpecifyRange = false,
                    MinActiveRange = 0f,
                    MaxActiveRange = 0f
                }
            };

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

		public class EmptyScript : Script
		{
			//protected override IEnumerator Top()
			//{
			//	if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
			//	{
			//		base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("796a7ed4ad804984859088fc91672c7f").bulletBank.GetBullet("default"));
			//	}
			//	for (int i = 0; i < 4; i++)
			//	{
			//		this.Fire(new Direction((float)(i * 80), DirectionType.Absolute, -1f), new Speed(5f, SpeedType.Absolute), new DefaultBullet());
			//	}
			//	yield break;
			//}
		}

		public class DefaultBullet : Bullet
		{
			public DefaultBullet() : base("default", false, false, false) { }

		}

	}
}
using System;
using System.Collections.Generic;
using Gungeon;
using ItemAPI;
using UnityEngine;
using AnimationType = ItemAPI.EnemyBuilder.AnimationType;
using System.Collections;
using Dungeonator;
using System.Linq;
using Brave.BulletScript;
using GungeonAPI;
using EnemyBulletBuilder;

namespace HallOfGundead
{
	class Trankenstine : AIActor
	{
		public static GameObject prefab;
		public static readonly string guid = "trankensteins-monster";
		private static tk2dSpriteCollectionData TrankCollection;


		public static void Init()
		{
			Trankenstine.BuildPrefab();
		}

		public static void BuildPrefab()
		{
			//
			bool flag = prefab != null || EnemyBuilder.Dictionary.ContainsKey(guid);
			bool flag2 = flag;
			if (!flag2)
			{
				AIActor aIActor = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5");
				prefab = EnemyBuilder.BuildPrefab("Trankensteins Monster", guid, spritePaths[0], new IntVector2(0, 0), new IntVector2(8, 9), true);
				var companion = prefab.AddComponent<EnemyBehavior>(); ;
				companion.aiActor.knockbackDoer.weight = 70;
				companion.aiActor.MovementSpeed = 2f;
				companion.aiActor.healthHaver.PreventAllDamage = false;
				companion.aiActor.CollisionDamage = 1f;
				companion.aiActor.HasShadow = false;
				companion.aiActor.IgnoreForRoomClear = false;
				companion.aiActor.aiAnimator.HitReactChance = 0f;
				companion.aiActor.specRigidbody.CollideWithOthers = true;
				companion.aiActor.specRigidbody.CollideWithTileMap = true;
				companion.aiActor.PreventFallingInPitsEver = false;
				companion.aiActor.healthHaver.ForceSetCurrentHealth(30f);
				companion.aiActor.CollisionKnockbackStrength = 5f;
				companion.aiActor.CanTargetPlayers = true;
				companion.aiActor.healthHaver.SetHealthMaximum(30f, null, false);
				companion.aiActor.specRigidbody.PixelColliders.Clear();
				companion.aiActor.specRigidbody.PixelColliders.Add(new PixelCollider
				{
					ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
					CollisionLayer = CollisionLayer.EnemyCollider,
					IsTrigger = false,
					BagleUseFirstFrameOnly = false,
					SpecifyBagelFrame = string.Empty,
					BagelColliderNumber = 0,
					ManualOffsetX = 0,
					ManualOffsetY = 0,
					ManualWidth = 15,
					ManualHeight = 17,
					ManualDiameter = 0,
					ManualLeftX = 0,
					ManualLeftY = 0,
					ManualRightX = 0,
					ManualRightY = 0,
				});
				companion.aiActor.specRigidbody.PixelColliders.Add(new PixelCollider
				{

					ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
					CollisionLayer = CollisionLayer.EnemyHitBox,
					IsTrigger = false,
					BagleUseFirstFrameOnly = false,
					SpecifyBagelFrame = string.Empty,
					BagelColliderNumber = 0,
					ManualOffsetX = 0,
					ManualOffsetY = 0,
					ManualWidth = 15,
					ManualHeight = 17,
					ManualDiameter = 0,
					ManualLeftX = 0,
					ManualLeftY = 0,
					ManualRightX = 0,
					ManualRightY = 0,



				});
				companion.aiActor.CorpseObject = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").CorpseObject;
				companion.aiActor.PreventBlackPhantom = false;
				AIAnimator aiAnimator = companion.aiAnimator;
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

						   "die_right",
						   "die_left"

							}

						}
					}
				};
				aiAnimator.IdleAnimation = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
					Flipped = new DirectionalAnimation.FlipType[2],
					AnimNames = new string[]
					{
						"idle_right",
						"idle_left"
					}
				};
				aiAnimator.MoveAnimation = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
					Flipped = new DirectionalAnimation.FlipType[2],
					AnimNames = new string[]
						{
						"run_right",
						"run_left"
						}
				};

				bool flag3 = TrankCollection == null;
				if (flag3)
				{
					TrankCollection = ItemAPI.SpriteBuilder.ConstructCollection(prefab, "Tranken_Collection");
					UnityEngine.Object.DontDestroyOnLoad(TrankCollection);
					for (int i = 0; i < spritePaths.Length; i++)
					{
						ItemAPI.SpriteBuilder.AddSpriteToCollection(spritePaths[i], TrankCollection);
					}
					ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, TrankCollection, new List<int>
					{


					0,
					1

					}, "idle_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 5f;
					ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, TrankCollection, new List<int>
					{

					2,
					3

					}, "idle_right", tk2dSpriteAnimationClip.WrapMode.Once).fps = 5f;
					ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, TrankCollection, new List<int>
					{

					4,
					5,
					6,
					7,
					8,
					9
					}, "run_left", tk2dSpriteAnimationClip.WrapMode.Once).fps = 10f;
					ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, TrankCollection, new List<int>
					{

					10,
					11,
					12,
					13,
					14,
					15


					}, "run_right", tk2dSpriteAnimationClip.WrapMode.Once).fps = 10f;
					ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, TrankCollection, new List<int>
					{



					16,
					17,
					18,
					19,
					20


					}, "die_right", tk2dSpriteAnimationClip.WrapMode.Once).fps = 5f;
					ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, TrankCollection, new List<int>
					{
					21,
					22,
					23,
					24,
					25

					}, "die_left", tk2dSpriteAnimationClip.WrapMode.Once).fps = 5f;

				}

				var bs = prefab.GetComponent<BehaviorSpeculator>();
				BehaviorSpeculator behaviorSpeculator = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").behaviorSpeculator;

				bs.OverrideBehaviors = behaviorSpeculator.OverrideBehaviors;
				bs.OtherBehaviors = behaviorSpeculator.OtherBehaviors;
				
				bs.TargetBehaviors = new List<TargetBehaviorBase>
			{
				new TargetPlayerBehavior
				{
					Radius = 35f,
					LineOfSight = true,
					ObjectPermanence = true,
					SearchInterval = 0.25f,
					PauseOnTargetSwitch = false,
					PauseTime = 0.25f
					
				}
			};
				bs.AttackBehaviors = new List<AttackBehaviorBase>() {
				new ShootGunBehavior() {
					GroupCooldownVariance = 0.2f,
					LineOfSight = false,
					WeaponType = WeaponType.BulletScript,
					OverrideBulletName = null,
					BulletScript = new CustomBulletScriptSelector(typeof(TrankScript)),
					FixTargetDuringAttack = true,
					StopDuringAttack = true,
					LeadAmount = 0f,
					LeadChance = 1,
					RespectReload = true,
					MagazineCapacity = 3,
					ReloadSpeed = 5f,
					EmptiesClip = true,
					SuppressReloadAnim = false,
					TimeBetweenShots = -1,
					PreventTargetSwitching = true,
					OverrideAnimation = null,
					OverrideDirectionalAnimation = null,
					HideGun = false,
					UseLaserSight = false,
					UseGreenLaser = false,
					PreFireLaserTime = -1,
					AimAtFacingDirectionWhenSafe = false,
					Cooldown = 0.6f,
					CooldownVariance = 0,
					AttackCooldown = 0,
					GlobalCooldown = 0,
					InitialCooldown = 0,
					InitialCooldownVariance = 0,
					GroupName = null,
					GroupCooldown = 0,
					MinRange = 0,
					Range = 16,
					MinWallDistance = 0,
					MaxEnemiesInRoom = 0,
					MinHealthThreshold = 0,
					MaxHealthThreshold = 1,
					HealthThresholds = new float[0],
					AccumulateHealthThresholds = true,
					targetAreaStyle = null,
					IsBlackPhantom = false,
					resetCooldownOnDamage = null,
					RequiresLineOfSight = true,
					MaxUsages = 0,

				}
			};
				bs.MovementBehaviors = new List<MovementBehaviorBase>
			{
				new SeekTargetBehavior
				{
					StopWhenInRange = true,
					CustomRange = 7f,
					LineOfSight = false,
					ReturnToSpawn = false,
					SpawnTetherDistance = 0f,
					PathInterval = 0.5f,
					SpecifyRange = false,
					MinActiveRange = 0f,
					MaxActiveRange = 0f
				}
			};
				//BehaviorSpeculator load = EnemyDatabase.GetOrLoadByGuid("206405acad4d4c33aac6717d184dc8d4").behaviorSpeculator;
				//Tools.DebugInformation(load);
				prefab.GetOrAddComponent<ObjectVisibilityManager>();
				bs.InstantFirstTick = behaviorSpeculator.InstantFirstTick;
				bs.TickInterval = behaviorSpeculator.TickInterval;
				bs.PostAwakenDelay = behaviorSpeculator.PostAwakenDelay;
				bs.RemoveDelayOnReinforce = behaviorSpeculator.RemoveDelayOnReinforce;
				bs.OverrideStartingFacingDirection = behaviorSpeculator.OverrideStartingFacingDirection;
				bs.StartingFacingDirection = behaviorSpeculator.StartingFacingDirection;
				bs.SkipTimingDifferentiator = behaviorSpeculator.SkipTimingDifferentiator;
				GameObject m_CachedGunAttachPoint = companion.transform.Find("GunAttachPoint").gameObject;
				var yah = companion.transform.Find("GunAttachPoint").gameObject;
				yah.transform.position = companion.aiActor.transform.position;
				yah.transform.localPosition = new Vector2(0f, 0.3f);
				EnemyBuilder.DuplicateAIShooterAndAIBulletBank(prefab, aIActor.aiShooter, aIActor.GetComponent<AIBulletBank>(), 42, yah.transform);

				BulletBuilder.CreateBulletPrefab("HallOfGundead/Resources/Trankenstein/TrankensteinBullet.png", "NailBulletThing", true, PixelCollider.PixelColliderGeneration.Manual, 11, 7, 0, 0, true);
				Game.Enemies.Add("hotg:trankensteins_monster", companion.aiActor);

				bitch = companion.aiActor;
				
			}
		}
		private static AIActor bitch;
		public static GameObject nail;
		public class TrankScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				AkSoundEngine.PostEvent("Play_WPN_stickycrossbow_shot_01", this.BulletBank.aiActor.gameObject);
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(BulletBuilder.GetBulletEntryByName("NailBulletThing"));
				}
				float aimDirection = base.GetAimDirection(0.7f, 9f);
				for (int i = -2; i <= 2; i++)
				{

					this.Fire(new Direction(aimDirection + (float)(i * 6), DirectionType.Absolute, -1f), new Speed(9f, SpeedType.Absolute), new TrankBullet());
					yield return this.Wait(2);
				}
				yield break;
			}
		}


		public class TrankBullet : Bullet
		{
			public TrankBullet() : base("NailBulletThing", false, false, false)
			{


			} 
		}




		private static string[] spritePaths = new string[]
		{
			
			//idles
			"HallOfGundead/Resources/Trankenstein/tranken_idle_left_001",
			"HallOfGundead/Resources/Trankenstein/tranken_idle_left_002",
			"HallOfGundead/Resources/Trankenstein/tranken_idle_right_001",
			"HallOfGundead/Resources/Trankenstein/tranken_idle_right_002",

			//run
			"HallOfGundead/Resources/Trankenstein/tranken_run_left_001",
			"HallOfGundead/Resources/Trankenstein/tranken_run_left_002",
			"HallOfGundead/Resources/Trankenstein/tranken_run_left_003",
			"HallOfGundead/Resources/Trankenstein/tranken_run_left_004",
			"HallOfGundead/Resources/Trankenstein/tranken_run_left_005",
			"HallOfGundead/Resources/Trankenstein/tranken_run_left_006",

			"HallOfGundead/Resources/Trankenstein/tranken_run_right_001",
			"HallOfGundead/Resources/Trankenstein/tranken_run_right_002",
			"HallOfGundead/Resources/Trankenstein/tranken_run_right_003",
			"HallOfGundead/Resources/Trankenstein/tranken_run_right_004",
			"HallOfGundead/Resources/Trankenstein/tranken_run_right_005",
			"HallOfGundead/Resources/Trankenstein/tranken_run_right_006",

			//die left
			"HallOfGundead/Resources/Trankenstein/tranken_die_left_001",
			"HallOfGundead/Resources/Trankenstein/tranken_die_left_002",
			"HallOfGundead/Resources/Trankenstein/tranken_die_left_003",
			"HallOfGundead/Resources/Trankenstein/tranken_die_left_004",
			"HallOfGundead/Resources/Trankenstein/tranken_die_left_005",

			//die right
			"HallOfGundead/Resources/Trankenstein/tranken_die_right_001",
			"HallOfGundead/Resources/Trankenstein/tranken_die_right_002",
			"HallOfGundead/Resources/Trankenstein/tranken_die_right_003",
			"HallOfGundead/Resources/Trankenstein/tranken_die_right_004",
			"HallOfGundead/Resources/Trankenstein/tranken_die_right_005",
			//who even needs attack anims amirite
		};

		public class EnemyBehavior : BraveBehaviour
		{

			private RoomHandler m_StartRoom;
			private void Update()
			{
				if (!base.aiActor.HasBeenEngaged) { CheckPlayerRoom(); }
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
				yield return new WaitForSeconds(2);
				base.aiActor.HasBeenEngaged = true;
				yield break;
			}
			private void Start()
			{
				m_StartRoom = aiActor.GetAbsoluteParentRoom();
				//base.aiActor.HasBeenEngaged = true;
				base.aiActor.healthHaver.OnPreDeath += (obj) =>
				{
					AkSoundEngine.PostEvent("Play_OBJ_skeleton_collapse_01", base.aiActor.gameObject);
				};
			}


		}


	}
}

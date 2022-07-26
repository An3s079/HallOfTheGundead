using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;
using EnemyAPI;
using UnityEngine;
using Brave.BulletScript;
using Gungeon;
using System.Collections;
using InControl;
using Dungeonator;
using System.Reflection;

namespace HallOfGundead
{
	class VampireMantis : AIActor
	{
		public static GameObject prefab;
		public static readonly string guid = "Vampire-Mantis";
		private static tk2dSpriteCollectionData TemplateBossCollection;
		public static GameObject shootpoint;
		public static GameObject shootpoint1;
		private static Texture2D BossCardTexture = ItemAPI.ResourceExtractor.GetTextureFromResource("HallOfGundead/Resources/Vampire_Mantis_Bosscard.png");
		public static string TargetVFX;
		public static Texture _gradTexture;

		public static void Init()
		{
			VampireMantis.BuildPrefab();
		}

		public static void BuildPrefab()
		{
			bool flag = prefab != null || BossBuilder.Dictionary.ContainsKey(guid);
			if (!flag)
			{
				prefab = BossBuilder.BuildPrefab("VampireMantis", guid, spritePaths[0], new IntVector2(0, 0), new IntVector2(8, 9), false, true);
				prefab.name = "VampireMantis";
				var companion = prefab.AddComponent<EnemyBehavior>();
				companion.aiActor = prefab.GetComponent<AIActor>();
				companion.aiAnimator = prefab.GetComponent<AIAnimator>();
				companion.spriteAnimator = prefab.GetComponent<tk2dSpriteAnimator>();
				companion.healthHaver = prefab.GetComponent<HealthHaver>();
				companion.specRigidbody = prefab.GetComponent<SpeculativeRigidbody>();
				prefab.GetComponent<AIActor>().enabled = true;
				prefab.GetComponent<AIAnimator>().enabled = true;
				if (companion.aiActor is null)
				{
					ETGModConsole.Log("companion.aiactor is null!");
				}
				companion.aiActor.knockbackDoer.weight = 200;
				companion.aiActor.MovementSpeed = 3.2f;
				companion.aiActor.healthHaver.PreventAllDamage = false;
				companion.aiActor.CollisionDamage = 1f;
				companion.aiActor.HasShadow = false;
				companion.aiActor.IgnoreForRoomClear = false;
				companion.aiActor.aiAnimator.HitReactChance = 0.05f;
				companion.aiActor.specRigidbody.CollideWithOthers = true;
				companion.aiActor.specRigidbody.CollideWithTileMap = true;
				companion.aiActor.PreventFallingInPitsEver = true;
				companion.aiActor.healthHaver.ForceSetCurrentHealth(2000);
				companion.aiActor.healthHaver.SetHealthMaximum(2000);
				companion.aiActor.CollisionKnockbackStrength = 2f;
				companion.aiActor.CanTargetPlayers = true;
				companion.aiActor.procedurallyOutlined = true;
				companion.aiActor.HasShadow = true;
				companion.aiActor.SetIsFlying(true, "bat");

				companion.aiActor.specRigidbody.PixelColliders.Clear();
				

				companion.aiActor.CorpseObject = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").CorpseObject;
				companion.aiActor.PreventBlackPhantom = false;
				AIAnimator aiAnimator = companion.aiAnimator;
				aiAnimator.IdleAnimation = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					Prefix = "idle",
					AnimNames = new string[1],
					Flipped = new DirectionalAnimation.FlipType[1]
				};

				DirectionalAnimation bat_flap = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					AnimNames = new string[]
					{
						"bat_flap",

					},
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "bat_flap",
						anim = bat_flap
					}
				};

				DirectionalAnimation attack = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					AnimNames = new string[]
	{
						"attack",

	},
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "attack",
						anim = attack
					}
				};

				DirectionalAnimation attack_left = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					AnimNames = new string[]
					{
						"attack_left",

					},
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "attack_left",
						anim = attack_left
					}
				};
				DirectionalAnimation attack_right = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					AnimNames = new string[]
	{
						"attack_right",

	},
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "attack_right",
						anim = attack_right
					}
				};

				DirectionalAnimation attack_v1 = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					AnimNames = new string[]
{
						"attack_v1",

},
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "attack_v1",
						anim = attack_v1
					}
				};

				DirectionalAnimation cape_attack = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					AnimNames = new string[]
{
						"cape_attack",

},
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "cape_attack",
						anim = cape_attack
					}
				};
				DirectionalAnimation cape_out = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					AnimNames = new string[]
{
						"cape_out",

},
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "cape_out",
						anim = cape_out
					}
				};
				DirectionalAnimation hit = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					AnimNames = new string[]
{
						"hit",

},
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "hit",
						anim = hit
					}
				};
				DirectionalAnimation teleport_in = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					AnimNames = new string[]
{
						"teleport_in",

},
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "teleport_in",
						anim = teleport_in
					}
				};
				DirectionalAnimation teleport = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					AnimNames = new string[]
{
						"teleport",

},
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "teleport",
						anim = teleport
					}
				};
				DirectionalAnimation almostdone = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					Prefix = "intro",
					AnimNames = new string[1],
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "intro",
						anim = almostdone
					}
				};

				DirectionalAnimation done = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					Prefix = "death",
					AnimNames = new string[1],
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "death",
						anim = done
					}
				};

				bool flag3 = TemplateBossCollection == null;
				if (flag3)
				{
					TemplateBossCollection = SpriteBuilder.ConstructCollection(prefab, "TemplateBossCollection");
					UnityEngine.Object.DontDestroyOnLoad(TemplateBossCollection);
					for (int i = 0; i < spritePaths.Length; i++)
					{
						SpriteBuilder.AddSpriteToCollection(spritePaths[i], TemplateBossCollection);
					}

					SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{

					6,7,8,9


					}, "idle", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 6f;

					SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{
						27,28,29,30,31,32

					}, "bat_flap", tk2dSpriteAnimationClip.WrapMode.Once).fps = 7f;

					SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{

						33,34,35,36,37,38,39,40,41,42
					}, "attack", tk2dSpriteAnimationClip.WrapMode.Once).fps = 3.5f;

					SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{

						43,44,45,46,47,48
					}, "attack_left", tk2dSpriteAnimationClip.WrapMode.Once).fps = 7f;

					SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{

						49,50,51,52,53, 54


					}, "attack_right", tk2dSpriteAnimationClip.WrapMode.Once).fps = 11f;


					SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{
						55,56,57,58
					}, "attack_v1", tk2dSpriteAnimationClip.WrapMode.Once).fps = 7f;

					SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{
						59,60,61,62,63

					}, "cape_attack", tk2dSpriteAnimationClip.WrapMode.Once).fps = 7f;
					SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{
						64,65,66

					}, "cape_out", tk2dSpriteAnimationClip.WrapMode.Once).fps = 7f;
					SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{
						67,68,69

					}, "hit", tk2dSpriteAnimationClip.WrapMode.Once).fps = 7f; SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{
						24,25,26

					}, "teleport_in", tk2dSpriteAnimationClip.WrapMode.Once).fps = 7f; SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{
						10,11,12,13,14,15,16,17,18,19,20,21,22,23

					}, "teleport", tk2dSpriteAnimationClip.WrapMode.Once).fps = 7f;
					SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{
				
				0,1,2,3,4,5


					}, "intro", tk2dSpriteAnimationClip.WrapMode.Once).fps = 9f;
					SpriteBuilder.AddAnimation(companion.spriteAnimator, TemplateBossCollection, new List<int>
					{
						70,71,72,73
					}, "death", tk2dSpriteAnimationClip.WrapMode.Once).fps = 10f;

				}

				var bs = prefab.GetComponent<BehaviorSpeculator>();
				BehaviorSpeculator behaviorSpeculator = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").behaviorSpeculator;
				bs.OverrideBehaviors = behaviorSpeculator.OverrideBehaviors;
				bs.OtherBehaviors = behaviorSpeculator.OtherBehaviors;

				shootpoint = new GameObject("attach");
				shootpoint.transform.parent = companion.transform;
				shootpoint.transform.position = new Vector2(1.5f, 2.5f);
				GameObject m_CachedGunAttachPoint = companion.transform.Find("attach").gameObject;

				shootpoint1 = new GameObject("bollocks");
				shootpoint1.transform.parent = companion.transform;
				shootpoint1.transform.position = new Vector2(1.1f, 1.1f);
				GameObject m_CachedGunAttachPoint1 = companion.transform.Find("bollocks").gameObject;

				bs.TargetBehaviors = new List<TargetBehaviorBase>
			{
				new TargetPlayerBehavior
				{
					Radius = 35f,
					LineOfSight = false,
					ObjectPermanence = true,
					SearchInterval = 0.25f,
					PauseOnTargetSwitch = false,
					PauseTime = 0.25f
				}
			};

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

				},
			};
				bs.AttackBehaviorGroup.AttackBehaviors = new List<AttackBehaviorGroup.AttackGroupItem>
				{
					new AttackBehaviorGroup.AttackGroupItem()
					{

					Probability = 1f,
					Behavior = new TeleportBehavior{

						AttackCooldown = 15,
						MinRange = 5,
						MaxDistanceFromPlayer = 50,
						GoneTime = 0.3f,
						teleportOutAnim = "teleport",
						teleportInAnim = "teleport_in",
						teleportOutBulletScript	= new CustomBulletScriptSelector(typeof(TpOut)),
						teleportInBulletScript = new CustomBulletScriptSelector(typeof(TpIn))
					},
					NickName = "TP"
					},
					//new AttackBehaviorGroup.AttackGroupItem()
					//{

					//Probability = 0.7f,
					//Behavior = new ShootBehavior{
					//ShootPoint = m_CachedGunAttachPoint1,
					//BulletScript = new CustomBulletScriptSelector(typeof(TpOut)),
					//LeadAmount = 0f,
					//AttackCooldown = 1f,
					//Cooldown = 2f,
					//TellAnimation = "bottletell",
					//FireAnimation = "bottle",
					//RequiresLineOfSight = true,

					//MultipleFireEvents = true,
					//Uninterruptible = false,
					//	},
					//	NickName = "Bottle"

					//},

					//new AttackBehaviorGroup.AttackGroupItem()
					//{

					//Probability = 0.4f,
					//Behavior = new ShootBehavior{
					//ShootPoint = m_CachedGunAttachPoint,
					//BulletScript = new CustomBulletScriptSelector(typeof(BigWhips)),
					//LeadAmount = 0f,
					//AttackCooldown = 1f,
					//Cooldown = 3f,
					//TellAnimation = "roartell",
					//FireAnimation = "roar",
					//RequiresLineOfSight = true,
					//MultipleFireEvents = true,
					//Uninterruptible = false,
					//	},
					//	NickName = "ROAR"

					//},
					//new AttackBehaviorGroup.AttackGroupItem()
					//{

					//Probability = 0.7f,
					//Behavior = new ShootBehavior{
					//ShootPoint = m_CachedGunAttachPoint1,
					//BulletScript = new CustomBulletScriptSelector(typeof(TpOut)),
					//LeadAmount = 0f,
					//AttackCooldown = 1f,
					//Cooldown = 2f,
					//TellAnimation = "bottletell",
					//FireAnimation = "bottle",
					//RequiresLineOfSight = true,

					//MultipleFireEvents = true,
					//Uninterruptible = false,
					//	},
					//	NickName = "Bottle"

					//},
				};

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
					ManualWidth = 36,
					ManualHeight = 40,
					ManualDiameter = 0,
					ManualLeftX = 0,
					ManualLeftY = 0,
					ManualRightX = 0,
					ManualRightY = 0,
					Sprite = (tk2dBaseSprite)companion.spriteAnimator.sprite
				}) ; 
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
					ManualWidth = 36,
					ManualHeight = 40,
					ManualDiameter = 0,
					ManualLeftX = 0,
					ManualLeftY = 0,
					ManualRightX = 0,
					ManualRightY = 0,



				});

				bs.InstantFirstTick = behaviorSpeculator.InstantFirstTick;
				bs.TickInterval = behaviorSpeculator.TickInterval;
				bs.PostAwakenDelay = behaviorSpeculator.PostAwakenDelay;
				bs.RemoveDelayOnReinforce = behaviorSpeculator.RemoveDelayOnReinforce;
				bs.OverrideStartingFacingDirection = behaviorSpeculator.OverrideStartingFacingDirection;
				bs.StartingFacingDirection = behaviorSpeculator.StartingFacingDirection;
				bs.SkipTimingDifferentiator = behaviorSpeculator.SkipTimingDifferentiator;
				Game.Enemies.Add("hotg:vampiremantis", companion.aiActor);




				GenericIntroDoer miniBossIntroDoer = prefab.AddComponent<GenericIntroDoer>();
				prefab.AddComponent<VampireMantisIntroDoer>();
				miniBossIntroDoer.triggerType = GenericIntroDoer.TriggerType.PlayerEnteredRoom;
				miniBossIntroDoer.initialDelay = 0.15f;
				miniBossIntroDoer.cameraMoveSpeed = 14;
				miniBossIntroDoer.specifyIntroAiAnimator = null;
				miniBossIntroDoer.BossMusicEvent = "Play_MUS_Lich_Double_01";
				miniBossIntroDoer.PreventBossMusic = false;
				miniBossIntroDoer.InvisibleBeforeIntroAnim = true;
				miniBossIntroDoer.preIntroAnim = string.Empty;
				miniBossIntroDoer.preIntroDirectionalAnim = string.Empty;
				miniBossIntroDoer.introAnim = "intro";
				miniBossIntroDoer.introDirectionalAnim = string.Empty;
				miniBossIntroDoer.continueAnimDuringOutro = false;
				miniBossIntroDoer.cameraFocus = null;
				miniBossIntroDoer.roomPositionCameraFocus = Vector2.zero;
				miniBossIntroDoer.restrictPlayerMotionToRoom = false;
				miniBossIntroDoer.fusebombLock = false;
				miniBossIntroDoer.AdditionalHeightOffset = 0;
				FloorModModule.Strings.Enemies.Set("#VAMPIRENAME", "Vampire Mantis");
				FloorModModule.Strings.Enemies.Set("#VAMPIRESHDES", "I Vant to Suck Your Dick!");
				FloorModModule.Strings.Enemies.Set("#QUOTE", "");
				miniBossIntroDoer.portraitSlideSettings = new PortraitSlideSettings()
				{
					bossNameString = "#VAMPIRENAME",
					bossSubtitleString = "#VAMPIRESHDES",
					bossQuoteString = "#QUOTE",
					bossSpritePxOffset = IntVector2.Zero,
					topLeftTextPxOffset = IntVector2.Zero,
					bottomRightTextPxOffset = IntVector2.Zero,
					bgColor = Color.cyan
				};
				if (BossCardTexture)
				{
					miniBossIntroDoer.portraitSlideSettings.bossArtSprite = BossCardTexture;
					miniBossIntroDoer.SkipBossCard = false;
					companion.aiActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.SubbossBar;
				}
				else
				{
					miniBossIntroDoer.SkipBossCard = true;
					companion.aiActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.SubbossBar;
				}
				miniBossIntroDoer.SkipFinalizeAnimation = true;
				miniBossIntroDoer.RegenerateCache();

				//==================
				//Important for not breaking basegame stuff!
				StaticReferenceManager.AllHealthHavers.Remove(companion.aiActor.healthHaver);
				//==================
			}

		}

		public class SpawnDash : Script
		{
			protected override IEnumerator Top()
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("68a238ed6a82467ea85474c595c49c6e").bulletBank.GetBullet("poundSmall"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("da797878d215453abba824ff902e21b4").bulletBank.GetBullet("snakeBullet"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("41ee1c8538e8474a82a74c4aff99c712").bulletBank.GetBullet("big"));
				}

				base.PostWwiseEvent("Play_ENM_blobulord_reform_01", null);
				base.Fire(new Direction(0, DirectionType.Aim, -1f), new Speed(0, SpeedType.Absolute), new SpawnDash.Superball());

				yield break;
			}

			public class Superball : Bullet
			{
				public Superball() : base("big", false, false, false)
				{
				}
				protected override IEnumerator Top()
				{
					if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
					{
						base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("da797878d215453abba824ff902e21b4").bulletBank.GetBullet("snakeBullet"));
					}
					yield return this.Wait(120);
					base.Vanish(false);
					yield break;
				}
				public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
				{
					if (!preventSpawningProjectiles)
					{
						return;
					}
				}
			}
			public class BurstBullet : Bullet
			{
				public BurstBullet() : base("snakeBullet", false, false, false)
				{
				}
				protected override IEnumerator Top()
				{
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 60);
					yield return base.Wait(60);
					base.Vanish(false);
					yield break;
				}
			}
		}

		public class TpOut : Script
		{
			protected override IEnumerator Top()
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("f38686671d524feda75261e469f30e0b").bulletBank.GetBullet("nibblesBullet"));
				}
				for (int i = 0; i < 50; i++)
				{
					base.Fire(new Direction(i * 7.2f, DirectionType.Absolute, -1f), new Speed(8, SpeedType.Absolute), new TpIn.ArrowBullet());
					yield return this.Wait(1.66f);

				}
				yield break;
			}
		}
		public class TpIn : Script
		{
			protected override IEnumerator Top()
			{
				var enemyPrefab = Game.Enemies["king_bullat"];
				for (int i = -1; i < 1; i++)
				{
					IntVector2? targetCenter = new IntVector2?(base.BulletBank.aiActor.CenterPosition.ToIntVector2(VectorConversions.Floor));
					AIActor aIActor = AIActor.Spawn(enemyPrefab, targetCenter.Value - new IntVector2(i, i), GameManager.Instance.PrimaryPlayer.CurrentRoom, true, AIActor.AwakenAnimationType.Default, true);
					yield return this.Wait(2);
				}
				yield break;
				
			}
			public class ArrowBullet : Bullet
			{
				// Token: 0x06000A91 RID: 2705 RVA: 0x00030B38 File Offset: 0x0002ED38
				public ArrowBullet() : base("nibblesBullet", false, false, false)
				{
				}

			}
		}
		public class BigWhips : Script
		{
			protected override IEnumerator Top()
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("da797878d215453abba824ff902e21b4").bulletBank.GetBullet("snakeBullet"));
				}
				float Aim = base.AimDirection;

				for (int i = 0; i < 50; i++)
				{
					base.Fire(new Direction(Aim - i * 15f, DirectionType.Absolute, -1f), new Speed(9, SpeedType.Absolute), new BigWhips.BasicBullet());
					yield return this.Wait(1.66f);

				}
				yield break;
			}
			public class BasicBullet : Bullet
			{
				public BasicBullet() : base("snakeBullet", false, false, false)
				{
				}

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
				yield return new WaitForSeconds(1);
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

		public static List<int> Lootdrops = new List<int>
		{
			73,
			85,
			120,
			67,
			224,
			600,
			78
		};





		private static string[] spritePaths = new string[]
		{

			//intro
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_intro_001", //0
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_intro_002",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_intro_003",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_intro_004",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_intro_005",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_intro_006",
				
            //idle
            "HallOfGundead/Resources/Boss_Psychoman/psychomant_idle_001", //6
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_idle_002",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_idle_003",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_idle_004",

			//teleport out
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_001", //10
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_002",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_003",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_004",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_005",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_006",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_007",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_008",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_009",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_010",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_011",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_012",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_014",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_014",

			//teleport in
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_in_001", //24
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_in_002",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_teleport_in_003",

			//bat flap
			"HallOfGundead/Resources/Boss_Psychoman/bat_flap_001", //27
			"HallOfGundead/Resources/Boss_Psychoman/bat_flap_002",
			"HallOfGundead/Resources/Boss_Psychoman/bat_flap_003",
			"HallOfGundead/Resources/Boss_Psychoman/bat_flap_004",
			"HallOfGundead/Resources/Boss_Psychoman/bat_flap_005",
			"HallOfGundead/Resources/Boss_Psychoman/bat_flap_006",

			//attack
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_001", //33
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_002",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_003",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_004",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_005",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_006",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_007",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_008",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_009",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_010",

			//attack left
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_left_001", //43
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_left_002",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_left_003",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_left_004", 
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_left_005",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_left_006",

			//attack right
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_right_001", //49
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_right_002",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_right_003",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_right_004",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_right_005",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_right_006",
			
			//attack v1
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_v1_001", //55
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_v1_002",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_v1_003",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_attack_v1_004",
			
			//attack cape
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_cape_attack_001", //59
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_cape_attack_002",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_cape_attack_003",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_cape_attack_004",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_cape_attack_005",

			//cape out
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_cape_out_001", //64
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_cape_out_002",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_cape_out_003",

			//hit
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_hit_001", //67
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_hit_002",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_hit_003",

			//die
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_die_001", //70
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_die_002",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_die_003",
			"HallOfGundead/Resources/Boss_Psychoman/psychomant_die_004",
		};
	}

	
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using EnemyAPI;
using ItemAPI;
using Gungeon;
using Dungeonator;
using Brave.BulletScript;
using EnemyBulletBuilder;
using System.Collections;

namespace HallOfGundead
{
    class Witch_kin : AIActor
    {
        public static GameObject prefab;
        public static readonly string guid = "witch-Kin";
        private static tk2dSpriteCollectionData WitchKinCollection;
        public static GameObject shootpoint;
        public static void Init()
        {
            BuildPrefab();
        }

        private static string[] spritePaths = new string[]
        {
			
			//idles
			"HallOfGundead/Resources/Witch_Kin/witch_kin_idle_left_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_idle_right_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_idle_back_001",
            
            //run
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_left_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_left_002",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_left_003",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_left_004",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_left_005",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_left_006",

            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_left_back_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_left_back_002",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_left_back_003",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_left_back_004",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_left_back_005",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_left_back_006",

            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_right_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_right_002",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_right_003",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_right_004",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_right_005",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_right_006",

            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_right_back_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_right_back_002",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_right_back_003",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_right_back_004",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_right_back_005",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_run_right_back_006",

            //charge
            "HallOfGundead/Resources/Witch_Kin/witch_kin_fly_charge_back_left_001",

            "HallOfGundead/Resources/Witch_Kin/witch_kin_fly_charge_left_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_fly_charge_left_002",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_fly_charge_left_003",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_fly_charge_left_004",

            "HallOfGundead/Resources/Witch_Kin/witch_kin_fly_charge_back_right_001",

            "HallOfGundead/Resources/Witch_Kin/witch_kin_fly_charge_right_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_fly_charge_right_002",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_fly_charge_right_003",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_fly_charge_right_004",

			//die
			"HallOfGundead/Resources/Witch_Kin/witch_kin_death_left_front_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_death_left_front_002",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_death_left_front_003",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_death_left_front_004",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_death_left_front_005",

            "HallOfGundead/Resources/Witch_Kin/witch_kin_death_right_front_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_death_right_front_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_death_right_front_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_death_right_front_001",
            "HallOfGundead/Resources/Witch_Kin/witch_kin_death_right_front_001",

        };

        public static void BuildPrefab()
        {
            //
            bool exists = prefab != null || EnemyBuilder.Dictionary.ContainsKey(guid);
            if (!exists)
            {

                AIActor aIActor = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5");
                prefab = EnemyBuilder.BuildPrefab("Witch Kin", guid, spritePaths[0], new IntVector2(0, 0), new IntVector2(8, 9), true);
                var companion = prefab.AddComponent<EnemyBehavior>();

                companion.aiActor.knockbackDoer.weight = 200;
                companion.aiActor.MovementSpeed = 3f;
                companion.aiActor.healthHaver.PreventAllDamage = false;
                companion.aiActor.CollisionDamage = 0.5f;
                companion.aiActor.HasShadow = false;
                companion.aiActor.IgnoreForRoomClear = false;
                companion.aiActor.aiAnimator.HitReactChance = 0f;
                companion.aiActor.specRigidbody.CollideWithOthers = true;
                companion.aiActor.specRigidbody.CollideWithTileMap = true;
                companion.aiActor.PreventFallingInPitsEver = false;
                companion.aiActor.healthHaver.ForceSetCurrentHealth(28f);
                companion.aiActor.CollisionKnockbackStrength = 5f;
                companion.aiActor.CanTargetPlayers = true;
                companion.aiActor.healthHaver.SetHealthMaximum(28f, null, false);
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
                    ManualRightY = 0
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
                var bs = prefab.GetComponent<BehaviorSpeculator>();
                BehaviorSpeculator behaviorSpeculator = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").behaviorSpeculator;

                bs.OverrideBehaviors = behaviorSpeculator.OverrideBehaviors;
                bs.OtherBehaviors = behaviorSpeculator.OtherBehaviors;

                AIAnimator aiAnimator = companion.aiAnimator;
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "idle_left",
                        "idle_right"
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
                            "die_left",
                            "die_right"
                            }

                        }
                    },
                    new AIAnimator.NamedDirectionalAnimation
                    {
                        name = "charge",
                        anim = new DirectionalAnimation
                        {
                            Type = DirectionalAnimation.DirectionType.FourWay,
                            Flipped = new DirectionalAnimation.FlipType[4],
                            AnimNames = new string[]
                            {
                            "charge_back_left",
                            "charge_back_right",
                            "charge_front_left",
                            "charge_front_right",
                            }

                        }
                    }
                };

                aiAnimator.MoveAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.FourWay,
                    Flipped = new DirectionalAnimation.FlipType[4],
                    AnimNames = new string[]
                        {
                        "run_back_left",
                        "run_front_right",
                        "run_front_left",
                        "run_back_right",

                        }
                };


                bool flag3 = WitchKinCollection == null;
                if (flag3)
                {
                    WitchKinCollection = ItemAPI.SpriteBuilder.ConstructCollection(prefab, "Witch_Kin_Collection");
                    UnityEngine.Object.DontDestroyOnLoad(WitchKinCollection);
                    for (int i = 0; i < spritePaths.Length; i++)
                    {
                        ItemAPI.SpriteBuilder.AddSpriteToCollection(spritePaths[i], WitchKinCollection);
                    }
                    ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, WitchKinCollection, new List<int>
                    {
                    0,
                    }, "idle_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 5f;

                    ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, WitchKinCollection, new List<int>
                    {
                    1,
                    }, "idle_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 5f;

                    ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, WitchKinCollection, new List<int>
                    {

                    3,
                    4,
                    5,
                    6,
                    7,
                    8
                    }, "run_front_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 10f;

                    ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, WitchKinCollection, new List<int>
                    {

                    9,
                    10,
                    11,
                    12,
                    13,
                    14
                    }, "run_back_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 10f;

                    ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, WitchKinCollection, new List<int>
                    {

                    15,
                    16,
                    17,
                    18,
                    19,
                    20
                    }, "run_front_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 10f;

                    ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, WitchKinCollection, new List<int>
                    {

                    21,
                    22,
                    23,
                    24,
                    25,
                    26
                    }, "run_back_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 10f;

                    ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, WitchKinCollection, new List<int>
                    {

                    27
                    }, "charge_back_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 1f;

                    ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, WitchKinCollection, new List<int>
                    {

                    28,
                    29,
                    30,
                    31
                    }, "charge_front_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 8f;

                    ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, WitchKinCollection, new List<int>
                    {

                    32
                    }, "charge_back_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 1f;

                    ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, WitchKinCollection, new List<int>
                    {

                    33,
                    34,
                    35,
                    36
                    }, "charge_front_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 8;

                    ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, WitchKinCollection, new List<int>
                    {

                    37,
                    38,
                    39,
                    40,
                    41
                    }, "die_left", tk2dSpriteAnimationClip.WrapMode.Once).fps = 5;

                    ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, WitchKinCollection, new List<int>
                    {

                    42,
                    43,
                    44,
                    45,
                    46
                    }, "die_right", tk2dSpriteAnimationClip.WrapMode.Once).fps = 5;
                }


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
               new ShootGunBehavior() {
                    GroupCooldownVariance = 0.2f,
                    LineOfSight = false,
                    WeaponType = WeaponType.BulletScript,
                    OverrideBulletName = null,
                    BulletScript = new CustomBulletScriptSelector(typeof(WitchScript)),
                    FixTargetDuringAttack = true,
                    StopDuringAttack = true,
                    LeadAmount = 0f,
                    LeadChance = 1,
                    RespectReload = true,
                    MagazineCapacity = 1,
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

                },
                new ChargeBehavior()
                {
                    chargeAnim = "charge",
                    chargeSpeed = 10,
                    chargeDamage = 0.5f,
                    HideGun = true,
                    RequiresLineOfSight = true,
                    Cooldown = 8,
                    leadAmount = 0.5f
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
                    MaxActiveRange = 0f,

                }
            };

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
                EnemyBuilder.DuplicateAIShooterAndAIBulletBank(prefab, aIActor.aiShooter, aIActor.GetComponent<AIBulletBank>(), 25, yah.transform);

                Game.Enemies.Add("hotg:witch_kin", companion.aiActor);


            }
        }




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
                    AkSoundEngine.PostEvent("Play_OBJ_stone_crumble_01", base.aiActor.gameObject);
                };

            }


        }

        public class WitchScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
        {
            protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
            {
                AkSoundEngine.PostEvent("Play_WPN_stickycrossbow_shot_01", this.BulletBank.aiActor.gameObject);
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").bulletBank.GetBullet("default"));

                    base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("31a3ea0c54a745e182e22ea54844a82d").bulletBank.GetBullet("sniper"));

               }
               base.Fire(new Direction(0, Brave.BulletScript.DirectionType.Aim, -1f), new Speed(25f, SpeedType.Absolute), new SniperBullet());
                yield break;
            }
        }


        private class LightningBullet : Bullet
        {
            public LightningBullet(float direction, float sign, int maxRemainingBullets, int timeSinceLastTurn, Vector2? truePosition = null) : base("default", false, false, false)
            {
                this.m_direction = direction;
                this.m_sign = sign;
                this.m_maxRemainingBullets = maxRemainingBullets;
                this.m_timeSinceLastTurn = timeSinceLastTurn;
                this.m_truePosition = truePosition;
                this.SuppressVfx = true;
            }

            protected override IEnumerator Top()
            {
                yield return base.Wait(2);
                
                Vector2? truePosition = this.m_truePosition;
                if (truePosition == null)
                {
                    this.m_truePosition = new Vector2?(base.Position);
                }
                if (this.m_maxRemainingBullets > 0)
                {
                    if (this.m_timeSinceLastTurn > 0 && this.m_timeSinceLastTurn != 2 && this.m_timeSinceLastTurn != 3 && UnityEngine.Random.value < 0.2f)
                    {
                        this.m_sign *= -1f;
                        this.m_timeSinceLastTurn = 0;
                    }
                    float num = this.m_direction + this.m_sign * 30f;
                    Vector2 vector = this.m_truePosition.Value + BraveMathCollege.DegreesToVector(num, 0.8f);
                    Vector2 vector2 = vector + BraveMathCollege.DegreesToVector(num + 90f, UnityEngine.Random.Range(-0.3f, 0.3f));
                    if (!base.IsPointInTile(vector2))
                    {
                        LightningBullet lightningBullet = new LightningBullet(this.m_direction, this.m_sign, this.m_maxRemainingBullets - 1, this.m_timeSinceLastTurn + 1, new Vector2?(vector));
                        base.Fire(Offset.OverridePosition(vector2), lightningBullet);
                        if (lightningBullet.Projectile && lightningBullet.Projectile.specRigidbody && PhysicsEngine.Instance.OverlapCast(lightningBullet.Projectile.specRigidbody, null, true, false, null, null, false, null, null, new SpeculativeRigidbody[0]))
                        {
                            lightningBullet.Projectile.DieInAir(false, true, true, false);
                        }
                    }
                }
                yield return base.Wait(30);
                base.Vanish(true);
                
                yield break;
            }

            private float m_direction;

            private float m_sign;

            private int m_maxRemainingBullets;

            private int m_timeSinceLastTurn;

            private Vector2? m_truePosition;
            public const float Dist = 0.8f;

            public const int MaxBulletDepth = 30;

            public const float RandomOffset = 0.3f;

            public const float TurnChance = 0.2f;

            public const float TurnAngle = 30f;
        }
            public class SniperBullet : Bullet
        {
            public SniperBullet() : base("sniper", false, false, false)
            {
            }

            public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
            {
                if (preventSpawningProjectiles)
                {
                    return;
                }
                for(int i = 0; i < 12; i++)
				{
                    
                    if (BraveUtility.RandomBool())
                    {
                        base.Fire(new LightningBullet(i*30, -1f, 30, 4, null));
                    }
                    else
                    {
                        base.Fire(new LightningBullet(i*30, 1f, 30, 4, null));
                    }
                }
			}
            

            }
    }
}
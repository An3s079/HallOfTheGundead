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
using InControl;
using System.Reflection;
namespace HallOfGundead
{
    class succubus : AIActor
    {
        public static GameObject prefab;
        public static readonly string guid = "succubus";
        private static tk2dSpriteCollectionData SuccuColection;
        public static GameObject shootpoint;

        public static void Init()
        {
            succubus.BuildPrefab();
        }

        public static void BuildPrefab()
        {
            //
            prefab = EnemyBuilder.BuildPrefab("Succubus", guid, spritePaths[0], new IntVector2(0, 0), new IntVector2(8, 9), false);
            var companion = prefab.AddComponent<EnemyBehavior>();
            companion.aiActor.SetIsFlying(false, "idk lmao, it just cant. DO I NEED TO EXPLAIN MYSLEF TO YOU MUTHA*****?", true, true);
            companion.aiActor.knockbackDoer.weight = 50;
            companion.aiActor.MovementSpeed = 7f;
            companion.aiActor.healthHaver.PreventAllDamage = false;
            companion.aiActor.CollisionDamage = 1f;
            companion.aiActor.HasShadow = true;
            companion.aiActor.IgnoreForRoomClear = false;
            companion.aiActor.aiAnimator.HitReactChance = 0f;
            companion.aiActor.specRigidbody.CollideWithOthers = true;
            companion.aiActor.specRigidbody.CollideWithTileMap = true;
            companion.aiActor.PreventFallingInPitsEver = false;
            companion.aiActor.CollisionKnockbackStrength = 5f;
            companion.aiActor.healthHaver.SetHealthMaximum(20f, null, false);
            AIAnimator aiAnimator = companion.aiAnimator;
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

            bool flag3 = SuccuColection == null;
            if (flag3)
            {
                SuccuColection = ItemAPI.SpriteBuilder.ConstructCollection(prefab, "Succu_Collection");
                UnityEngine.Object.DontDestroyOnLoad(SuccuColection);
                for (int i = 0; i < spritePaths.Length; i++)
                {
                    ItemAPI.SpriteBuilder.AddSpriteToCollection(spritePaths[i], SuccuColection);
                }
                ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, SuccuColection, new List<int>
                    {


                    0

                    }, "idle_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 5f;
                ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, SuccuColection, new List<int>
                    {

                    9

                    }, "idle_right", tk2dSpriteAnimationClip.WrapMode.Once).fps = 5f;
                ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, SuccuColection, new List<int>
                    {

                    0,1,2,3,4,5,6,7,8
                    }, "run_left", tk2dSpriteAnimationClip.WrapMode.Once).fps = 10f;
                ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, SuccuColection, new List<int>
                    {
                    9,10,11,12,13,14,15,16,17

                    }, "run_right", tk2dSpriteAnimationClip.WrapMode.Once).fps = 10f;
                ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, SuccuColection, new List<int>
                    {



                    0,


                    }, "die_right", tk2dSpriteAnimationClip.WrapMode.Once).fps = 5f;
                ItemAPI.SpriteBuilder.AddAnimation(companion.spriteAnimator, SuccuColection, new List<int>
                    {
                    0,

                    }, "die_left", tk2dSpriteAnimationClip.WrapMode.Once).fps = 5f;

            }
            var bs = prefab.GetComponent<BehaviorSpeculator>();
            float[] angles = { 45, 135, 225, 135 };
            prefab.GetOrAddComponent<ObjectVisibilityManager>();
            BehaviorSpeculator behaviorSpeculator = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").behaviorSpeculator;
            bs.OverrideBehaviors = behaviorSpeculator.OverrideBehaviors;
            bs.OtherBehaviors = behaviorSpeculator.OtherBehaviors;
            shootpoint = new GameObject("shootpointSuccu");
            shootpoint.transform.parent = companion.transform;
            shootpoint.transform.position = companion.sprite.WorldCenter;
            GameObject m_CachedGunAttachPoint = companion.transform.Find("shootpointSuccu").gameObject;
			bs.TargetBehaviors = new List<TargetBehaviorBase>
			{
                new TargetPlayerBehavior
				{
                    LineOfSight = true,
                    ObjectPermanence = true,
				}
			};

            bs.AttackBehaviors = new List<AttackBehaviorBase>() {
            new ShootBehavior
            {
                ShootPoint = m_CachedGunAttachPoint,
                BulletScript = new CustomBulletScriptSelector(typeof(SuccuScript)),
                LeadAmount = 0f,
                AttackCooldown = 1f,
                RequiresLineOfSight = false,
                Uninterruptible = true
            },
        };
            
            bs.MovementBehaviors = new List<MovementBehaviorBase>
            {
                new MoveErraticallyBehavior
                {

                    PathInterval = 2f,
                    AvoidTarget = true,
                    StayOnScreen = false,
                    InitialDelay = 0,
                    PreventFiringWhileMoving = true,
                    PointReachedPauseTime = 1         
                }
            };
            
            aiAnimator.facingType = AIAnimator.FacingType.Movement;
            bs.InstantFirstTick = behaviorSpeculator.InstantFirstTick;
            bs.TickInterval = behaviorSpeculator.TickInterval;
            bs.PostAwakenDelay = behaviorSpeculator.PostAwakenDelay;
            bs.RemoveDelayOnReinforce = behaviorSpeculator.RemoveDelayOnReinforce;
            bs.OverrideStartingFacingDirection = behaviorSpeculator.OverrideStartingFacingDirection;
            bs.StartingFacingDirection = behaviorSpeculator.StartingFacingDirection;
            bs.SkipTimingDifferentiator = behaviorSpeculator.SkipTimingDifferentiator;
            Game.Enemies.Add("hotg:succubus", companion.aiActor);
        }
        public static GameObject yah;

        public static float distortionMaxRadius = 20;

        public static float distortionDuration = 3f;
        public static float distortionIntensity = 0.5f;
        public static float distortionThickness = 0.04f;
        private static Vector2 m_distortionCenter;

        public class SuccuScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
        {
          
            protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
            {
                AkSoundEngine.PostEvent("Play_WPN_stickycrossbow_shot_01", this.BulletBank.aiActor.gameObject);
                m_distortionCenter = base.BulletBank.aiActor.sprite.WorldCenter;
                float m_prevWaveDist = 0f;
                Exploder.DoDistortionWave(m_distortionCenter, distortionIntensity, distortionThickness, distortionMaxRadius, distortionDuration);
                float waveRemaining = distortionDuration - BraveTime.DeltaTime;
                while (waveRemaining > 0f)
                {
                    waveRemaining -= BraveTime.DeltaTime;
                    float waveDist = BraveMathCollege.LinearToSmoothStepInterpolate(0f, distortionMaxRadius, 1f - waveRemaining / distortionDuration);
                    for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++)
                    {
                        PlayerController playerController = GameManager.Instance.AllPlayers[i];
                        if (!playerController.healthHaver.IsDead)
                        {
                            if (!playerController.spriteAnimator.QueryInvulnerabilityFrame() && playerController.healthHaver.IsVulnerable)
                            {
                                Vector2 unitCenter = playerController.specRigidbody.GetUnitCenter(ColliderType.HitBox);
                                float num = Vector2.Distance(unitCenter, m_distortionCenter);
                                if (num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f)
                                {
                                    float b = (unitCenter - m_distortionCenter).ToAngle();
                                    if (BraveMathCollege.AbsAngleBetween(playerController.FacingDirection, b) >= 60f)
                                    {
                                        if (playerController == GameManager.Instance.PrimaryPlayer && IsAlreadyReversedP1 == false)
                                            ReverseControlsP1(4);
                                        if (playerController == GameManager.Instance.SecondaryPlayer && IsAlreadyReversedP2 == false)
                                            ReverseControlsP2(4);
                                        
                                    }
                                }
                            }
                        }
                    }
                    m_prevWaveDist = waveDist;
                    yield return null;
                }
            }

        }
        private static bool IsAlreadyReversedP1 = false;
        private static bool IsAlreadyReversedP2 = false;

        private static BindingSource[] ActionDownValueP1 = new BindingSource[4];
        private static BindingSource[] ActionUpValueP1 = new BindingSource[4];
        private static BindingSource[] ActionRightValueP1 = new BindingSource[4];
        private static BindingSource[] ActionLeftValueP1 = new BindingSource[4];
        private static BindingSource[] ActionDownValueP2 = new BindingSource[4];
        private static BindingSource[] ActionUpValueP2 = new BindingSource[4];
        private static BindingSource[] ActionRightValueP2 = new BindingSource[4];
        private static BindingSource[] ActionLeftValueP2 = new BindingSource[4];
        public static void ReverseControlsP1(float AmountOfTimeControlsReversed)
        {
            
            GungeonActions actions = BraveInput.PrimaryPlayerInstance.ActiveActions;
            var ActionLeft = actions.Left.Bindings;
            for(int i = 0; i < ActionLeft.Count; i++)
                ActionLeftValueP1[i] = ActionLeft[i];
            var ActionRight = actions.Right.Bindings;
            for (int i = 0; i < ActionRight.Count; i++)
                ActionRightValueP1[i] = ActionRight[i];
            var ActionUp = actions.Up.Bindings;
            for (int i = 0; i < ActionUp.Count; i++)
                ActionUpValueP1[i] = ActionUp[i];
            var ActionDown = actions.Down.Bindings;
            for (int i = 0; i < ActionDown.Count; i++)
                ActionDownValueP1[i] = ActionDown[i];

            actions.Left.ClearBindings();
            actions.Up.ClearBindings();
            actions.Down.ClearBindings();
            actions.Right.ClearBindings();

            for (int i = 0; i < ActionRightValueP1.Length; i++)
                actions.Left.AddBinding(ActionRightValueP1[i]);
            for (int i = 0; i < ActionLeftValueP1.Length; i++)
                actions.Right.AddBinding(ActionLeftValueP1[i]);
            for (int i = 0; i < ActionUpValueP1.Length; i++)
                actions.Down.AddBinding(ActionUpValueP1[i]);
            for (int i = 0; i < ActionDownValueP1.Length; i++)
                actions.Up.AddBinding(ActionDownValueP1[i]);

            IsAlreadyReversedP1 = true;
            GameManager.Instance.StartCoroutine(FixControlsP1(AmountOfTimeControlsReversed));
        }
        private static void ReverseControlsP2(float AmountOfTimeControlsReversed)
        {
            GungeonActions actions = BraveInput.SecondaryPlayerInstance.ActiveActions;
            var ActionLeft = actions.Left.Bindings;
            for (int i = 0; i < ActionLeft.Count; i++)
                ActionLeftValueP2[i] = ActionLeft[i];
            var ActionRight = actions.Right.Bindings;
            for (int i = 0; i < ActionRight.Count; i++)
                ActionRightValueP2[i] = ActionRight[i];
            var ActionUp = actions.Up.Bindings;
            for (int i = 0; i < ActionUp.Count; i++)
                ActionUpValueP2[i] = ActionUp[i];
            var ActionDown = actions.Down.Bindings;
            for (int i = 0; i < ActionDown.Count; i++)
                ActionDownValueP2[i] = ActionDown[i];

            actions.Left.ClearBindings();
            actions.Up.ClearBindings();
            actions.Down.ClearBindings();
            actions.Right.ClearBindings();

            for (int i = 0; i < ActionRightValueP2.Length; i++)
                actions.Left.AddBinding(ActionRightValueP2[i]);
            for (int i = 0; i < ActionLeftValueP2.Length; i++)
                actions.Right.AddBinding(ActionLeftValueP2[i]);
            for (int i = 0; i < ActionUpValueP2.Length; i++)
                actions.Down.AddBinding(ActionUpValueP2[i]);
            for (int i = 0; i < ActionDownValueP2.Length; i++)
                actions.Up.AddBinding(ActionDownValueP2[i]);

            IsAlreadyReversedP2 = true;
            GameManager.Instance.StartCoroutine(FixControlsP2(AmountOfTimeControlsReversed));
        }
        private static IEnumerator FixControlsP1(float SecondsToWait)
        {
            yield return new WaitForSeconds(SecondsToWait);
            GungeonActions actions = BraveInput.PrimaryPlayerInstance.ActiveActions;
            actions.Left.ClearBindings();
            actions.Up.ClearBindings();
            actions.Down.ClearBindings();
            actions.Right.ClearBindings();

            for (int i = 0; i < ActionLeftValueP1.Length; i++)
                actions.Left.AddBinding(ActionLeftValueP1[i]);
            for (int i = 0; i < ActionRightValueP1.Length; i++)
                actions.Right.AddBinding(ActionRightValueP1[i]);
            for (int i = 0; i < ActionDownValueP1.Length; i++)
                actions.Down.AddBinding(ActionDownValueP1[i]);
            for (int i = 0; i < ActionUpValueP1.Length; i++)
                actions.Up.AddBinding(ActionUpValueP1[i]);

            IsAlreadyReversedP1 = false;
        }

        private static IEnumerator FixControlsP2(float SecondsToWait)
        {
            yield return new WaitForSeconds(SecondsToWait);
            GungeonActions actions = BraveInput.SecondaryPlayerInstance.ActiveActions;
            actions.Left.ClearBindings();
            actions.Up.ClearBindings();
            actions.Down.ClearBindings();
            actions.Right.ClearBindings();

            for (int i = 0; i < ActionLeftValueP2.Length; i++)
                actions.Left.AddBinding(ActionLeftValueP2[i]);
            for (int i = 0; i < ActionRightValueP2.Length; i++)
                actions.Right.AddBinding(ActionRightValueP2[i]);
            for (int i = 0; i < ActionDownValueP2.Length; i++)
                actions.Down.AddBinding(ActionDownValueP2[i]);
            for (int i = 0; i < ActionUpValueP2.Length; i++)
                actions.Up.AddBinding(ActionUpValueP2[i]);
            IsAlreadyReversedP2 = false;
        }
        
        // Token: 0x04003FCA RID: 16330
        private float m_timer;

        // Token: 0x04003FCB RID: 16331
        private float m_prevWaveDist;

        private static string[] spritePaths = new string[]
        {
			
			//run left
			"HallOfGundead/Resources/Succu/Succu_run_left_001",
			"HallOfGundead/Resources/Succu/Succu_run_left_002",
			"HallOfGundead/Resources/Succu/Succu_run_left_003",
			"HallOfGundead/Resources/Succu/Succu_run_left_004",
			"HallOfGundead/Resources/Succu/Succu_run_left_005",
			"HallOfGundead/Resources/Succu/Succu_run_left_006",
			"HallOfGundead/Resources/Succu/Succu_run_left_007",
			"HallOfGundead/Resources/Succu/Succu_run_left_008",
			"HallOfGundead/Resources/Succu/Succu_run_left_009",

            //run right
            "HallOfGundead/Resources/Succu/Succu_run_right_001",
            "HallOfGundead/Resources/Succu/Succu_run_right_002",
            "HallOfGundead/Resources/Succu/Succu_run_right_003",
            "HallOfGundead/Resources/Succu/Succu_run_right_004",
            "HallOfGundead/Resources/Succu/Succu_run_right_005",
            "HallOfGundead/Resources/Succu/Succu_run_right_006",
            "HallOfGundead/Resources/Succu/Succu_run_right_007",
            "HallOfGundead/Resources/Succu/Succu_run_right_008",
            "HallOfGundead/Resources/Succu/Succu_run_right_009",
                };

        public class EnemyBehavior : BraveBehaviour
        {

            private RoomHandler m_StartRoom;

            float cooldowntime = 1;
            public void FixedUpdate()
			{
                if (Time.time > cooldowntime && base.aiActor.Velocity.x <= 0 && base.aiActor.Velocity.y <= 0)
                {
                    cooldowntime = Time.time + 2;
                    
                }
			}
            private void Update()
            {
                
                if (!base.aiActor.HasBeenEngaged) { CheckPlayerRoom(); }
                
                if (GameManager.Instance.PrimaryPlayer != null && GameManager.Instance.PrimaryPlayer.healthHaver.GetCurrentHealth() <= 0 && IsAlreadyReversedP1 == true)
                    FixControlsP1(0);
                if (GameManager.Instance.SecondaryPlayer != null && GameManager.Instance.SecondaryPlayer.healthHaver.GetCurrentHealth() <= 0 && IsAlreadyReversedP2 == true)
                    FixControlsP2(0);
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
                yield return new WaitForSeconds(2f);
                base.aiActor.HasBeenEngaged = true;
                yield break;
            }
            private void Start()
            {
                cooldowntime = Time.time + 1;
               
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

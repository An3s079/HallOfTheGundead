using Dungeonator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HallOfGundead
{
	class BigPompEntranceController: BraveBehaviour, IPlaceConfigurable
	{
        public BigPompEntranceController()
        {
            targetLevelName = "tt_hall";
            PitOffset = new IntVector2(5, 2);
            m_Triggered = false;
            m_Destroyed = false;
        }

        public string targetLevelName;
        public IntVector2 PitOffset;

        private bool m_Triggered;
        private bool m_Destroyed;

        private RoomHandler m_ParentRoom;

        private IEnumerator Start()
        {
            yield return null;
            while (GameManager.Instance.IsLoadingLevel && Dungeon.IsGenerating) { yield return null; }
            yield return null;

            //IntVector2 baseCellPosition = (transform.position.IntXY(VectorConversions.Floor) + new IntVector2(4, 1.5));

            // specRigidbody.OnHitByBeam = (Action<BasicBeamController>)Delegate.Combine(specRigidbody.OnHitByBeam, new Action<BasicBeamController>(HandleBeamCollision));
            GameObject PitManager = new GameObject("Hall Pit Manager") { layer = 0 };
            PitManager.transform.position = (transform.position + new Vector3(5, 1.5f));
            tk2dSprite PitDummySprite = PitManager.AddComponent<tk2dSprite>();
            //Toolbox.DuplicateSprite(PitDummySprite, null);
            tk2dSprite pitSprite = PitManager.GetComponent<tk2dSprite>();
            pitSprite.renderer.enabled = false;

            Toolbox.GenerateOrAddToRigidBody(PitManager, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, dimensions: new IntVector2(2, 2));

            BigPompPitController HallPitManager = PitManager.AddComponent<BigPompPitController>();
            HallPitManager.targetLevelName = targetLevelName;
            yield break;
        }

        


        /*private void HandleBeamCollision(BasicBeamController obj) {
            GoopModifier component = obj.GetComponent<GoopModifier>();
            if (component && component.goopDefinition != null && component.goopDefinition.CanBeIgnited && component.goopDefinition.fireEffect != null) { OnFireStarted(); }
        }*/

        public void ConfigureOnPlacement(RoomHandler room)
        {
            m_ParentRoom = room;

            Minimap.Instance.RegisterRoomIcon(m_ParentRoom, HallPrefabs.BigPomp_Icon, false);

            IntVector2 basePosition = (transform.position.IntXY(VectorConversions.Floor) + PitOffset);
            IntVector2 cellPos = basePosition;
            IntVector2 cellPos2 = (basePosition + new IntVector2(1, 0));
            IntVector2 cellPos3 = (basePosition + new IntVector2(1, 1));
            IntVector2 cellPos4 = (basePosition + new IntVector2(0, 1));
            CellData cellData = GameManager.Instance.Dungeon.data[cellPos];
            CellData cellData2 = GameManager.Instance.Dungeon.data[cellPos2];
            CellData cellData3 = GameManager.Instance.Dungeon.data[cellPos3];
            CellData cellData4 = GameManager.Instance.Dungeon.data[cellPos4];

            cellData.type = CellType.PIT;
            cellData2.type = CellType.PIT;
            cellData3.type = CellType.PIT;
            cellData4.type = CellType.PIT;

            cellData.forceAllowGoop = false;
            cellData2.forceAllowGoop = false;
            cellData3.forceAllowGoop = false;
            cellData4.forceAllowGoop = false;

            cellData.fallingPrevented = false;
            cellData2.fallingPrevented = false;
            cellData3.fallingPrevented = false;
            cellData4.fallingPrevented = false;
        }

        private void Update() { }
        private void LateUpdate() { }

        protected override void OnDestroy()
        {
            m_Destroyed = true;
            base.OnDestroy();
        }
    }

    public class BigPompPitController : DungeonPlaceableBehaviour, IPlaceConfigurable
    {

        public BigPompPitController() { targetLevelName = "tt_hall"; }

        public string targetLevelName;

        private void Start()
        {
            var i = HallPrefabs.Hall_BigPomp.GetComponent<tk2dSpriteAnimator>();
                i.Play();


            //var shadow = Instantiate(HallPrefabs.Hall_BigPompShadow);
            //shadow.transform.position = new Vector3(HallPrefabs.Hall_BigPomp.transform.position.x, HallPrefabs.Hall_BigPomp.transform.position.y, HallPrefabs.Hall_BigPomp.transform.position.z - 3);

            SpeculativeRigidbody Rigidbody = specRigidbody;
            Rigidbody.OnEnterTrigger = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(Rigidbody.OnEnterTrigger, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerEntered));
            SpeculativeRigidbody Rigidbody2 = specRigidbody;
            Rigidbody2.OnExitTrigger = (SpeculativeRigidbody.OnTriggerExitDelegate)Delegate.Combine(Rigidbody2.OnExitTrigger, new SpeculativeRigidbody.OnTriggerExitDelegate(HandleTriggerExited));
        }

        private void HandleTriggerEntered(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData)
        {
            PlayerController component = specRigidbody.GetComponent<PlayerController>();
            if (component) { component.LevelToLoadOnPitfall = targetLevelName; }
        }

        private void HandleTriggerExited(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody)
        {
            PlayerController component = specRigidbody.GetComponent<PlayerController>();
            if (component) { component.LevelToLoadOnPitfall = string.Empty; }
        }

        public void ConfigureOnPlacement(RoomHandler room) { }

        private void Update() {
            

        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

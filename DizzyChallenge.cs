using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChallengeAPI;
using UnityEngine;
using Dungeonator;
using InControl;
using System.Collections;
namespace HallOfGundead
{
    class DizzyChallenge : ChallengeModifier
    {
        public void Start()
        {
            for (int i = 0; i < GameManager.Instance.AllPlayers.Length; i++)
            {
                PlayerController playerController = GameManager.Instance.AllPlayers[i];
                if (playerController == GameManager.Instance.PrimaryPlayer)
                {
                    GungeonActions actions = BraveInput.PrimaryPlayerInstance.ActiveActions;
                    var ActionLeft = actions.Left.Bindings;
                    for (int j = 0; j < ActionLeft.Count; j++)
                        ActionLeftValueP1[j] = ActionLeft[j];
                    var ActionRight = actions.Right.Bindings;
                    for (int j = 0; j < ActionRight.Count; j++)
                        ActionRightValueP1[j] = ActionRight[j];
                    var ActionUp = actions.Up.Bindings;
                    for (int j = 0; j < ActionUp.Count; j++)
                        ActionUpValueP1[j] = ActionUp[j];
                    var ActionDown = actions.Down.Bindings;
                    for (int j = 0; j < ActionDown.Count; j++)
                        ActionDownValueP1[j] = ActionDown[j];
                }
                if (playerController == GameManager.Instance.SecondaryPlayer)
                {
                    GungeonActions actions = BraveInput.PrimaryPlayerInstance.ActiveActions;
                    var ActionLeft = actions.Left.Bindings;
                    for (int ij = 0; ij < ActionLeft.Count; ij++)
                        ActionLeftValueP2[ij] = ActionLeft[ij];
                    var ActionRight = actions.Right.Bindings;
                    for (int ij = 0; ij < ActionRight.Count; ij++)
                        ActionRightValueP2[ij] = ActionRight[ij];
                    var ActionUp = actions.Up.Bindings;
                    for (int ij = 0; ij < ActionUp.Count; ij++)
                        ActionUpValueP2[ij] = ActionUp[ij];
                    var ActionDown = actions.Down.Bindings;
                    for (int ij = 0; ij < ActionDown.Count; ij++)
                        ActionDownValueP2[ij] = ActionDown[ij];
                }
            }
       }

        public void Update()
        {
            if(GameManager.Instance.PrimaryPlayer != null)
                ReverseControlsP1();
            if (GameManager.Instance.SecondaryPlayer != null)
                ReverseControlsP2();

            if (GameManager.Instance.PrimaryPlayer != null && GameManager.Instance.PrimaryPlayer.healthHaver.GetCurrentHealth() <= 0)
                FixControlsP1();
            if (GameManager.Instance.SecondaryPlayer != null && GameManager.Instance.SecondaryPlayer.healthHaver.GetCurrentHealth() <= 0)
                FixControlsP2();
        }

        private BindingSource[] ActionDownValueP1 = new BindingSource[4];
        private BindingSource[] ActionUpValueP1 = new BindingSource[4];
        private BindingSource[] ActionRightValueP1 = new BindingSource[4];
        private BindingSource[] ActionLeftValueP1 = new BindingSource[4];
        private BindingSource[] ActionDownValueP2 = new BindingSource[4];
        private BindingSource[] ActionUpValueP2 = new BindingSource[4];
        private BindingSource[] ActionRightValueP2 = new BindingSource[4];
        private BindingSource[] ActionLeftValueP2 = new BindingSource[4];
        public void ReverseControlsP1()
        {

            GungeonActions actions = BraveInput.PrimaryPlayerInstance.ActiveActions;

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

        }
        private void ReverseControlsP2()
        {
            GungeonActions actions = BraveInput.SecondaryPlayerInstance.ActiveActions;

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

        }

        private void FixControlsP1()
        {
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

        }

        private void FixControlsP2()
        {
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
        }
        public void OnDestroy()
        {
            if (GameManager.Instance.PrimaryPlayer != null)
                FixControlsP1();
            if (GameManager.Instance.SecondaryPlayer != null)
                FixControlsP2();
        }

        public override bool IsValid(RoomHandler room)
        {
            //This method checks if the room is valid for this challenge. If you return true it means it's valid, if you return false it means it's not valid.
            return true;
        }
    }
}

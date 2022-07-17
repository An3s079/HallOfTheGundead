using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using Dungeonator;
using System.Collections;

namespace ItemAPI
{
    static class PlayerEventAdder
    {

        public static void Init()
        {
            Hook hook = new Hook(typeof(PlayerController).GetMethod("orig_Start", BindingFlags.Public | BindingFlags.Instance), typeof(PlayerEventAdder).GetMethod("AddComponent"));
            Hook hook2 = new Hook(typeof(RoomHandler).GetMethod("PlayerEnter", BindingFlags.Public | BindingFlags.Instance), typeof(FuckyEvents).GetMethod("OnRoomEnter"));
        }

        public static void AddComponent(Action<PlayerController> action, PlayerController player)
        {
            action(player);
            player.gameObject.AddComponent<FuckyEvents>();
        }
    }


    public class FuckyEvents : MonoBehaviour
    {
        public event Action<PlayerController, RoomHandler> OnEnterAnyRoom;

        public event Action<int> OnCasingsChanged;

        public static void OnRoomEnter(Action<RoomHandler, PlayerController> action, RoomHandler room, PlayerController player)
        {
            action(room, player);
            player.GetComponent<FuckyEvents>().OnEnterAnyRoom?.Invoke(player, room);
        }


        public static PlayerController GetPlayerFromConsumables(PlayerConsumables consumables)
        {
            if (GameManager.HasInstance && GameManager.Instance.AllPlayers != null)
            {
                return Array.Find(GameManager.Instance.AllPlayers, (PlayerController player) => player.carriedConsumables == consumables);
            }
            return null;
        }
    }
}

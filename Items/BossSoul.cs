using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace HallOfGundead
{
    class BossSoul : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Boss Soul";
            string resourceName = "HallOfGundead/Resources/BossSoul";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<BossSoul>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "your power grows";
            string longDesc = "If you are seeing this i did something wrong, lemme know.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "hotg");
            item.quality = PickupObject.ItemQuality.EXCLUDED;
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);

            if (player.HasPickupID(SoulEater.SoulEaterID))
            {
                SoulEater.BossSoulsCollected++;
                player.RemoveItemFromInventory(this);
            }

        }
    }
}

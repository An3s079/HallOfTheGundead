using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace HallOfGundead
{
    class NonBossSoul : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Non Boss Soul";
            string resourceName = "HallOfGundead/Resources/NonBossSoul";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<NonBossSoul>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "one step closer";
            string longDesc = "If you are seeing this i did something wrong, lemme know.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "hotg");
            item.quality = PickupObject.ItemQuality.EXCLUDED;
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);

            if(player.HasPickupID(SoulEater.SoulEaterID))
            {
                SoulEater.nonBossSoulsCollected++;
                player.RemoveItemFromInventory(this);
            }

        }
    }
}

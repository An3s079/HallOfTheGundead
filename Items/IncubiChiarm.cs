using ItemAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HallOfGundead
{
    class IncubiChiarm
    {
        public static void Init()
        {
            string itemName = "Incubi's Charm";
            string resourceName = "HallOfGundead/Resources/Charm";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<NonBossSoul>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Drop Dead Looks!";
            string longDesc = "It is unknown how it was possible to store the charm of an Incubus in an item, yet " +
                " the scientists of the gungeon seem to have done it.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "hotg");
            item.quality = PickupObject.ItemQuality.D;
        }
    }
}

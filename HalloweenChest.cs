using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChestAPI;
using UnityEngine;
namespace HallOfGundead
{
    class HalloweenChest
    {
        public static Chest PompChest;
        private static List<string> pompChestCollection = new List<string>()
        {
        "HallOfGundead/Resources/pomp_chest/pomp_chest_open_001",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_open_002",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_open_003",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_open_004",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_open_005",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_appear_001",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_appear_002",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_appear_003",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_appear_004",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_appear_005",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_break_001",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_break_002",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_break_003",
        "HallOfGundead/Resources/pomp_chest/pomp_chest_break_004",
        };
        public static void Init()
        {
             PompChest = ChestBuilder.CreateChest("HallOfGundead/Resources/pomp_chest/pomp_chest", "Halloween Pumpkin Chest", new IntVector2(0,0), new IntVector2(200, 200), pompChestCollection, FLoorModModule.itemandWeight, 4, 9, 40, 37, 10, ChestBuilder.ChestType.Unspecified, true, null);
            PompChest.IsLocked = true;
        }
    }
}

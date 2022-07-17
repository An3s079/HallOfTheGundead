using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;
using UnityEngine;
using MonoMod;
namespace HallOfGundead
{
    class GoopLibrary
    {
        public static GoopDefinition PumpkinGuts = new GoopDefinition()
        {
            CanBeIgnited = false,
            damagesEnemies = false,
            damagesPlayers = false,
            baseColor32 = new Color32(230, 118, 32, 200),
            goopTexture = ResourceExtractor.GetTextureFromResource("HallOfGundead/Resources/goop_standard_base_001.png"),
            AppliesDamageOverTime = false,
        };
        
    }
}

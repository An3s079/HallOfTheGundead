using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dungeonator;
using GungeonAPI;

namespace HallOfGundead
{
	class HallRoomPrefabs : MonoBehaviour
	{
		public static PrototypeDungeonRoom Hollow_Pumpkin_Room;

        public static PrototypeDungeonRoom Hall_Entrance_Room;
        public static PrototypeDungeonRoom Hall_Exit_Room;
        public static PrototypeDungeonRoom[] Hall_Rooms;
        public static PrototypeDungeonRoom Hall_Boss;
        public static List<string> Hall_RoomList;

        

        public static void InitCustomRooms()
		{
            Hall_RoomList = new List<string>() {
                "hall_acrossthepond.room",                                              
"hall_aktranken.room",                                                  
"hall_amethystshard.room",
"hall_askew.room",
"hall_battrow.room",
"hall_bombard.room",
"hall_boneangle.room",
"hall_boneanza.room",
"hall_bonegrindmachine.room",
"hall_boneworks.room",
"hall_brokenleg.room",
"hall_cellsofabyss.room",
"hall_chambersofevil.room",
"hall_cheeseslice.room",
"hall_coffinslide.room",
"hall_crookedhouse.room",
"hall_crooksakimbo.room",
"hall_crossyourheartandhopetodie.room",
"hall_cursedroundabout.room",
"hall_cursesuponye.room",
"hall_curvyandgorgeous.room",
"hall_darkmachinations.room",
"hall_darkritual.room",
"hall_deadroom.room",
"hall_dembones.room",
"hall_diagonalley.room",
"hall_dodgethis.room",
"hall_fantasyisland.room",
"hall_funnel.room",
"hall_ghostnook.room",
"hall_guardianspirit.room",
"hall_hallofconfusion.room",
"hall_hauntedhalls.room",
"hall_hauntedmansion.room",
"hall_intestine.room",
"hall_islasnipa.room",
"hall_ithinkthereforeijam.room",
"hall_itsalive.room",
"hall_jammedwitchgobrr.room",
"hall_joinusinthespiral.room",
"hall_jumpabout.room",
"hall_lakeoffire.room",
"hall_lostskull.room",
"hall_massdelusion.room",
"hall_massiveturd.room",
"hall_minecraftcavenoise.room",
"hall_morgue.room",
"hall_mountainsofmadness.room",
"hall_nowitsjustyouandme.room",
"hall_ofthepumpking.room",
"hall_pumpkavern.room",
"hall_pumpkinfeast.room",
"hall_redrum.room",
"hall_roadtonowhere.room",
"hall_roughtimes.room",
"hall_separatedtranks.room",
"hall_shard.room",
"hall_shutzmidikov.room",
"hall_spiderproblem.room",
"hall_spookyscaryskelletons.room",
"hall_stuckinthemiddlewithyou.room",
"hall_succmedaddy.room",
"hall_succywuccy.room",
"hall_summoningroom.room",
"hall_thatonepixelartroom.room",
"hall_thegummystarringbrengunfrasier.room",
"hall_theiris.room",
"hall_themusictheymake.room",
"hall_thepitofdespair.room",
"hall_thepyramid.room",
"hall_theraveyard.room",
"hall_torturechamber.room",
"hall_towerofterror.room",
"hall_twolillads.room",
"hall_ultrapump.room",
"hall_waitingfortheworms.room",
"hall_whatevenisthis.room",
"hall_whichwitch.room",
"hall_witchbitch.room",
"hall_youcanrun.room",
"hall_youhaveangeredthehorde.room",
            };
            Hollow_Pumpkin_Room = RoomFactory.BuildFromResource("HallOfGundead/Resources/HallOfGundeadRooms/Hollow_Pumpkin_Room.room");
            Hollow_Pumpkin_Room.overrideRoomVisualType = 6;
            Hollow_Pumpkin_Room.allowFloorDecoration = false;
            Hollow_Pumpkin_Room.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            RoomBuilder.AddObjectToRoom(Hollow_Pumpkin_Room, new Vector2(3.5f, 12), Toolbox.GenerateDungeonPlacable(HallPrefabs.Hall_BigPomp, useExternalPrefab: true));

            Hall_Entrance_Room = RoomFactory.BuildFromResource("HallOfGundead/Resources/HallOfGundeadRooms/GraveYardEntrance.room");
            Hall_Exit_Room = RoomFactory.BuildFromResource("HallOfGundead/Resources/HallOfGundeadRooms/GraveYardExit.room");
            Hall_Entrance_Room.category = PrototypeDungeonRoom.RoomCategory.ENTRANCE;
            Hall_Exit_Room.category = PrototypeDungeonRoom.RoomCategory.EXIT;
            List<PrototypeDungeonRoom> m_HallRooms = new List<PrototypeDungeonRoom>();

            foreach (string name in Hall_RoomList)
            {
                PrototypeDungeonRoom m_room = RoomFactory.BuildFromResource("HallOfGundead/Resources/HallOfGundeadRooms/" + name);
                m_HallRooms.Add(m_room);
            }

            // Expand_Jungle_Rooms = ExpandUtility.BuildRoomArrayFromTextFile("Textures/RoomLayoutData/RoomFactoryRooms/Jungle/Jungle_RoomEntries.txt");
            Hall_Rooms = m_HallRooms.ToArray();

            foreach (PrototypeDungeonRoom room in Hall_Rooms)
            {
                HallPrefabs.HallRoomTable.includedRooms.elements.Add(GenerateWeightedRoom(room, 1));
            }

            Hall_Boss = RoomFactory.BuildFromResource("HallOfGundead/Resources/VampireBossRoom.room");
            Hall_Boss.category = PrototypeDungeonRoom.RoomCategory.BOSS;
            Hall_Boss.subCategoryBoss = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS;
            Hall_Boss.subCategoryNormal = PrototypeDungeonRoom.RoomNormalSubCategory.COMBAT;
            Hall_Boss.subCategorySpecial = PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
            Hall_Boss.subCategorySecret = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET;
            Hall_Boss.roomEvents = new List<RoomEventDefinition>() {
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENTER_WITH_ENEMIES, RoomEventTriggerAction.SEAL_ROOM),
                new RoomEventDefinition(RoomEventTriggerCondition.ON_ENEMIES_CLEARED, RoomEventTriggerAction.UNSEAL_ROOM),
            };
            Hall_Boss.associatedMinimapIcon = HallPrefabs.doublebeholsterroom01.associatedMinimapIcon;
            Hall_Boss.usesProceduralLighting = false;
            Hall_Boss.usesProceduralDecoration = false;
            Hall_Boss.rewardChestSpawnPosition = new IntVector2(25, 20);
            Hall_Boss.overriddenTilesets = GlobalDungeonData.ValidTilesets.JUNGLEGEON;
            foreach (PrototypeRoomExit exit in Hall_Boss.exitData.exits) { exit.exitType = PrototypeRoomExit.ExitType.ENTRANCE_ONLY; }
                RoomBuilder.AddExitToRoom(Hall_Boss, new Vector2(26, 37), DungeonData.Direction.NORTH, PrototypeRoomExit.ExitType.EXIT_ONLY, PrototypeRoomExit.ExitGroup.B);
        }

        public static WeightedRoom GenerateWeightedRoom(PrototypeDungeonRoom Room, float Weight = 1, bool LimitedCopies = true, int MaxCopies = 1, DungeonPrerequisite[] AdditionalPrerequisites = null)
        {
            if (Room == null) { return null; }
            if (AdditionalPrerequisites == null) { AdditionalPrerequisites = new DungeonPrerequisite[0]; }
            return new WeightedRoom() { room = Room, weight = Weight, limitedCopies = LimitedCopies, maxCopies = MaxCopies, additionalPrerequisites = AdditionalPrerequisites };
        }
    }
}

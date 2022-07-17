using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace HallOfGundead
{
	public class HallDungeonCollections : MonoBehaviour
	{
        public static tk2dSpriteCollectionData ENV_Tileset_Hall(GameObject TargetObject, AssetBundle sharedAssets)
        {

            Material m_LitCutout = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTk2dCustomFalloffCutout"));
            m_LitCutout.mainTexture = HallPrefabs.ENV_Tileset_Hall_Texture;
            m_LitCutout.SetFloat("_Cutoff", 0.5f);
            m_LitCutout.SetFloat("_MaxValue", 1);
            m_LitCutout.SetFloat("_Perpendicular", 1);

            Material m_LitBlend = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTK2dCustomFalloff"));
            m_LitBlend.mainTexture = HallPrefabs.ENV_Tileset_Hall_Texture;
            m_LitBlend.SetFloat("_Cutoff", 0.5f);
            m_LitBlend.SetFloat("_Perpendicular", 1);

            Material m_UnlitCutout = new Material(sharedAssets.LoadAsset<Shader>("BraveUnlitCutout"));
            m_UnlitCutout.mainTexture = HallPrefabs.ENV_Tileset_Hall_Texture;
            m_UnlitCutout.SetFloat("_Cutoff", 0.5f);
            m_UnlitCutout.SetFloat("_Perpendicular", 1);


            IndexNeighborDependency m_neighborDependency1 = new IndexNeighborDependency()
            {
                neighborDirection = Dungeonator.DungeonData.Direction.NORTH,
                neighborIndex = 22
            };
            IndexNeighborDependency m_neighborDependency2 = new IndexNeighborDependency()
            {
                neighborDirection = Dungeonator.DungeonData.Direction.NORTH,
                neighborIndex = 26
            };
            IndexNeighborDependency m_neighborDependency3 = new IndexNeighborDependency()
            {
                neighborDirection = Dungeonator.DungeonData.Direction.NORTH,
                neighborIndex = 30
            };

            tk2dSpriteCollectionData m_NewDungeonCollection = TargetObject.AddComponent<tk2dSpriteCollectionData>();
            m_NewDungeonCollection.version = 3;
            m_NewDungeonCollection.name = string.Empty;
            m_NewDungeonCollection.materialIdsValid = true;
            m_NewDungeonCollection.needMaterialInstance = false;
            m_NewDungeonCollection.SpriteIDsWithBagelColliders = new List<int>(0);
            m_NewDungeonCollection.SpriteDefinedBagelColliders = new List<BagelColliderData>(0);
            m_NewDungeonCollection.SpriteIDsWithAttachPoints = new List<int>(0);
            m_NewDungeonCollection.SpriteDefinedAttachPoints = new List<AttachPointData>(0);
            m_NewDungeonCollection.SpriteIDsWithNeighborDependencies = new List<int>() { 44, 48, 52 };
            m_NewDungeonCollection.SpriteDefinedIndexNeighborDependencies = new List<NeighborDependencyData>() {
                new NeighborDependencyData(new List<IndexNeighborDependency>() { m_neighborDependency1 }),
                new NeighborDependencyData(new List<IndexNeighborDependency>() { m_neighborDependency2 }),
                new NeighborDependencyData(new List<IndexNeighborDependency>() { m_neighborDependency3 }),
            };
            m_NewDungeonCollection.SpriteIDsWithAnimationSequences = new List<int>() { 22, 26, 30, 44, 48, 52, 66, 67, 68, 69, 70, 748, 770, 792, 814, 858 };
            m_NewDungeonCollection.SpriteDefinedAnimationSequences = new List<SimpleTilesetAnimationSequence>() {
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.SIMPLE_LOOP,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 22, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 23, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 24, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 25, frameTime = 0.3f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.SIMPLE_LOOP,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 26, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 27, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 28, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 29, frameTime = 0.3f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.SIMPLE_LOOP,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 30, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 31, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 32, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 33, frameTime = 0.3f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.SIMPLE_LOOP,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 44, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 45, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 46, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 47, frameTime = 0.3f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.SIMPLE_LOOP,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 48, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 49, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 50, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 51, frameTime = 0.3f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.SIMPLE_LOOP,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 52, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 53, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 54, frameTime = 0.3f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 55, frameTime = 0.3f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.TRIGGERED_ONCE,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 66, frameTime = 0.01f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 88, frameTime = 0.4f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.TRIGGERED_ONCE,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 67, frameTime = 0.01f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 89, frameTime = 0.4f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.TRIGGERED_ONCE,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 68, frameTime = 0.01f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 90, frameTime = 0.4f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.TRIGGERED_ONCE,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 69, frameTime = 0.01f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 91, frameTime = 0.4f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.TRIGGERED_ONCE,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 70, frameTime = 0.01f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 92, frameTime = 0.4f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.SIMPLE_LOOP,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 748, frameTime = 1 },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 748, frameTime = 0.5f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.LOOPCEPTION,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = 748,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 748, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 770, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 771, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 772, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 773, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 774, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 775, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 776, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 748, frameTime = 0.2f },
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.LOOPCEPTION,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = 748,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 748, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 792, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 793, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 794, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 795, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 796, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 797, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 748, frameTime = 0.2f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.LOOPCEPTION,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = 748,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 748, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 814, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 815, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 816, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 817, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 818, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 819, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 820, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 748, frameTime = 0.2f }
                    }
                },
                new SimpleTilesetAnimationSequence() {
                    playstyle = SimpleTilesetAnimationSequence.TilesetSequencePlayStyle.SIMPLE_LOOP,
                    loopDelayMin = 5,
                    loopDelayMax = 10,
                    loopceptionTarget = -1,
                    loopceptionMin = 1,
                    loopceptionMax = 3,
                    coreceptionMin = 1,
                    coreceptionMax = 1,
                    randomStartFrame = false,
                    entries = new List<SimpleTilesetAnimationSequenceEntry>() {
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 858, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 859, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 860, frameTime = 0.2f },
                        new SimpleTilesetAnimationSequenceEntry() { entryIndex = 861, frameTime = 0.2f }
                    }
                }
            };
            m_NewDungeonCollection.premultipliedAlpha = false;
            m_NewDungeonCollection.shouldGenerateTilemapReflectionData = false;
            m_NewDungeonCollection.materials = new Material[] { m_LitCutout, m_LitBlend, m_UnlitCutout };
            m_NewDungeonCollection.textures = new Texture[] { HallPrefabs.ENV_Tileset_Hall_Texture };
            m_NewDungeonCollection.pngTextures = new TextAsset[0];
            m_NewDungeonCollection.materialPngTextureId = new int[] { 0, 0, 0 };
            m_NewDungeonCollection.textureFilterMode = FilterMode.Point;
            m_NewDungeonCollection.textureMipMaps = false;
            m_NewDungeonCollection.allowMultipleAtlases = false;
            m_NewDungeonCollection.spriteCollectionGUID = Guid.NewGuid().ToString();
            m_NewDungeonCollection.spriteCollectionName = "ENV_Tileset_Belly";
            m_NewDungeonCollection.assetName = string.Empty;
            m_NewDungeonCollection.loadable = false;
            m_NewDungeonCollection.invOrthoSize = 2;
            m_NewDungeonCollection.halfTargetHeight = 8;
            m_NewDungeonCollection.buildKey = 638523106;
            m_NewDungeonCollection.dataGuid = Guid.NewGuid().ToString();
            m_NewDungeonCollection.managedSpriteCollection = false;
            m_NewDungeonCollection.hasPlatformData = false;
            m_NewDungeonCollection.spriteCollectionPlatforms = new string[0];
            m_NewDungeonCollection.spriteCollectionPlatformGUIDs = new string[0];


            
            return m_NewDungeonCollection;
        }
    }
}

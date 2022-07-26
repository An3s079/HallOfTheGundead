using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using Gungeon;
using ItemAPI;
using Dungeonator;
using System.Collections.ObjectModel;
using System.Reflection;
using Brave.BulletScript;
using tk2dRuntime.TileMap;
using System.IO;
using SGUI;
using MonoMod.RuntimeDetour;
namespace HallOfGundead
{


    public static class Toolbox
    {
        public static DungeonPlaceable GenerateDungeonPlacable(GameObject ObjectPrefab = null, bool spawnsEnemy = false, bool useExternalPrefab = false, bool spawnsItem = false, string EnemyGUID = "479556d05c7c44f3b6abb3b2067fc778", int itemID = 307, Vector2? CustomOffset = null, bool itemHasDebrisObject = true, float spawnChance = 1f)
        {
            AssetBundle m_assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
            AssetBundle m_assetBundle2 = ResourceManager.LoadAssetBundle("shared_auto_002");
            AssetBundle m_resourceBundle = ResourceManager.LoadAssetBundle("brave_resources_001");

            // Used with custom DungeonPlacable        
            GameObject ChestBrownTwoItems = m_assetBundle.LoadAsset<GameObject>("Chest_Wood_Two_Items");
            GameObject Chest_Silver = m_assetBundle.LoadAsset<GameObject>("chest_silver");
            GameObject Chest_Green = m_assetBundle.LoadAsset<GameObject>("chest_green");
            GameObject Chest_Synergy = m_assetBundle.LoadAsset<GameObject>("chest_synergy");
            GameObject Chest_Red = m_assetBundle.LoadAsset<GameObject>("chest_red");
            GameObject Chest_Black = m_assetBundle.LoadAsset<GameObject>("Chest_Black");
            GameObject Chest_Rainbow = m_assetBundle.LoadAsset<GameObject>("Chest_Rainbow");
            // GameObject Chest_Rat = m_assetBundle.LoadAsset<GameObject>("Chest_Rat");

            m_assetBundle = null;
            m_assetBundle2 = null;
            m_resourceBundle = null;

            DungeonPlaceableVariant BlueChestVariant = new DungeonPlaceableVariant();
            BlueChestVariant.percentChance = 0.35f;
            BlueChestVariant.unitOffset = new Vector2(1, 0.8f);
            BlueChestVariant.enemyPlaceableGuid = string.Empty;
            BlueChestVariant.pickupObjectPlaceableId = -1;
            BlueChestVariant.forceBlackPhantom = false;
            BlueChestVariant.addDebrisObject = false;
            BlueChestVariant.prerequisites = null;
            BlueChestVariant.materialRequirements = null;
            BlueChestVariant.nonDatabasePlaceable = Chest_Silver;

            DungeonPlaceableVariant BrownChestVariant = new DungeonPlaceableVariant();
            BrownChestVariant.percentChance = 0.28f;
            BrownChestVariant.unitOffset = new Vector2(1, 0.8f);
            BrownChestVariant.enemyPlaceableGuid = string.Empty;
            BrownChestVariant.pickupObjectPlaceableId = -1;
            BrownChestVariant.forceBlackPhantom = false;
            BrownChestVariant.addDebrisObject = false;
            BrownChestVariant.prerequisites = null;
            BrownChestVariant.materialRequirements = null;
            BrownChestVariant.nonDatabasePlaceable = ChestBrownTwoItems;

            DungeonPlaceableVariant GreenChestVariant = new DungeonPlaceableVariant();
            GreenChestVariant.percentChance = 0.25f;
            GreenChestVariant.unitOffset = new Vector2(1, 0.8f);
            GreenChestVariant.enemyPlaceableGuid = string.Empty;
            GreenChestVariant.pickupObjectPlaceableId = -1;
            GreenChestVariant.forceBlackPhantom = false;
            GreenChestVariant.addDebrisObject = false;
            GreenChestVariant.prerequisites = null;
            GreenChestVariant.materialRequirements = null;
            GreenChestVariant.nonDatabasePlaceable = Chest_Green;

            DungeonPlaceableVariant SynergyChestVariant = new DungeonPlaceableVariant();
            SynergyChestVariant.percentChance = 0.2f;
            SynergyChestVariant.unitOffset = new Vector2(1, 0.8f);
            SynergyChestVariant.enemyPlaceableGuid = string.Empty;
            SynergyChestVariant.pickupObjectPlaceableId = -1;
            SynergyChestVariant.forceBlackPhantom = false;
            SynergyChestVariant.addDebrisObject = false;
            SynergyChestVariant.prerequisites = null;
            SynergyChestVariant.materialRequirements = null;
            SynergyChestVariant.nonDatabasePlaceable = Chest_Synergy;

            DungeonPlaceableVariant RedChestVariant = new DungeonPlaceableVariant();
            RedChestVariant.percentChance = 0.15f;
            RedChestVariant.unitOffset = new Vector2(0.5f, 0.5f);
            RedChestVariant.enemyPlaceableGuid = string.Empty;
            RedChestVariant.pickupObjectPlaceableId = -1;
            RedChestVariant.forceBlackPhantom = false;
            RedChestVariant.addDebrisObject = false;
            RedChestVariant.prerequisites = null;
            RedChestVariant.materialRequirements = null;
            RedChestVariant.nonDatabasePlaceable = Chest_Red;

            DungeonPlaceableVariant BlackChestVariant = new DungeonPlaceableVariant();
            BlackChestVariant.percentChance = 0.1f;
            BlackChestVariant.unitOffset = new Vector2(0.5f, 0.5f);
            BlackChestVariant.enemyPlaceableGuid = string.Empty;
            BlackChestVariant.pickupObjectPlaceableId = -1;
            BlackChestVariant.forceBlackPhantom = false;
            BlackChestVariant.addDebrisObject = false;
            BlackChestVariant.prerequisites = null;
            BlackChestVariant.materialRequirements = null;
            BlackChestVariant.nonDatabasePlaceable = Chest_Black;

            DungeonPlaceableVariant RainbowChestVariant = new DungeonPlaceableVariant();
            RainbowChestVariant.percentChance = 0.005f;
            RainbowChestVariant.unitOffset = new Vector2(0.5f, 0.5f);
            RainbowChestVariant.enemyPlaceableGuid = string.Empty;
            RainbowChestVariant.pickupObjectPlaceableId = -1;
            RainbowChestVariant.forceBlackPhantom = false;
            RainbowChestVariant.addDebrisObject = false;
            RainbowChestVariant.prerequisites = null;
            RainbowChestVariant.materialRequirements = null;
            RainbowChestVariant.nonDatabasePlaceable = Chest_Rainbow;

            DungeonPlaceableVariant ItemVariant = new DungeonPlaceableVariant();
            ItemVariant.percentChance = spawnChance;
            if (CustomOffset.HasValue)
            {
                ItemVariant.unitOffset = CustomOffset.Value;
            }
            else
            {
                ItemVariant.unitOffset = Vector2.zero;
            }
            // ItemVariant.unitOffset = new Vector2(0.5f, 0.8f);
            ItemVariant.enemyPlaceableGuid = string.Empty;
            ItemVariant.pickupObjectPlaceableId = itemID;
            ItemVariant.forceBlackPhantom = false;
            if (itemHasDebrisObject)
            {
                ItemVariant.addDebrisObject = true;
            }
            else
            {
                ItemVariant.addDebrisObject = false;
            }
            RainbowChestVariant.prerequisites = null;
            RainbowChestVariant.materialRequirements = null;

            List<DungeonPlaceableVariant> ChestTiers = new List<DungeonPlaceableVariant>();
            ChestTiers.Add(BrownChestVariant);
            ChestTiers.Add(BlueChestVariant);
            ChestTiers.Add(GreenChestVariant);
            ChestTiers.Add(SynergyChestVariant);
            ChestTiers.Add(RedChestVariant);
            ChestTiers.Add(BlackChestVariant);
            ChestTiers.Add(RainbowChestVariant);

            DungeonPlaceableVariant EnemyVariant = new DungeonPlaceableVariant();
            EnemyVariant.percentChance = spawnChance;
            EnemyVariant.unitOffset = Vector2.zero;
            EnemyVariant.enemyPlaceableGuid = EnemyGUID;
            EnemyVariant.pickupObjectPlaceableId = -1;
            EnemyVariant.forceBlackPhantom = false;
            EnemyVariant.addDebrisObject = false;
            EnemyVariant.prerequisites = null;
            EnemyVariant.materialRequirements = null;

            List<DungeonPlaceableVariant> EnemyTiers = new List<DungeonPlaceableVariant>();
            EnemyTiers.Add(EnemyVariant);

            List<DungeonPlaceableVariant> ItemTiers = new List<DungeonPlaceableVariant>();
            ItemTiers.Add(ItemVariant);

            DungeonPlaceable m_cachedCustomPlacable = ScriptableObject.CreateInstance<DungeonPlaceable>();
            m_cachedCustomPlacable.name = "CustomChestPlacable";
            if (spawnsEnemy | useExternalPrefab)
            {
                m_cachedCustomPlacable.width = 2;
                m_cachedCustomPlacable.height = 2;
            }
            else if (spawnsItem)
            {
                m_cachedCustomPlacable.width = 1;
                m_cachedCustomPlacable.height = 1;
            }
            else
            {
                m_cachedCustomPlacable.width = 4;
                m_cachedCustomPlacable.height = 1;
            }
            m_cachedCustomPlacable.roomSequential = false;
            m_cachedCustomPlacable.respectsEncounterableDifferentiator = true;
            m_cachedCustomPlacable.UsePrefabTransformOffset = false;
            m_cachedCustomPlacable.isPassable = true;
            if (spawnsItem)
            {
                m_cachedCustomPlacable.MarkSpawnedItemsAsRatIgnored = true;
            }
            else
            {
                m_cachedCustomPlacable.MarkSpawnedItemsAsRatIgnored = false;
            }

            m_cachedCustomPlacable.DebugThisPlaceable = false;
            if (useExternalPrefab && ObjectPrefab != null)
            {
                DungeonPlaceableVariant ExternalObjectVariant = new DungeonPlaceableVariant();
                ExternalObjectVariant.percentChance = spawnChance;
                if (CustomOffset.HasValue)
                {
                    ExternalObjectVariant.unitOffset = CustomOffset.Value;
                }
                else
                {
                    ExternalObjectVariant.unitOffset = Vector2.zero;
                }
                ExternalObjectVariant.enemyPlaceableGuid = string.Empty;
                ExternalObjectVariant.pickupObjectPlaceableId = -1;
                ExternalObjectVariant.forceBlackPhantom = false;
                ExternalObjectVariant.addDebrisObject = false;
                ExternalObjectVariant.nonDatabasePlaceable = ObjectPrefab;
                List<DungeonPlaceableVariant> ExternalObjectTiers = new List<DungeonPlaceableVariant>();
                ExternalObjectTiers.Add(ExternalObjectVariant);
                m_cachedCustomPlacable.variantTiers = ExternalObjectTiers;
            }
            else if (spawnsEnemy)
            {
                m_cachedCustomPlacable.variantTiers = EnemyTiers;
            }
            else if (spawnsItem)
            {
                m_cachedCustomPlacable.variantTiers = ItemTiers;
            }
            else
            {
                m_cachedCustomPlacable.variantTiers = ChestTiers;
            }
            return m_cachedCustomPlacable;
        }

        public static tk2dSpriteDefinition Copy(this tk2dSpriteDefinition orig)
        {
            tk2dSpriteDefinition m_newSpriteCollection = new tk2dSpriteDefinition();

            m_newSpriteCollection.boundsDataCenter = orig.boundsDataCenter;
            m_newSpriteCollection.boundsDataExtents = orig.boundsDataExtents;
            m_newSpriteCollection.colliderConvex = orig.colliderConvex;
            m_newSpriteCollection.colliderSmoothSphereCollisions = orig.colliderSmoothSphereCollisions;
            m_newSpriteCollection.colliderType = orig.colliderType;
            m_newSpriteCollection.colliderVertices = orig.colliderVertices;
            m_newSpriteCollection.collisionLayer = orig.collisionLayer;
            m_newSpriteCollection.complexGeometry = orig.complexGeometry;
            m_newSpriteCollection.extractRegion = orig.extractRegion;
            m_newSpriteCollection.flipped = orig.flipped;
            m_newSpriteCollection.indices = orig.indices;
            if (orig.material != null) { m_newSpriteCollection.material = new Material(orig.material); }
            m_newSpriteCollection.materialId = orig.materialId;
            if (orig.materialInst != null) { m_newSpriteCollection.materialInst = new Material(orig.materialInst); }
            m_newSpriteCollection.metadata = orig.metadata;
            m_newSpriteCollection.name = orig.name;
            m_newSpriteCollection.normals = orig.normals;
            m_newSpriteCollection.physicsEngine = orig.physicsEngine;
            m_newSpriteCollection.position0 = orig.position0;
            m_newSpriteCollection.position1 = orig.position1;
            m_newSpriteCollection.position2 = orig.position2;
            m_newSpriteCollection.position3 = orig.position3;
            m_newSpriteCollection.regionH = orig.regionH;
            m_newSpriteCollection.regionW = orig.regionW;
            m_newSpriteCollection.regionX = orig.regionX;
            m_newSpriteCollection.regionY = orig.regionY;
            m_newSpriteCollection.tangents = orig.tangents;
            m_newSpriteCollection.texelSize = orig.texelSize;
            m_newSpriteCollection.untrimmedBoundsDataCenter = orig.untrimmedBoundsDataCenter;
            m_newSpriteCollection.untrimmedBoundsDataExtents = orig.untrimmedBoundsDataExtents;
            m_newSpriteCollection.uvs = orig.uvs;

            return m_newSpriteCollection;
            /*return new tk2dSpriteDefinition {
                boundsDataCenter = orig.boundsDataCenter,
                boundsDataExtents = orig.boundsDataExtents,
                colliderConvex = orig.colliderConvex,
                colliderSmoothSphereCollisions = orig.colliderSmoothSphereCollisions,
                colliderType = orig.colliderType,
                colliderVertices = orig.colliderVertices,
                collisionLayer = orig.collisionLayer,
                complexGeometry = orig.complexGeometry,
                extractRegion = orig.extractRegion,
                flipped = orig.flipped,
                indices = orig.indices,
                material = new Material(orig.material),
                materialId = orig.materialId,
                materialInst = new Material(orig.materialInst),
                metadata = orig.metadata,
                name = orig.name,
                normals = orig.normals,
                physicsEngine = orig.physicsEngine,
                position0 = orig.position0,
                position1 = orig.position1,
                position2 = orig.position2,
                position3 = orig.position3,
                regionH = orig.regionH,
                regionW = orig.regionW,
                regionX = orig.regionX,
                regionY = orig.regionY,
                tangents = orig.tangents,
                texelSize = orig.texelSize,
                untrimmedBoundsDataCenter = orig.untrimmedBoundsDataCenter,
                untrimmedBoundsDataExtents = orig.untrimmedBoundsDataExtents,
                uvs = orig.uvs
            };*/
        }
        public static Material Copy(this Material orig, Texture2D textureOverride = null, Shader shaderOverride = null)
        {
            Material m_NewMaterial = new Material(orig.shader)
            {
                name = orig.name,
                shaderKeywords = orig.shaderKeywords,
                globalIlluminationFlags = orig.globalIlluminationFlags,
                enableInstancing = orig.enableInstancing,
                doubleSidedGI = orig.doubleSidedGI,
                mainTextureOffset = orig.mainTextureOffset,
                mainTextureScale = orig.mainTextureScale,
                renderQueue = orig.renderQueue,
                color = orig.color,
                hideFlags = orig.hideFlags
            };
            if (textureOverride != null)
            {
                m_NewMaterial.mainTexture = textureOverride;
            }
            else
            {
                m_NewMaterial.mainTexture = orig.mainTexture;
            }

            if (shaderOverride != null)
            {
                m_NewMaterial.shader = shaderOverride;
            }
            else
            {
                m_NewMaterial.shader = orig.shader;
            }
            return m_NewMaterial;
        }

        public static tk2dSpriteCollectionData ReplaceDungeonCollection(tk2dSpriteCollectionData sourceCollection, Texture2D spriteSheet = null, List<string> spriteList = null)
        {
            if (sourceCollection == null) { return null; }
            tk2dSpriteCollectionData collectionData = UnityEngine.Object.Instantiate(sourceCollection);
            tk2dSpriteDefinition[] spriteDefinietions = new tk2dSpriteDefinition[collectionData.spriteDefinitions.Length];
            for (int i = 0; i < collectionData.spriteDefinitions.Length; i++) { spriteDefinietions[i] = collectionData.spriteDefinitions[i].Copy(); }
            collectionData.spriteDefinitions = spriteDefinietions;
            if (spriteSheet != null)
            {
                Material[] materials = sourceCollection.materials;
                Material[] newMaterials = new Material[materials.Length];
                if (materials != null)
                {
                    for (int i = 0; i < materials.Length; i++)
                    {
                        newMaterials[i] = materials[i].Copy(spriteSheet);
                    }
                    collectionData.materials = newMaterials;
                    foreach (Material material2 in collectionData.materials)
                    {
                        foreach (tk2dSpriteDefinition spriteDefinition in collectionData.spriteDefinitions)
                        {
                            bool flag3 = material2 != null && spriteDefinition.material.name.Equals(material2.name);
                            if (flag3)
                            {
                                spriteDefinition.material = material2;
                                spriteDefinition.materialInst = new Material(material2);
                            }
                        }
                    }
                }
            }
            else if (spriteList != null)
            {
                RuntimeAtlasPage runtimeAtlasPage = new RuntimeAtlasPage(0, 0, TextureFormat.RGBA32, 2);
                for (int i = 0; i < spriteList.Count; i++)
                {
                    Texture2D texture2D = ResourceExtractor.GetTextureFromResource(spriteList[i]);
                    if (!texture2D)
                    {
                        Debug.Log("[BuildDungeonCollection] Null Texture found at index: " + i);
                        goto IL_EXIT;
                    }
                    float X = (texture2D.width / 16f);
                    float Y = (texture2D.height / 16f);
                    // tk2dSpriteDefinition spriteData = collectionData.GetSpriteDefinition(i.ToString());
                    tk2dSpriteDefinition spriteData = collectionData.spriteDefinitions[i];
                    if (spriteData != null)
                    {
                        if (spriteData.boundsDataCenter != Vector3.zero)
                        {
                            try
                            {
                                // Debug.Log("[BuildDungeonCollection] Pack Existing Atlas Element at index: " + i);
                                RuntimeAtlasSegment runtimeAtlasSegment = runtimeAtlasPage.Pack(texture2D, false);
                                spriteData.materialInst.mainTexture = runtimeAtlasSegment.texture;
                                spriteData.uvs = runtimeAtlasSegment.uvs;
                                spriteData.extractRegion = true;
                                spriteData.position0 = Vector3.zero;
                                spriteData.position1 = new Vector3(X, 0, 0);
                                spriteData.position2 = new Vector3(0, Y, 0);
                                spriteData.position3 = new Vector3(X, Y, 0);
                                spriteData.boundsDataCenter = new Vector2((X / 2), (Y / 2));
                                spriteData.untrimmedBoundsDataCenter = spriteData.boundsDataCenter;
                                spriteData.boundsDataExtents = new Vector2(X, Y);
                                spriteData.untrimmedBoundsDataExtents = spriteData.boundsDataExtents;
                            }
                            catch (Exception)
                            {
                                Debug.Log("[BuildDungeonCollection] Exception caught at index: " + i);
                            }
                        }
                        else
                        {
                            // Debug.Log("Test 3. Replace Existing Atlas Element at index: " + i);
                            try
                            {
                                ETGMod.ReplaceTexture(spriteData, texture2D, true);
                            }
                            catch (Exception)
                            {
                                Debug.Log("[BuildDungeonCollection] Exception caught at index: " + i);
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("[BuildDungeonCollection] SpriteData is null at index: " + i);
                    }
                    IL_EXIT:;
                }
                runtimeAtlasPage.Apply();
            }
            else
            {
                Debug.Log("[BuildDungeonCollection] SpriteList is null!");
            }
            return collectionData;
        }
        public static PlayerController GunPlayerOwner(this Gun bullet)
        {
            if (bullet && bullet.CurrentOwner && bullet.CurrentOwner is PlayerController) return bullet.CurrentOwner as PlayerController;
            else return null;
        }
        public static void RemoveItemFromInventory(this PlayerController player, PickupObject item)
        {
            if (item == null) return;
            if (item is PassiveItem)
            {
                if (player.passiveItems.Contains(item as PassiveItem))
                {
                    player.passiveItems.Remove(item as PassiveItem);
                    GameUIRoot.Instance.RemovePassiveItemFromDock(item as PassiveItem);
                    player.stats.RecalculateStats(player, false, false);
                }
            }
            else if (item is PlayerItem)
            {
                if (player.activeItems.Contains(item as PlayerItem))
                {
                    player.activeItems.Remove(item as PlayerItem);
                    player.stats.RecalculateStats(player, false, false);
                }
            }
            else if (item is Gun)
            {
                if (player.inventory.AllGuns.Contains(item as Gun))
                {
                    player.inventory.RemoveGunFromInventory(item as Gun);
                    player.stats.RecalculateStats(player, false, false);
                }
            }
        }

        public static void CreateAmmoType(string resourcePath, string silhouetteResourcePath)
        {

            dfAtlas.ItemInfo newItem = new dfAtlas.ItemInfo();
            dfAtlas.ItemInfo newItem2 = new dfAtlas.ItemInfo();
            newItem.name = "testeru tex";
            newItem2.name = "testeru tex2";
            newItem.texture = ResourceExtractor.GetTextureFromResource("SpecialItemPack/Resources/BlackGuonStone.png");
            newItem.sizeInPixels = new Vector2(newItem.texture.width, newItem.texture.height);
            newItem2.sizeInPixels = newItem.sizeInPixels;
            Texture2D atlas = GameUIRoot.Instance.heartControllers[0].armorSpritePrefab.Atlas.Texture;
            Rect region = GameUIRoot.Instance.heartControllers[0].extantHearts[0].Atlas.Items[49].region;

            for (int x = 0; x < newItem.texture.width; x++)
            {
                for (int y = 0; y < newItem.texture.height; y++)
                {
                    atlas.SetPixel(x + (int)(region.xMin * 2048), y + (int)(region.yMin * 2048), newItem.texture.GetPixel(x, y));
                }
            }

            for (int x = 0; x < newItem.texture.width; x++)
            {
                for (int y = 0; y < newItem.texture.height; y++)
                {
                    atlas.SetPixel(x + (int)(region.xMin * 2048) + 1 + newItem.texture.width, y + (int)(region.yMin * 2048) + 1 + newItem.texture.height, Color.gray);
                }
            }
            atlas.Apply(false, false);
            newItem.region = new Rect(region.xMin, region.yMin, (float)newItem.texture.width / 2048f, (float)newItem.texture.height / 2048f);
            newItem2.region = new Rect(region.xMin + (float)(1 + newItem.texture.width) / 2048f, region.yMin + (float)(1 + newItem.texture.height) / 2048f, (float)newItem.texture.width / 2048f, (float)newItem.texture.height / 2048f);
            GameUIRoot.Instance.heartControllers[0].extantHearts[0].Atlas.AddItem(newItem);
            GameUIRoot.Instance.heartControllers[0].extantHearts[0].Atlas.AddItem(newItem2);
            Array.Resize(ref GameUIRoot.Instance.ammoControllers[0].ammoTypes, GameUIRoot.Instance.ammoControllers[0].ammoTypes.Length + 1);
            int last_memeber = GameUIRoot.Instance.ammoControllers[0].ammoTypes.Length - 1;
            GameUIAmmoType type = new GameUIAmmoType
            {
                ammoType = GameUIAmmoType.AmmoType.CUSTOM,
                customAmmoType = "ExampleUIClip"
            };
            GameUIRoot.Instance.ammoControllers[0].ammoTypes[last_memeber] = type;
            GameObject ExampleBG = new GameObject("ExampleBG");
            GameObject ExampleFG = new GameObject("ExampleFG");
            ExampleBG.AddComponent<dfTiledSprite>();
            ExampleFG.AddComponent<dfTiledSprite>();
            type.ammoBarBG = ExampleBG.GetComponent<dfTiledSprite>();
            type.ammoBarFG = ExampleFG.GetComponent<dfTiledSprite>();
            type.ammoBarBG.SpriteName = "testeru tex2";
            type.ammoBarFG.SpriteName = "testeru tex";
        }

        public static void Add<T>(ref T[] array, T toAdd)
        {
            List<T> list = array.ToList();
            list.Add(toAdd);
            array = list.ToArray<T>();

        }
        public static void Add<T>(this T[] array, T toAdd)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = toAdd;
        }

        public static DungeonFlowNode GenerateFlowNode(DungeonFlow flow, PrototypeDungeonRoom.RoomCategory category, PrototypeDungeonRoom overrideRoom = null, GenericRoomTable overrideRoomTable = null, bool loopTargetIsOneWay = false, bool isWarpWing = false,
           bool handlesOwnWarping = true, float weight = 1f, DungeonFlowNode.NodePriority priority = DungeonFlowNode.NodePriority.MANDATORY, string guid = "")
        {
            if (string.IsNullOrEmpty(guid))
            {
                guid = Guid.NewGuid().ToString();
            }
            DungeonFlowNode node = new DungeonFlowNode(flow)
            {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = category,
                percentChance = weight,
                priority = priority,
                overrideExactRoom = overrideRoom,
                overrideRoomTable = overrideRoomTable,
                capSubchain = false,
                subchainIdentifier = string.Empty,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                subchainIdentifiers = new List<string>(0),
                receivesCaps = false,
                isWarpWingEntrance = isWarpWing,
                handlesOwnWarping = handlesOwnWarping,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                chainRules = new List<ChainRule>(0),
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                guidAsString = guid,
                parentNodeGuid = string.Empty,
                childNodeGuids = new List<string>(0),
                loopTargetNodeGuid = string.Empty,
                loopTargetIsOneWay = loopTargetIsOneWay,
                flow = flow
            };
            return node;
        }
		

		public static tk2dSpriteDefinition CopyDefinitionFrom(this tk2dSpriteDefinition other)
        {
            tk2dSpriteDefinition result = new tk2dSpriteDefinition
            {
                boundsDataCenter = new Vector3
                {
                    x = other.boundsDataCenter.x,
                    y = other.boundsDataCenter.y,
                    z = other.boundsDataCenter.z
                },
                boundsDataExtents = new Vector3
                {
                    x = other.boundsDataExtents.x,
                    y = other.boundsDataExtents.y,
                    z = other.boundsDataExtents.z
                },
                colliderConvex = other.colliderConvex,
                colliderSmoothSphereCollisions = other.colliderSmoothSphereCollisions,
                colliderType = other.colliderType,
                colliderVertices = other.colliderVertices,
                collisionLayer = other.collisionLayer,
                complexGeometry = other.complexGeometry,
                extractRegion = other.extractRegion,
                flipped = other.flipped,
                indices = other.indices,
                material = new Material(other.material),
                materialId = other.materialId,
                materialInst = new Material(other.materialInst),
                metadata = other.metadata,
                name = other.name,
                normals = other.normals,
                physicsEngine = other.physicsEngine,
                position0 = new Vector3
                {
                    x = other.position0.x,
                    y = other.position0.y,
                    z = other.position0.z
                },
                position1 = new Vector3
                {
                    x = other.position1.x,
                    y = other.position1.y,
                    z = other.position1.z
                },
                position2 = new Vector3
                {
                    x = other.position2.x,
                    y = other.position2.y,
                    z = other.position2.z
                },
                position3 = new Vector3
                {
                    x = other.position3.x,
                    y = other.position3.y,
                    z = other.position3.z
                },
                regionH = other.regionH,
                regionW = other.regionW,
                regionX = other.regionX,
                regionY = other.regionY,
                tangents = other.tangents,
                texelSize = new Vector2
                {
                    x = other.texelSize.x,
                    y = other.texelSize.y
                },
                untrimmedBoundsDataCenter = new Vector3
                {
                    x = other.untrimmedBoundsDataCenter.x,
                    y = other.untrimmedBoundsDataCenter.y,
                    z = other.untrimmedBoundsDataCenter.z
                },
                untrimmedBoundsDataExtents = new Vector3
                {
                    x = other.untrimmedBoundsDataExtents.x,
                    y = other.untrimmedBoundsDataExtents.y,
                    z = other.untrimmedBoundsDataExtents.z
                }
            };
            List<Vector2> uvs = new List<Vector2>();
            foreach (Vector2 vector in other.uvs)
            {
                uvs.Add(new Vector2
                {
                    x = vector.x,
                    y = vector.y
                });
            }
            result.uvs = uvs.ToArray();
            List<Vector3> colliderVertices = new List<Vector3>();
            foreach (Vector3 vector in other.colliderVertices)
            {
                colliderVertices.Add(new Vector3
                {
                    x = vector.x,
                    y = vector.y,
                    z = vector.z
                });
            }
            result.colliderVertices = colliderVertices.ToArray();
            return result;
        }

        public static void SetProjectileSpriteRight(this Projectile proj, string name, int pixelWidth, int pixelHeight, int? overrideColliderPixelWidth = null, int? overrideColliderPixelHeight = null)
        {
            try
            {
                bool flag = overrideColliderPixelWidth == null;
                if (flag)
                {
                    overrideColliderPixelWidth = new int?(pixelWidth);
                }
                bool flag2 = overrideColliderPixelHeight == null;
                if (flag2)
                {
                    overrideColliderPixelHeight = new int?(pixelHeight);
                }
                float num = (float)pixelWidth / 16f;
                float num2 = (float)pixelHeight / 16f;
                float x = (float)overrideColliderPixelWidth.Value / 16f;
                float y = (float)overrideColliderPixelHeight.Value / 16f;
                proj.GetAnySprite().spriteId = ETGMod.Databases.Items.ProjectileCollection.inst.GetSpriteIdByName(name);
                tk2dSpriteDefinition tk2dSpriteDefinition = ETGMod.Databases.Items.ProjectileCollection.inst.spriteDefinitions[(PickupObjectDatabase.GetById(12) as Gun).DefaultModule.projectiles[0].GetAnySprite().spriteId].CopyDefinitionFrom();
                tk2dSpriteDefinition.boundsDataCenter = new Vector3(num / 2f, num2 / 2f, 0f);
                tk2dSpriteDefinition.boundsDataExtents = new Vector3(num, num2, 0f);
                tk2dSpriteDefinition.untrimmedBoundsDataCenter = new Vector3(num / 2f, num2 / 2f, 0f);
                tk2dSpriteDefinition.untrimmedBoundsDataExtents = new Vector3(num, num2, 0f);
                tk2dSpriteDefinition.position0 = new Vector3(0f, 0f, 0f);
                tk2dSpriteDefinition.position1 = new Vector3(0f + num, 0f, 0f);
                tk2dSpriteDefinition.position2 = new Vector3(0f, 0f + num2, 0f);
                tk2dSpriteDefinition.position3 = new Vector3(0f + num, 0f + num2, 0f);
                tk2dSpriteDefinition.colliderVertices[1].x = x;
                tk2dSpriteDefinition.colliderVertices[1].y = y;
                tk2dSpriteDefinition.name = name;
                ETGMod.Databases.Items.ProjectileCollection.inst.spriteDefinitions[proj.GetAnySprite().spriteId] = tk2dSpriteDefinition;
                proj.baseData.force = 0f;
            }
            catch (Exception ex)
            {
                ETGModConsole.Log("Ooops! Seems like something got very, Very, VERY wrong. Here's the exception:", false);
                ETGModConsole.Log(ex.ToString(), false);
            }
        }
        public class PierceDeadActors : MonoBehaviour
        {
            public PierceDeadActors()
            {
            }
            private void Start()
            {
                this.m_projectile = base.GetComponent<Projectile>();
                this.m_projectile.specRigidbody.OnPreRigidbodyCollision += this.PreCollision;
            }
            private void PreCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider)
            {
                if (myRigidbody != null && otherRigidbody != null)
                {
                    if (otherRigidbody.healthHaver != null && otherRigidbody.healthHaver.IsDead)
                    {
                        PhysicsEngine.SkipCollision = true;
                    }
                }
            }
            private Projectile m_projectile;
        }

        public class ModifiedHomingModifier : BraveBehaviour
        {
            private void Start()
            {
                if (!this.m_projectile)
                {
                    this.m_projectile = base.GetComponent<Projectile>();
                }
                Projectile projectile = this.m_projectile;
                projectile.ModifyVelocity = (Func<Vector2, Vector2>)Delegate.Combine(projectile.ModifyVelocity, new Func<Vector2, Vector2>(this.ModifyVelocity));
            }

            public void AssignProjectile(Projectile source)
            {
                this.m_projectile = source;
            }

            private Vector2 ModifyVelocity(Vector2 inVel)
            {
                Vector2 vector = inVel;
                RoomHandler absoluteRoomFromPosition = GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(this.m_projectile.LastPosition.IntXY(VectorConversions.Floor));
                List<AIActor> activeEnemies = absoluteRoomFromPosition.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                if (activeEnemies == null || activeEnemies.Count == 0)
                {
                    return inVel;
                }
                float num = float.MaxValue;
                Vector2 vector2 = Vector2.zero;
                AIActor x = null;
                Vector2 b = (!base.sprite) ? base.transform.position.XY() : base.sprite.WorldCenter;
                for (int i = 0; i < activeEnemies.Count; i++)
                {
                    AIActor aiactor = activeEnemies[i];
                    if (!aiactor.healthHaver.IsDead)
                    {
                        if (aiactor && aiactor.IsWorthShootingAt && !aiactor.IsGone)
                        {
                            Vector2 vector3 = aiactor.CenterPosition - b;
                            float sqrMagnitude = vector3.sqrMagnitude;
                            if (sqrMagnitude < num)
                            {
                                vector2 = vector3;
                                num = sqrMagnitude;
                                x = aiactor;
                            }
                        }
                    }
                }
                num = Mathf.Sqrt(num);
                if (num < this.HomingRadius && x != null)
                {
                    float num2 = 1f - num / this.HomingRadius;
                    float target = vector2.ToAngle();
                    float num3 = inVel.ToAngle();
                    float maxDelta = this.AngularVelocity * num2 * this.m_projectile.LocalDeltaTime;
                    float num4 = Mathf.MoveTowardsAngle(num3, target, maxDelta);
                    if (this.m_projectile is HelixProjectile)
                    {
                        float angleDiff = num4 - num3;
                        (this.m_projectile as HelixProjectile).AdjustRightVector(angleDiff);
                    }
                    else
                    {
                        if (this.m_projectile.shouldRotate)
                        {
                            base.transform.rotation = Quaternion.Euler(0f, 0f, num4);
                        }
                        vector = BraveMathCollege.DegreesToVector(num4, inVel.magnitude);
                    }
                    if (this.m_projectile.OverrideMotionModule != null)
                    {
                        this.m_projectile.OverrideMotionModule.AdjustRightVector(num4 - num3);
                    }
                }
                if (vector == Vector2.zero || float.IsNaN(vector.x) || float.IsNaN(vector.y))
                {
                    return inVel;
                }
                return vector;
            }

            protected override void OnDestroy()
            {
                if (this.m_projectile)
                {
                    Projectile projectile = this.m_projectile;
                    projectile.ModifyVelocity = (Func<Vector2, Vector2>)Delegate.Remove(projectile.ModifyVelocity, new Func<Vector2, Vector2>(this.ModifyVelocity));
                }
                base.OnDestroy();
            }


            public float HomingRadius = 2f;

            public float AngularVelocity = 180f;
            public Projectile m_projectile;
        }
        public static SpeculativeRigidbody GenerateOrAddToRigidBody(GameObject targetObject, CollisionLayer collisionLayer, PixelCollider.PixelColliderGeneration colliderGenerationMode = PixelCollider.PixelColliderGeneration.Tk2dPolygon, bool collideWithTileMap = false, bool CollideWithOthers = true, bool CanBeCarried = true, bool CanBePushed = false, bool RecheckTriggers = false, bool IsTrigger = false, bool replaceExistingColliders = false, bool UsesPixelsAsUnitSize = false, IntVector2? dimensions = null, IntVector2? offset = null)
        {
            SpeculativeRigidbody m_CachedRigidBody = GameObjectExtensions.GetOrAddComponent<SpeculativeRigidbody>(targetObject);
            m_CachedRigidBody.CollideWithOthers = CollideWithOthers;
            m_CachedRigidBody.CollideWithTileMap = collideWithTileMap;
            m_CachedRigidBody.Velocity = Vector2.zero;
            m_CachedRigidBody.MaxVelocity = Vector2.zero;
            m_CachedRigidBody.ForceAlwaysUpdate = false;
            m_CachedRigidBody.CanPush = false;
            m_CachedRigidBody.CanBePushed = CanBePushed;
            m_CachedRigidBody.PushSpeedModifier = 1f;
            m_CachedRigidBody.CanCarry = false;
            m_CachedRigidBody.CanBeCarried = CanBeCarried;
            m_CachedRigidBody.PreventPiercing = false;
            m_CachedRigidBody.SkipEmptyColliders = false;
            m_CachedRigidBody.RecheckTriggers = RecheckTriggers;
            m_CachedRigidBody.UpdateCollidersOnRotation = false;
            m_CachedRigidBody.UpdateCollidersOnScale = false;

            IntVector2 Offset = IntVector2.Zero;
            IntVector2 Dimensions = IntVector2.Zero;
            if (colliderGenerationMode != PixelCollider.PixelColliderGeneration.Tk2dPolygon)
            {
                if (dimensions.HasValue)
                {
                    Dimensions = dimensions.Value;
                    if (!UsesPixelsAsUnitSize)
                    {
                        Dimensions = (new IntVector2(Dimensions.x * 16, Dimensions.y * 16));
                    }
                }
                if (offset.HasValue)
                {
                    Offset = offset.Value;
                    if (!UsesPixelsAsUnitSize)
                    {
                        Offset = (new IntVector2(Offset.x * 16, Offset.y * 16));
                    }
                }
            }
            PixelCollider m_CachedCollider = new PixelCollider()
            {
                ColliderGenerationMode = colliderGenerationMode,
                CollisionLayer = collisionLayer,
                IsTrigger = IsTrigger,
                BagleUseFirstFrameOnly = (colliderGenerationMode == PixelCollider.PixelColliderGeneration.Tk2dPolygon),
                SpecifyBagelFrame = string.Empty,
                BagelColliderNumber = 0,
                ManualOffsetX = Offset.x,
                ManualOffsetY = Offset.y,
                ManualWidth = Dimensions.x,
                ManualHeight = Dimensions.y,
                ManualDiameter = 0,
                ManualLeftX = 0,
                ManualLeftY = 0,
                ManualRightX = 0,
                ManualRightY = 0
            };

            if (replaceExistingColliders | m_CachedRigidBody.PixelColliders == null)
            {
                m_CachedRigidBody.PixelColliders = new List<PixelCollider> { m_CachedCollider };
            }
            else
            {
                m_CachedRigidBody.PixelColliders.Add(m_CachedCollider);
            }

            if (m_CachedRigidBody.sprite && colliderGenerationMode == PixelCollider.PixelColliderGeneration.Tk2dPolygon)
            {
                Bounds bounds = m_CachedRigidBody.sprite.GetBounds();
                m_CachedRigidBody.sprite.GetTrueCurrentSpriteDef().colliderVertices = new Vector3[] { bounds.center - bounds.extents, bounds.center + bounds.extents };
                // m_CachedRigidBody.ForceRegenerate();
                // m_CachedRigidBody.RegenerateCache();
            }

            return m_CachedRigidBody;
        }


        public static void DuplicateSprite(tk2dSprite targetSprite, tk2dSprite sourceSprite)
        {
            targetSprite.automaticallyManagesDepth = sourceSprite.automaticallyManagesDepth;
            targetSprite.ignoresTiltworldDepth = sourceSprite.ignoresTiltworldDepth;
            targetSprite.depthUsesTrimmedBounds = sourceSprite.depthUsesTrimmedBounds;
            targetSprite.allowDefaultLayer = sourceSprite.allowDefaultLayer;
            targetSprite.attachParent = sourceSprite.attachParent;
            targetSprite.OverrideMaterialMode = sourceSprite.OverrideMaterialMode;
            targetSprite.independentOrientation = sourceSprite.independentOrientation;
            targetSprite.autodetectFootprint = sourceSprite.autodetectFootprint;
            targetSprite.customFootprintOrigin = sourceSprite.customFootprintOrigin;
            targetSprite.customFootprint = sourceSprite.customFootprint;
            targetSprite.hasOffScreenCachedUpdate = sourceSprite.hasOffScreenCachedUpdate;
            targetSprite.offScreenCachedCollection = sourceSprite.offScreenCachedCollection;
            targetSprite.offScreenCachedID = sourceSprite.offScreenCachedID;
            targetSprite.Collection = sourceSprite.Collection;
            targetSprite.color = sourceSprite.color;
            targetSprite.scale = sourceSprite.scale;
            targetSprite.spriteId = sourceSprite.spriteId;
            targetSprite.boxCollider2D = sourceSprite.boxCollider2D;
            targetSprite.boxCollider = sourceSprite.boxCollider;
            targetSprite.meshCollider = sourceSprite.meshCollider;
            targetSprite.meshColliderPositions = sourceSprite.meshColliderPositions;
            targetSprite.meshColliderMesh = sourceSprite.meshColliderMesh;
            targetSprite.CachedPerpState = sourceSprite.CachedPerpState;
            targetSprite.HeightOffGround = sourceSprite.HeightOffGround;
            targetSprite.SortingOrder = sourceSprite.SortingOrder;
            targetSprite.IsBraveOutlineSprite = sourceSprite.IsBraveOutlineSprite;
            targetSprite.IsZDepthDirty = sourceSprite.IsZDepthDirty;
            targetSprite.ApplyEmissivePropertyBlock = sourceSprite.ApplyEmissivePropertyBlock;
            targetSprite.GenerateUV2 = sourceSprite.GenerateUV2;
            targetSprite.LockUV2OnFrameOne = sourceSprite.LockUV2OnFrameOne;
            targetSprite.StaticPositions = sourceSprite.StaticPositions;
        }

        public static GameObject SpawnProjectileTowardsPoint(GameObject projectile, Vector2 startingPosition, Vector2 targetPosition, float angleOffset = 0, float angleVariance = 0, PlayerController playerToScaleAccuracyOff = null)
        {
            Vector2 dirVec = (targetPosition - startingPosition);
            if (angleOffset != 0)
            {
                dirVec = dirVec.Rotate(angleOffset);
            }
            if (angleVariance != 0)
            {
                if (playerToScaleAccuracyOff != null) angleVariance *= playerToScaleAccuracyOff.stats.GetStatValue(PlayerStats.StatType.Accuracy);
                float positiveVariance = angleVariance * 0.5f;
                float negativeVariance = positiveVariance * -1f;
                float finalVariance = UnityEngine.Random.Range(negativeVariance, positiveVariance);
                dirVec = dirVec.Rotate(finalVariance);
            }
            return SpawnManager.SpawnProjectile(projectile, startingPosition, Quaternion.Euler(0f, 0f, dirVec.ToAngle()), true);
        }
    }

    public static class ReflectionHelpers
    {

        public static IList CreateDynamicList(Type type)
        {
            bool flag = type == null;
            if (flag) { throw new ArgumentNullException("type", "Argument cannot be null."); }
            ConstructorInfo[] constructors = typeof(List<>).MakeGenericType(new Type[] { type }).GetConstructors();
            foreach (ConstructorInfo constructorInfo in constructors)
            {
                ParameterInfo[] parameters = constructorInfo.GetParameters();
                bool flag2 = parameters.Length != 0;
                if (!flag2) { return (IList)constructorInfo.Invoke(null, null); }
            }
            throw new ApplicationException("Could not create a new list with type <" + type.ToString() + ">.");
        }

        public static IDictionary CreateDynamicDictionary(Type typeKey, Type typeValue)
        {
            bool flag = typeKey == null;
            if (flag)
            {
                throw new ArgumentNullException("type_key", "Argument cannot be null.");
            }
            bool flag2 = typeValue == null;
            if (flag2) { throw new ArgumentNullException("type_value", "Argument cannot be null."); }
            ConstructorInfo[] constructors = typeof(Dictionary<,>).MakeGenericType(new Type[] { typeKey, typeValue }).GetConstructors();
            foreach (ConstructorInfo constructorInfo in constructors)
            {
                ParameterInfo[] parameters = constructorInfo.GetParameters();
                bool flag3 = parameters.Length != 0;
                if (!flag3) { return (IDictionary)constructorInfo.Invoke(null, null); }
            }
            throw new ApplicationException(string.Concat(new string[] {
                "Could not create a new dictionary with types <",
                typeKey.ToString(),
                ",",
                typeValue.ToString(),
                ">."
            }));
        }

        public static T ReflectGetField<T>(Type classType, string fieldName, object o = null)
        {
            FieldInfo field = classType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static));
            return (T)field.GetValue(o);
        }

        public static void ReflectSetField<T>(Type classType, string fieldName, T value, object o = null)
        {
            FieldInfo field = classType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static));
            field.SetValue(o, value);
        }

        public static T ReflectGetProperty<T>(Type classType, string propName, object o = null, object[] indexes = null)
        {
            PropertyInfo property = classType.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static));
            return (T)property.GetValue(o, indexes);
        }

        public static void ReflectSetProperty<T>(Type classType, string propName, T value, object o = null, object[] indexes = null)
        {
            PropertyInfo property = classType.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static));
            property.SetValue(o, value, indexes);
        }

        public static MethodInfo ReflectGetMethod(Type classType, string methodName, Type[] methodArgumentTypes = null, Type[] genericMethodTypes = null, bool? isStatic = null)
        {
            MethodInfo[] array = ReflectTryGetMethods(classType, methodName, methodArgumentTypes, genericMethodTypes, isStatic);
            bool flag = array.Count() == 0;
            if (flag) { throw new MissingMethodException("Cannot reflect method, not found based on input parameters."); }
            bool flag2 = array.Count() > 1;
            if (flag2) { throw new InvalidOperationException("Cannot reflect method, more than one method matched based on input parameters."); }
            return array[0];
        }

        public static MethodInfo ReflectTryGetMethod(Type classType, string methodName, Type[] methodArgumentTypes = null, Type[] genericMethodTypes = null, bool? isStatic = null)
        {
            MethodInfo[] array = ReflectTryGetMethods(classType, methodName, methodArgumentTypes, genericMethodTypes, isStatic);
            bool flag = array.Count() == 0;
            MethodInfo result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = array.Count() > 1;
                if (flag2) { result = null; } else { result = array[0]; }
            }
            return result;
        }

        public static MethodInfo[] ReflectTryGetMethods(Type classType, string methodName, Type[] methodArgumentTypes = null, Type[] genericMethodTypes = null, bool? isStatic = null)
        {
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
            bool flag = isStatic == null || isStatic.Value;
            if (flag) { bindingFlags |= BindingFlags.Static; }
            bool flag2 = isStatic == null || !isStatic.Value;
            if (flag2) { bindingFlags |= BindingFlags.Instance; }
            MethodInfo[] methods = classType.GetMethods(bindingFlags);
            List<MethodInfo> list = new List<MethodInfo>();
            for (int i = 0; i < methods.Length; i++)
            {
                // foreach (MethodInfo methodInfo in methods) {
                bool flag3 = methods[i].Name != methodName;
                if (!flag3)
                {
                    bool isGenericMethodDefinition = methods[i].IsGenericMethodDefinition;
                    if (isGenericMethodDefinition)
                    {
                        bool flag4 = genericMethodTypes == null || genericMethodTypes.Length == 0;
                        if (flag4) { goto IL_14D; }
                        Type[] genericArguments = methods[i].GetGenericArguments();
                        bool flag5 = genericArguments.Length != genericMethodTypes.Length;
                        if (flag5) { goto IL_14D; }
                        methods[i] = methods[i].MakeGenericMethod(genericMethodTypes);
                    }
                    else
                    {
                        bool flag6 = genericMethodTypes != null && genericMethodTypes.Length != 0;
                        if (flag6) { goto IL_14D; }
                    }
                    ParameterInfo[] parameters = methods[i].GetParameters();
                    bool flag7 = methodArgumentTypes != null;
                    if (!flag7) { goto IL_141; }
                    bool flag8 = parameters.Length != methodArgumentTypes.Length;
                    if (!flag8)
                    {
                        for (int j = 0; j < parameters.Length; j++)
                        {
                            ParameterInfo parameterInfo = parameters[j];
                            bool flag9 = parameterInfo.ParameterType != methodArgumentTypes[j];
                            if (flag9) { goto IL_14A; }
                        }
                        goto IL_141;
                    }
                    IL_14A:
                    goto IL_14D;
                    IL_141:
                    list.Add(methods[i]);
                }
                IL_14D:;
            }
            return list.ToArray();
        }

        public static object InvokeRefs<T0>(MethodInfo methodInfo, object o, T0 p0)
        {
            object[] parameters = new object[] { p0 };
            return methodInfo.Invoke(o, parameters);
        }

        public static object InvokeRefs<T0>(MethodInfo methodInfo, object o, ref T0 p0)
        {
            object[] array = new object[] { p0 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            return result;
        }

        public static object InvokeRefs<T0, T1>(MethodInfo methodInfo, object o, T0 p0, T1 p1)
        {
            object[] parameters = new object[] { p0, p1 };
            return methodInfo.Invoke(o, parameters);
        }

        public static object InvokeRefs<T0, T1>(MethodInfo methodInfo, object o, ref T0 p0, T1 p1)
        {
            object[] array = new object[] { p0, p1 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            return result;
        }

        public static object InvokeRefs<T0, T1>(MethodInfo methodInfo, object o, T0 p0, ref T1 p1)
        {
            object[] array = new object[] { p0, p1 };
            object result = methodInfo.Invoke(o, array);
            p1 = (T1)array[1];
            return result;
        }

        public static object InvokeRefs<T0, T1>(MethodInfo methodInfo, object o, ref T0 p0, ref T1 p1)
        {
            object[] array = new object[] { p0, p1 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            p1 = (T1)array[1];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, T0 p0, T1 p1, T2 p2)
        {
            object[] parameters = new object[] { p0, p1, p2 };
            return methodInfo.Invoke(o, parameters);
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, ref T0 p0, T1 p1, T2 p2)
        {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, T0 p0, ref T1 p1, T2 p2)
        {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p1 = (T1)array[1];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, T0 p0, T1 p1, ref T2 p2)
        {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p2 = (T2)array[2];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, ref T0 p0, ref T1 p1, T2 p2)
        {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            p1 = (T1)array[1];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, ref T0 p0, T1 p1, ref T2 p2)
        {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            p2 = (T2)array[2];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, T0 p0, ref T1 p1, ref T2 p2)
        {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p1 = (T1)array[1];
            p2 = (T2)array[2];
            return result;
        }

        public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, ref T0 p0, ref T1 p1, ref T2 p2)
        {
            object[] array = new object[] { p0, p1, p2 };
            object result = methodInfo.Invoke(o, array);
            p0 = (T0)array[0];
            p1 = (T1)array[1];
            p2 = (T2)array[2];
            return result;
        }
    }
}

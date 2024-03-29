﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;

using ItemAPI;
namespace ItemAPI
{
    public static class SpriteBuilder
    {
        private static tk2dSpriteCollectionData itemCollection = PickupObjectDatabase.GetByEncounterName("singularity").sprite.Collection;
        public static tk2dSpriteCollectionData ammonomiconCollection = AmmonomiconController.ForceInstance.EncounterIconCollection;
        private static tk2dSprite baseSprite = PickupObjectDatabase.GetByEncounterName("singularity").GetComponent<tk2dSprite>();

        /// <summary>
        /// Returns an object with a tk2dSprite component with the 
        /// texture of a file in the sprites folder
        /// </summary>
        public static GameObject SpriteFromFile(string spriteName, GameObject obj = null)
        {
            string filename = spriteName.Replace(".png", "");

            var texture = ResourceExtractor.GetTextureFromFile(filename);
            if (texture == null) return null;

            return SpriteFromTexture(texture, spriteName, obj);
        }

        /// <summary>
        /// Returns an object with a tk2dSprite component with the 
        /// texture of an embedded resource
        /// </summary>
        public static GameObject SpriteFromResource(string spriteName, GameObject obj = null, bool copyFromExisting = true)
        {
            string str = (!spriteName.EndsWith(".png")) ? ".png" : "";
            string text = spriteName + str;
            Texture2D textureFromResource = ResourceExtractor.GetTextureFromResource(text);
            bool flag = textureFromResource == null;
            GameObject result;
            if (flag)
            {
                result = null;
            }
            else
            {
                result = SpriteFromTexture(textureFromResource, text, obj, copyFromExisting);
            }
            return result;
        }


        public static GameObject SpriteFromTexture(Texture2D texture, string spriteName, GameObject obj = null, bool copyFromExisting = true)
        {
            bool flag = obj == null;
            if (flag) { obj = new GameObject(); }
            tk2dSprite tk2dSprite;
            if (copyFromExisting)
            {
                tk2dSprite = obj.AddComponent(baseSprite);
            }
            else
            {
                tk2dSprite = obj.AddComponent<tk2dSprite>();
            }
            int num = AddSpriteToCollection(spriteName, itemCollection);
            tk2dSprite.SetSprite(itemCollection, num);
            tk2dSprite.SortingOrder = 0;
            obj.GetComponent<BraveBehaviour>().sprite = tk2dSprite;
            return obj;
        }

        public static GameObject SpriteFromTexture(Texture2D existingTexture, GameObject obj = null, bool copyFromExisting = true)
        {
            bool flag = obj == null;
            if (flag) { obj = new GameObject(); }
            tk2dSprite tk2dSprite;
            if (copyFromExisting)
            {
                tk2dSprite = obj.AddComponent(baseSprite);
            }
            else
            {
                tk2dSprite = obj.AddComponent<tk2dSprite>();
            }
            int num = AddSpriteToCollection(existingTexture, itemCollection);
            tk2dSprite.SetSprite(itemCollection, num);
            tk2dSprite.SortingOrder = 0;
            obj.GetComponent<BraveBehaviour>().sprite = tk2dSprite;
            return obj;
        }

        /// <summary>
        /// Adds a sprite (from a resource) to a collection
        /// </summary>
        /// <returns>The spriteID of the defintion in the collection</returns>
        public static int AddSpriteToCollection(string resourcePath, tk2dSpriteCollectionData collection)
        {
            string extension = !resourcePath.EndsWith(".png") ? ".png" : "";
            resourcePath += extension;
            var texture = ResourceExtractor.GetTextureFromResource(resourcePath); //Get Texture

            var definition = ConstructDefinition(texture); //Generate definition
            definition.name = texture.name; //naming the definition is actually extremely important 

            return AddSpriteToCollection(definition, collection);
        }

   
        public static int AddSpriteToCollection(Texture2D existingTexture, tk2dSpriteCollectionData collection)
        {
            tk2dSpriteDefinition tk2dSpriteDefinition = ConstructDefinition(existingTexture);
            tk2dSpriteDefinition.name = existingTexture.name;
            return AddSpriteToCollection(tk2dSpriteDefinition, collection);
        }

        public static void AddSpritesToCollection(AssetBundle assetSource, List<string> AssetNames, tk2dSpriteCollectionData collection)
        {
            List<Texture> m_Textures = new List<Texture>();
            foreach (string AssetName in AssetNames) { m_Textures.Add(assetSource.LoadAsset<Texture2D>(AssetName)); }

            if (m_Textures.Count > 0)
            {
                foreach (Texture2D texture in m_Textures)
                {
                    tk2dSpriteDefinition tk2dSpriteDefinition = ConstructDefinition(texture);
                    tk2dSpriteDefinition.name = texture.name;
                    AddSpriteToCollection(tk2dSpriteDefinition, collection);
                }
            }
        }

        public static void AddSpritesToCollection(List<string> ResourceNames, tk2dSpriteCollectionData collection)
        {
            foreach (string ResourceName in ResourceNames)
            {
                string resourcePath = ResourceName;
                string str = (!resourcePath.EndsWith(".png")) ? ".png" : "";
                resourcePath += str;
                Texture2D textureFromResource = ResourceExtractor.GetTextureFromResource(resourcePath);
                tk2dSpriteDefinition tk2dSpriteDefinition = ConstructDefinition(textureFromResource);
                tk2dSpriteDefinition.name = textureFromResource.name;
                AddSpriteToCollection(tk2dSpriteDefinition, collection);
            }
        }

        public static int AddSpriteToCollection(tk2dSpriteDefinition spriteDefinition, tk2dSpriteCollectionData collection)
        {
            tk2dSpriteDefinition[] spriteDefinitions = collection.spriteDefinitions;
            tk2dSpriteDefinition[] array = spriteDefinitions.Concat(new tk2dSpriteDefinition[] { spriteDefinition }).ToArray();
            collection.spriteDefinitions = array;
            FieldInfo field = typeof(tk2dSpriteCollectionData).GetField("spriteNameLookupDict", BindingFlags.Instance | BindingFlags.NonPublic);
            field.SetValue(collection, null);
            collection.InitDictionary();
            return array.Length - 1;
        }


        /// <summary>
        /// Adds a sprite definition to the Ammonomicon sprite collection
        /// </summary>
        /// <returns>The spriteID of the defintion in the ammonomicon collection</returns>
        public static int AddToAmmonomicon(tk2dSpriteDefinition spriteDefinition)
        {
            return AddSpriteToCollection(spriteDefinition, ammonomiconCollection);
        }

        public static tk2dSpriteAnimationClip AddAnimation(tk2dSpriteAnimator animator, tk2dSpriteCollectionData collection, List<int> spriteIDs,
            string clipName, tk2dSpriteAnimationClip.WrapMode wrapMode = tk2dSpriteAnimationClip.WrapMode.Loop)
        {
            if (animator.Library == null)
            {
                animator.Library = animator.gameObject.AddComponent<tk2dSpriteAnimation>();
                animator.Library.clips = new tk2dSpriteAnimationClip[0];
                animator.Library.enabled = true;

            }

            List<tk2dSpriteAnimationFrame> frames = new List<tk2dSpriteAnimationFrame>();
            for (int i = 0; i < spriteIDs.Count; i++)
            {
                tk2dSpriteDefinition sprite = collection.spriteDefinitions[spriteIDs[i]];
                if (sprite.Valid)
                {
                    frames.Add(new tk2dSpriteAnimationFrame()
                    {
                        spriteCollection = collection,
                        spriteId = spriteIDs[i]
                    });
                }
            }

            var clip = new tk2dSpriteAnimationClip()
            {
                name = clipName,
                fps = 15,
                wrapMode = wrapMode,
            };
            Array.Resize(ref animator.Library.clips, animator.Library.clips.Length + 1);
            animator.Library.clips[animator.Library.clips.Length - 1] = clip;

            clip.frames = frames.ToArray();
            return clip;
        }

        public static SpeculativeRigidbody SetUpSpeculativeRigidbody(this tk2dSprite sprite, IntVector2 offset, IntVector2 dimensions)
        {
            var body = sprite.gameObject.GetOrAddComponent<SpeculativeRigidbody>();
            PixelCollider collider = new PixelCollider();
            collider.ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual;
            collider.CollisionLayer = CollisionLayer.EnemyCollider;

            collider.ManualWidth = dimensions.x;
            collider.ManualHeight = dimensions.y;
            collider.ManualOffsetX = offset.x;
            collider.ManualOffsetY = offset.y;

            body.PixelColliders = new List<PixelCollider>() { collider };

            return body;
        }

        /// <summary>
        /// Constructs a new tk2dSpriteDefinition with the given texture
        /// </summary>
        /// <returns>A new sprite definition with the given texture</returns>
        public static tk2dSpriteDefinition ConstructDefinition(Texture2D texture)
        {
            RuntimeAtlasSegment ras = ETGMod.Assets.Packer.Pack(texture); //pack your resources beforehand or the outlines will turn out weird

            Material material = new Material(ShaderCache.Acquire(PlayerController.DefaultShaderName));
            material.mainTexture = ras.texture;
            //material.mainTexture = texture;

            var width = texture.width;
            var height = texture.height;

            var x = 0f;
            var y = 0f;

            var w = width / 16f;
            var h = height / 16f;

            var def = new tk2dSpriteDefinition
            {
                normals = new Vector3[] {
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
            },
                tangents = new Vector4[] {
                new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
            },
                texelSize = new Vector2(1 / 16f, 1 / 16f),
                extractRegion = false,
                regionX = 0,
                regionY = 0,
                regionW = 0,
                regionH = 0,
                flipped = tk2dSpriteDefinition.FlipMode.None,
                complexGeometry = false,
                physicsEngine = tk2dSpriteDefinition.PhysicsEngine.Physics3D,
                colliderType = tk2dSpriteDefinition.ColliderType.None,
                collisionLayer = CollisionLayer.HighObstacle,
                position0 = new Vector3(x, y, 0f),
                position1 = new Vector3(x + w, y, 0f),
                position2 = new Vector3(x, y + h, 0f),
                position3 = new Vector3(x + w, y + h, 0f),
                material = material,
                materialInst = material,
                materialId = 0,
                //uvs = ETGMod.Assets.GenerateUVs(texture, 0, 0, width, height), //uv machine broke
                uvs = ras.uvs,
                boundsDataCenter = new Vector3(w / 2f, h / 2f, 0f),
                boundsDataExtents = new Vector3(w, h, 0f),
                untrimmedBoundsDataCenter = new Vector3(w / 2f, h / 2f, 0f),
                untrimmedBoundsDataExtents = new Vector3(w, h, 0f),
            };

            def.name = texture.name;
            return def;
        }

        public static tk2dSpriteCollectionData ConstructCollection(GameObject obj, string name)
        {
            var collection = obj.AddComponent<tk2dSpriteCollectionData>();
            UnityEngine.Object.DontDestroyOnLoad(collection);

            collection.assetName = name;
            collection.spriteCollectionGUID = name;
            collection.spriteCollectionName = name;
            collection.spriteDefinitions = new tk2dSpriteDefinition[0];
            return collection;
        }

        public static T CopyFrom<T>(this Component comp, T other) where T : Component
        {
            Type type = comp.GetType();
            if (type != other.GetType()) return null; // type mis-match
            PropertyInfo[] pinfos = type.GetProperties();
            foreach (var pinfo in pinfos)
            {
                if (pinfo.CanWrite)
                {
                    try
                    {

                        pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                    }
                    catch { }
                }
                else
                {
                }
            }

            FieldInfo[] finfos = type.GetFields();
            foreach (var finfo in finfos)
            {
                finfo.SetValue(comp, finfo.GetValue(other));
            }
            return comp as T;
        }

        public static void SetColor(this tk2dSprite sprite, Color color)
        {
            sprite.renderer.material.SetColor("_OverrideColor", color);
        }

        public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
        {
            return go.AddComponent<T>().CopyFrom(toAdd) as T;
        }
    }
}

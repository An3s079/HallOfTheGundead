using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dungeonator;
using System.Collections;

namespace EnemyBulletBuilder
{
    class BulletBuilder
    {
        /// <summary>
        /// initializes all the necessaary fake prefab hooks and lists
        /// </summary>
        public static void Init()
        {
            BulletBuilderFakePrefabHooks.Init();
            bulletEntries = new Dictionary<string, AIBulletBank.Entry>(); 
        }
        /// <summary>
        /// creates a fake prefab of an enemy bullet and adds it to the list of bullet entries. returns the fake prefab object    
        /// </summary>
        /// <param name="spritePath">the path for the sprite.</param>
        /// <param name="bulletEntryName">the name of the bullet in the bullet bank, and also the name by which you will instantiate the entry.</param>
        /// <param name="shouldRotate">makes the projectile able to rotate when shot, like lead maidens projectiles.</param>
        /// <param name="colliderGenerationMode">this is experimental, and should always be set to manual.</param>
        /// <param name="manualWidth">width of collider.</param>
        /// <param name="manualHeight">height of collider.</param>
        /// <param name="manualOffsetX">x offset of collider.</param>
        /// <param name="manualOffsetY">y offset of collider.</param>
        /// <param name="UsesGlow">enables or disables the glow of the bullet. true by default</param>
        public static GameObject CreateBulletPrefab(string spritePath, string bulletEntryName, bool shouldRotate = false,
            PixelCollider.PixelColliderGeneration colliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual, int manualWidth = 0, int manualHeight = 0, int manualOffsetX = 1, int manualOffsetY = 1, bool UsesGlow = true)
        {
            if (spritePath == null || bulletEntryName == null)
            {
                ETGModConsole.Log("BULLET BUILDER : sprite path or bullet name are null");
                return null;
            }
            if (bulletEntries.ContainsKey(bulletEntryName))
            {
                ETGModConsole.Log("BULLET BUILDER : "+bulletEntryName+" already exists in the database.");
                return null;
            }
            int spriteID = SpriteBuilder.AddSpriteToCollection(spritePath, ETGMod.Databases.Items.ProjectileCollection);
            GameObject bulletObject = null;
            if (shouldRotate)
            {
                bulletObject = UnityEngine.Object.Instantiate(EnemyDatabase.GetOrLoadByGuid("cd4a4b7f612a4ba9a720b9f97c52f38c").bulletBank.GetBullet().BulletObject);  
            }
            else
            {
                bulletObject = UnityEngine.Object.Instantiate(EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").bulletBank.GetBullet().BulletObject);
            }


            bulletObject.SetActive(false);
            BulletBuilderFakePrefab.MarkAsFakePrefab(bulletObject);
            BulletBuilderFakePrefab.DontDestroyOnLoad(bulletObject);

            Projectile bulletProjectile = bulletObject.GetComponent<Projectile>();
            bulletProjectile.sprite.SetSprite(ETGMod.Databases.Items.ProjectileCollection, spriteID);
            bulletProjectile.BulletScriptSettings.preventPooling = true;
            bulletProjectile.specRigidbody = bulletObject.GetComponent<SpeculativeRigidbody>();
            bulletProjectile.collidesWithEnemies = false;



            SpeculativeRigidbody body = bulletProjectile.specRigidbody;
            body.PrimaryPixelCollider.Sprite = bulletProjectile.sprite;
            body.PrimaryPixelCollider.ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual;
            body.PrimaryPixelCollider.ManualWidth = manualWidth;
            body.PrimaryPixelCollider.ManualHeight = manualHeight;
            body.PrimaryPixelCollider.ManualOffsetX = manualOffsetX;
            body.PrimaryPixelCollider.ManualOffsetY = manualOffsetY;
            body.UpdateCollidersOnRotation = shouldRotate;

            if (UsesGlow)
            {
                
                Material sharedMaterial = body.sprite.renderer.sharedMaterial;
                body.sprite.usesOverrideMaterial = true;
                Material material = new Material(ShaderCache.Acquire("Brave/LitTk2dCustomFalloffTintableTiltedCutoutEmissive"));
                material.SetTexture("_MainTex", sharedMaterial.GetTexture("_MainTex"));
                BulletBuilder.LerpMaterialGlow(material, 0f, 22f, 0.4f);
                material.SetFloat("_EmissiveColorPower", 8f);
                material.SetColor("_EmissiveColor", Color.red);
                body.sprite.renderer.material = material;
                
            }

            
            AIBulletBank.Entry bulletEntry = new AIBulletBank.Entry();
            bulletEntry.BulletObject = bulletObject;
            bulletEntry.Name = bulletEntryName;
            bulletEntry.ProjectileData = bulletProjectile.baseData;
            VFXPool muzzleFlashEffects = new VFXPool { type = VFXPoolType.None, effects = new VFXComplex[0] };
            bulletEntry.MuzzleFlashEffects = muzzleFlashEffects;
            bulletEntries.Add(bulletEntryName, bulletEntry);

            return bulletObject;            
        }
        public static void LerpMaterialGlow(Material targetMaterial, float startGlow, float targetGlow, float duration)
        {
            targetMaterial.SetFloat("_EmissivePower", Mathf.Lerp(startGlow, targetGlow, duration));
        }

        public static AIBulletBank.Entry GetBulletEntryByName(string BulletEntryName)
        {
            if (bulletEntries.ContainsKey(BulletEntryName))
            {
                return bulletEntries[BulletEntryName];
            }
            ETGModConsole.Log("BULLET BUILDER : custom bullet entry not found (wrong name?)");
            return null;
        }
        private static Dictionary<string, AIBulletBank.Entry> bulletEntries = null;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using System.Collections;
using Dungeonator;
using System.Reflection;
namespace HallOfGundead
{
    class BloodiedChamber : PassiveItem
    {
        public static int Init()
        {
            string itemName = "Bloodied Chamber";
            string resourceName = "HallOfGundead/Resources/BloodiedChamber";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<BloodiedChamber>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "nyoom";
            string longDesc = "A power taken from an anceint being who adored chambers. \n\nOnly a fragment of his true power.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "hotg");
            item.quality = PickupObject.ItemQuality.C;

            return item.PickupObjectId;
            
        }

        PropertyInfo prop;
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            
            player.PostProcessProjectile += this.postProcessProj;
        }

        public override DebrisObject Drop(PlayerController player)
        {

            DebrisObject debrisObject = base.Drop(player);
            player.PostProcessProjectile -= this.postProcessProj;
            return debrisObject;
        }

        private void postProcessProj(Projectile proj, float idk)
        {
            proj.OnHitEnemy += this.OnHitEnemy;
        }

        private void OnHitEnemy(Projectile proj, SpeculativeRigidbody enemy, bool fatal)
        {
            if(fatal)
            {
                RoomHandler currentRoom = Owner.CurrentRoom;
                var target = currentRoom.GetRandomActiveEnemy();
                Projectile projectile = ((Gun)ETGMod.Databases.Items[spapiGun.ItemID]).DefaultModule.projectiles[0];
                GameObject gameObject = Toolbox.SpawnProjectileTowardsPoint(projectile.gameObject, enemy.sprite.WorldCenter, target.sprite.WorldCenter);
                gameObject.AddComponent<PierceDeadActors>();
                var homin = gameObject.AddComponent<Toolbox.ModifiedHomingModifier>();
                homin.HomingRadius = float.PositiveInfinity;
                homin.AngularVelocity = 300;
                Projectile component = gameObject.GetComponent<Projectile>();
                if (component != null)
                {
                    component.shouldRotate = true;
                    component.Owner = Owner;
                    component.baseData.damage = 7f;
                    component.collidesWithPlayer = false;
                }
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

    }
}

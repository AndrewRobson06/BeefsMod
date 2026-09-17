using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Ranged.Projectiles
{
    public class ChlorophytePipeProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 24;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            //DrawOffsetX = -17;
            DrawOriginOffsetY = -10;
        }

        public override bool? CanDamage() => false;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);

            int animationSpeed = Math.Min(1 / 40, 3);

            if (Main.myPlayer == Projectile.owner)
            {
                Item heldItem = player.HeldItem;
                if (player.HasAmmo(heldItem) && !player.noItems && !player.CCed)
                {
                    float holdoutDistance = ChlorophytePipe.HoldOutDistance * Projectile.scale;
                    Vector2 holdoutOffset = holdoutDistance * Vector2.Normalize(Main.MouseWorld - playerCenter);
                    if (holdoutOffset.X != Projectile.velocity.X || holdoutOffset.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        // Calculate a spawn location, taking into account the muzzle placement and a random variation
                        var spawnLocation = playerCenter + holdoutOffset * 1.2f;
                        bool ammoConsumed = player.PickAmmo(heldItem, out int projToShoot, out float speed, out int damage, out float knockBack, out int usedAmmoItemId);

                        if (ammoConsumed)
                        {
                            var source = player.GetSource_ItemUse_WithPotentialAmmo(heldItem, usedAmmoItemId);
                            Projectile.NewProjectile(source, spawnLocation, Vector2.Normalize(Projectile.velocity) * speed, projToShoot, damage, knockBack, Projectile.owner);
                        }
                    }

                    
                    // Set the projectile velocity, which is actually the holdout offset for held projectiles.
                    Projectile.velocity = holdoutOffset;

                }
            }

            Projectile.direction = Projectile.velocity.X < 0 ? -1 : 1;
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.SetDummyItemTime(2);
            Projectile.Center = playerCenter;
            float rotationOffset = Projectile.spriteDirection == -1 ? MathHelper.Pi : 0;
            Projectile.rotation = Projectile.velocity.ToRotation() + rotationOffset;
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
            Projectile.timeLeft = 2;

            //Projectile.velocity.X *= 1f + Main.rand.Next(-3, 4) * 0.04f;
            //Projectile.velocity.Y += -10;

            //Vector2 aim = Main.MouseWorld - player.MountedCenter;
            //aim.Normalize();
            //player.ChangeDir(aim.X > 0 ? 1 : -1);
        }

        /*public void FireShot(Vector2 velocity)
        {
            Player player = Main.player[Projectile.owner];

            //SoundEngine.PlaySound(SoundID.Item40 with { Volume = 2.5f });

            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);

            Item heldItem = player.HeldItem;

            var spawnLocation = playerCenter;

            bool ammoConsumed = player.PickAmmo(heldItem, out int projToShoot, out float speed, out int damage, out float knockBack, out int usedAmmoItemId);


            if (Main.myPlayer == Projectile.owner)
            {
                if (ammoConsumed)
                {
                    var source = player.GetSource_ItemUse_WithPotentialAmmo(heldItem, usedAmmoItemId);

                    Projectile.NewProjectileDirect(source, player.MountedCenter, velocity * 1f, projToShoot, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    
                }

            }
            Projectile.Kill();
        }*/
    }
}

using BeefsMod.Content.Weapons.Ranged.Ammo;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Ranged.Projectiles
{
    public class OverclockedBallistaProjectile : ModProjectile
    {

        public ref float ChargeTimer => ref Projectile.ai[0];

        public const float MaxCharge = 300f;

        public int ArrowAmount = 1;
        public override void SetDefaults()
        {
            Projectile.width = 74;
            Projectile.height = 76;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);

            int animationSpeed = Math.Min(10 / 40, 3);

            if (Main.myPlayer == Projectile.owner)
            {
                Item heldItem = player.HeldItem;
                if (player.channel && player.HasAmmo(heldItem) && !player.noItems && !player.CCed)
                {
                    float holdoutDistance = UnstableBalista.HoldOutDistance * Projectile.scale;
                    Vector2 holdoutOffset = holdoutDistance * Vector2.Normalize(Main.MouseWorld - playerCenter);
                    if (holdoutOffset.X != Projectile.velocity.X || holdoutOffset.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
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

            Projectile.velocity.X *= 1f + Main.rand.Next(-3, 4) * 0.04f;
            Projectile.velocity.Y *= 1f + Main.rand.Next(-3, 4) * 0.05f;

            Vector2 aim = Main.MouseWorld - player.MountedCenter;
            aim.Normalize();
            player.ChangeDir(aim.X > 0 ? 1 : -1);

            if (player.channel)
            {
                if (ChargeTimer < MaxCharge)
                {
                    ChargeTimer++;

                    if (ChargeTimer % 3 == 0)
                    {
                        //add 1 arrow every 3 ticks so 20 a second
                        ArrowAmount++;
                        if (ArrowAmount > 100)
                            ArrowAmount = 100;
                    }

                    if (ChargeTimer == 30 || ChargeTimer == 60 || ChargeTimer == 90 || ChargeTimer == 120 ||
                        ChargeTimer == 150 || ChargeTimer == 180 || ChargeTimer == 210 || ChargeTimer == 240 || ChargeTimer == 270)
                        SoundEngine.PlaySound(SoundID.Unlock);

                    if (ChargeTimer == 30)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 5f));

                            Dust.NewDustPerfect(Projectile.Center, DustID.GlowingMushroom, velocity * 1.3f, 0, default, 1.8f).noGravity = true;
                        }
                    }

                    if (ChargeTimer == 60)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 7f));

                            Dust.NewDustPerfect(Projectile.Center, DustID.GlowingMushroom, velocity * 3f, 0, default, 1.8f).noGravity = true;
                        }
                    }

                    if (ChargeTimer == 90)
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i /11f));

                            Dust.NewDustPerfect(Projectile.Center, DustID.GlowingMushroom, velocity * 5f, 0, default, 1.8f).noGravity = true;
                        }
                    }

                    if (ChargeTimer == 120)
                    {
                        for (int i = 0; i < 13; i++)
                        {
                            Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 13f));

                            Dust.NewDustPerfect(Projectile.Center, DustID.GlowingMushroom, velocity * 8f, 0, default, 1.8f).noGravity = true;
                        }
                    }

                    if (ChargeTimer == 150)
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 15f));

                            Dust.NewDustPerfect(Projectile.Center, DustID.GlowingMushroom, velocity * 12f, 0, default, 1.8f).noGravity = true;
                        }
                    }

                    if (ChargeTimer == 180)
                    {
                        for (int i = 0; i < 19; i++)
                        {
                            Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 19f));

                            Dust.NewDustPerfect(Projectile.Center, DustID.GlowingMushroom, velocity * 16f, 0, default, 1.8f).noGravity = true;
                        }
                    }

                    if (ChargeTimer == 210)
                    {
                        for (int i = 0; i < 21; i++)
                        {
                            Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 21f));

                            Dust.NewDustPerfect(Projectile.Center, DustID.GlowingMushroom, velocity * 20f, 0, default, 1.8f).noGravity = true;
                        }
                    }

                    if (ChargeTimer == 240)
                    {
                        for (int i = 0; i < 23; i++)
                        {
                            Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 23f));

                            Dust.NewDustPerfect(Projectile.Center, DustID.GlowingMushroom, velocity * 24f, 0, default, 1.8f).noGravity = true;
                        }
                    }

                    if (ChargeTimer == 270)
                    {
                        for (int i = 0; i < 25; i++)
                        {
                            Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 25f));

                            Dust.NewDustPerfect(Projectile.Center, DustID.GlowingMushroom, velocity * 28f, 0, default, 1.8f).noGravity = true;
                        }
                    }

                    if (ChargeTimer == MaxCharge)
                    {
                        SoundEngine.PlaySound(SoundID.MaxMana);

                        for (int i = 0; i < 30; i++)
                        {
                            Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 30f));

                            Dust.NewDustPerfect(Projectile.Center, DustID.GlowingMushroom, velocity * 50f, 0, default, 2.3f).noGravity = true;
                        }
                    }
                }
            }
            else
            {
                FireShot(aim);
            }
        }

        public void FireShot(Vector2 velocity)
        {
            Player player = Main.player[Projectile.owner];

            SoundEngine.PlaySound(SoundID.Item40 with { Volume = 2.5f });

            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);

            Item heldItem = player.HeldItem;

            var spawnLocation = playerCenter;

            bool ammoConsumed = player.PickAmmo(heldItem, out int projToShoot, out float speed, out int damage, out float knockBack, out int usedAmmoItemId);

            //make it less powerful to spam click
            if (ChargeTimer < 10)
                ArrowAmount = 1;

            if (Main.myPlayer == Projectile.owner)
            {
                if (ammoConsumed)
                {
                    var source = player.GetSource_ItemUse_WithPotentialAmmo(heldItem, usedAmmoItemId);

                    for (int i = 0; i < ArrowAmount; i++)
                    {
                        Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(20));

                        newVelocity *= 1f - Main.rand.NextFloat(0.4f);

                        Projectile.NewProjectileDirect(source, player.MountedCenter, newVelocity * 25f, ModContent.ProjectileType<OverclockedBallistaArrow>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                    }
                }


            }
            Projectile.Kill();
        }
    }
}

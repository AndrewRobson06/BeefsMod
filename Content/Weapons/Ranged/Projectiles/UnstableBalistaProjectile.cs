using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace BeefsMod.Content.Weapons.Ranged.Projectiles
{
    public class UnstableBalistaProjectile : ModProjectile
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


            Projectile.Center = player.MountedCenter;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 1;

            Vector2 aim = Main.MouseWorld - player.MountedCenter;
            aim.Normalize();
            player.ChangeDir(aim.X > 0 ? 1 : -1);

            if (player.channel)
            {
                if (ChargeTimer < MaxCharge)
                {
                    ChargeTimer++;

                    if (ChargeTimer % 6 == 0)
                    {
                        //add 1 arrow every 6 ticks so 10 a second
                        ArrowAmount++;
                        if (ArrowAmount > 50)
                            ArrowAmount = 50;
                    } 

                    if (ChargeTimer == 60 || ChargeTimer == 120 || ChargeTimer == 180 || ChargeTimer == 240)
                        SoundEngine.PlaySound(SoundID.Unlock);

                    if (ChargeTimer == MaxCharge)
                    {
                        SoundEngine.PlaySound(SoundID.MaxMana);
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

            if (Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < ArrowAmount; i++)
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                    newVelocity *= 1f - Main.rand.NextFloat(0.4f);

                    Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), player.MountedCenter, newVelocity * 20f, ProjectileID.WoodenArrowFriendly, Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
                
            }
            Projectile.Kill();
        }
    }
}

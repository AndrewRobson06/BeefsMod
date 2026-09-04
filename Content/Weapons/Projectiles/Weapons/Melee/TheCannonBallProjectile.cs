using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static BeefsMod.Content.Weapons.Ranged.CrystalGrenadeProjectile;

namespace BeefsMod.Content.Weapons.Projectiles.Weapons.Melee
{
   
    public class TheCannonBallProjectile : ModProjectile
    {
        private int hitCounter = 0;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Type] = -1f;

            ProjectileID.Sets.YoyosMaximumRange[Type] = 300f;

            ProjectileID.Sets.YoyosTopSpeed[Type] = 11f;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.aiStyle = ProjAIStyleID.Yoyo;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.penetrate = -1;
        }



        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            hitCounter++;

            if (hitCounter == 5)
                Projectile.damage += 200;
            if (hitCounter >= 6)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                    ModContent.ProjectileType<TheCannonBallExplode>(), Type, Projectile.damage, 2, Projectile.owner);

                Projectile.damage -= 200;
                hitCounter = 1;
            }

            
            
        }

    }

    public class TheCannonBallExplode : ModProjectile 
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.SolarWhipSwordExplosion;

        private float Progress => Utils.Clamp(1 - Projectile.timeLeft / 10f, 0f, 1f);

        private float Radius => Projectile.ai[0] * Progress;

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;

            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }

        public override void AI()
        {
            Main.instance.LoadProjectile(85);

            SoundEngine.PlaySound(SoundID.Item14 with { Volume = 0.7f }, Projectile.Center);

            for (int i = 0; i < 13; i++)
            {
                Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(45f, 45f), DustID.Smoke,
                    Main.rand.NextVector2CircularEdge(2f, 2f), Main.rand.Next(50, 100), default, 2f).noGravity = true;

                Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(45f, 45f), DustID.Smoke,
                    Main.rand.NextVector2CircularEdge(2f, 2f), Main.rand.Next(50, 100), default, 2f).noGravity = true;

                Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(45f, 45f), DustID.Smoke,
                    Main.rand.NextVector2CircularEdge(1f, 3f), Main.rand.Next(50, 100), default, 2f).noGravity = true;
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 line = targetHitbox.Center.ToVector2() - Projectile.Center;
            line.Normalize();
            line *= Radius + 80;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + line);

        }
    }

}

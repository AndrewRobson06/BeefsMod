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

namespace BeefsMod.Content.Weapons.Magic.Projectiles
{
    public class VenomSludgeTomeProjectile : ModProjectile
    {
        public int fade = 255;
        public override void SetDefaults()
        {
            Projectile.width = 24; // The width of projectile hitbox
            Projectile.height = 24; // The height of projectile hitbox
            Projectile.aiStyle = ProjAIStyleID.Arrow; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 6000; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            //Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            //Projectile.usesLocalNPCImmunity = true;



            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }

        public override void AI()
        {
            if (Main.rand.NextBool(2))
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Venom);

                Dust dust = Main.dust[d];

                dust.noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item54 with { Volume = 1f }, Projectile.Center);

            for (int i = 0; i < 25; i++)
            {
                Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 25f));

                Dust.NewDustPerfect(Projectile.Center, DustID.Venom, velocity * 2f, 0, default, 1.5f).noGravity = true;
            }

            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 10f));

                Dust.NewDustPerfect(Projectile.Center, DustID.Venom, velocity * 2f, 0, default, 1.5f).noGravity = true;
            }

            target.AddBuff(BuffID.Venom, 420);
        }
    }
}

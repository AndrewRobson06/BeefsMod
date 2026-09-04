using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using BeefsMod.Content.Weapons.Melee;

namespace BeefsMod.Content.Weapons.Melee.Projectiles
{
    public class BloodlustSaberProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 300;

            Projectile.aiStyle = ProjAIStyleID.Beam;

        }

        public override void AI()
        {
            if (Main.rand.NextBool(2))
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood);

                Dust dust = Main.dust[d];

                dust.noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            SoundEngine.PlaySound(Terraria.ID.SoundID.Item171, Projectile.position);
            return true;
        }
    }
}

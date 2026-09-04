using BeefsMod.Content.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Melee.Projectiles
{
    public class VampireToothProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.scale = 0.7f;


            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 300;

            Projectile.aiStyle = ProjAIStyleID.ShortSword;
        }

        public override void AI()
        {
            base.AI();

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;

            int halfProjWidth = Projectile.width / 2;
            int halfProjHeight = Projectile.height / 2;

            DrawOriginOffsetX = 0;

            DrawOffsetX = -((32/ 2) - halfProjWidth);

            DrawOriginOffsetY = ((10 / 2) - halfProjHeight);

            if (Main.rand.NextBool(2))
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood);

                Dust dust = Main.dust[d];

                dust.noGravity = true;
            }

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDustPerfect(target.Center, DustID.Blood, Main.rand.NextVector2Circular(5f, 5f));
                }
                SoundEngine.PlaySound(Terraria.ID.SoundID.NPCHit19, target.Center);

                Item.NewItem(target.GetSource_OnHit(target), target.getRect(), ModContent.ItemType<HealingBlood>());
                
            }
        }
    }
}

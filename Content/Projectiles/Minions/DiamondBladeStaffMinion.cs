using BeefsMod.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Projectiles.Minions
{
    public class DiamondBladeStaffMinion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Smolstar);

            AIType = ProjectileID.Smolstar;

            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 8;
        }

        public override void PostAI()
        {
            Player player = Main.player[Projectile.owner];
            if(player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<DiamondBladeStaffBuff>());
                return;
            }
            if (player.HasBuff(ModContent.BuffType<DiamondBladeStaffBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Vanilla has several particles that can easily be used anywhere.
            // The particles from the Particle Orchestra are predefined by vanilla and most can not be customized that much.
            // Use auto complete to see the other ParticleOrchestraType types there are.
            // Here we are spawning the Excalibur particle randomly inside of the target's hitbox.
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                Projectile.owner);

            // You could also spawn dusts at the enemy position. Here is simple an example:
            // Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, ModContent.DustType<Content.Dusts.Sparkle>());

            // Set the target's hit direction to away from the player so the knockback is in the correct direction.
            hit.HitDirection = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);

            
        }

        /*public override void PostDraw(Color lightColor)
        {
            Main.instance.LoadProjectile(79);
            Texture2D starTex = TextureAssets.Projectile[79].Value;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);

            Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(-4.2f, 5).RotatedBy(0) - Main.screenPosition,
                null, new Color(249, 250, 252, 150), 0, starTex.Size() / 2f, 0.8f, 0f, 0f);
        }*/
    }
}

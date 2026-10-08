using BeefsMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Melee.Projectiles
{
    public class ElbaiteSpearProjectile : ModProjectile
    {
        protected virtual float HoldoutRangeMin => 24f;
        protected virtual float HoldoutRangeMax => 240f;

        public float rotation = 0;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Spear);

            AIType = ProjectileID.Spear;
        }

        public override bool PreAI()
        {

            Player player = Main.player[Projectile.owner];
            int duration = player.itemAnimationMax;

            player.heldProj = Projectile.whoAmI;

            if (Projectile.timeLeft > duration)
                Projectile.timeLeft = duration;

            Projectile.velocity = Vector2.Normalize(Projectile.velocity);

            float halfDuration = duration * 0.5f;
            float progress;

            if (Projectile.timeLeft < halfDuration)
            {
                progress = Projectile.timeLeft / halfDuration;
            }
            else
            {
                progress = (duration - Projectile.timeLeft) / halfDuration;
            }

            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin,
                Projectile.velocity * HoldoutRangeMax, progress);

            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.ToRadians(45f);
            }
            else
            {
                Projectile.rotation += MathHelper.ToRadians(135f);
            }

            if (!Main.dedServ)
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<ElbaiteDust>(),
                    Projectile.velocity.X * 2f, Projectile.velocity.Y * 2f, Alpha: 70, Scale: 0.8f);

            if (Main.rand.NextBool(10))
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<ElbaiteDust>(),
                    Alpha: 70, Scale: 0.2f);
            // too much dust might change later
            return false;

        }
        public override void PostDraw(Color lightColor) //something is making the players arm glow??? idk what but fix it later
        {

            rotation++;

            if (rotation > 360)
                rotation = 0;

            Main.instance.LoadProjectile(79);
            //Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            Texture2D starTex = TextureAssets.Projectile[79].Value;
            //Texture2D bloomTex = TextureAssets.Projectile[540].Value;

            //Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, lightColor, 0, tex.Size() / 2f, Projectile.scale, 0f, 0f);


            {
                //Main.spriteBatch.End();
                //Main.spriteBatch.Begin((SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix));

                Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(0, 0).RotatedBy(rotation) - Main.screenPosition,
                    null, new Color(185, 65, 101, 255), 0, starTex.Size() / 2f, 0.8f, 0f, 0f);
                Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(0, 0).RotatedBy(rotation) - Main.screenPosition,
                    null, new Color(128, 41, 67, 255), 0, starTex.Size() / 2f, 0.5f, 0f, 0f);

                /*Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(7, -5).RotatedBy(Projectile.rotation / 10) - Main.screenPosition,
                    null, new Color(255, 0, 131, 255), 0, starTex.Size() / 2f, 0.8f, 0f, 0f);
                Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(7, -5).RotatedBy(Projectile.rotation / 10) - Main.screenPosition,
                    null, new Color(204, 56, 132, 255), 0, starTex.Size() / 2f, 0.5f, 0f, 0f);*/

                //Main.spriteBatch.End();
                //Main.spriteBatch.Begin(default, default, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }
    }
}


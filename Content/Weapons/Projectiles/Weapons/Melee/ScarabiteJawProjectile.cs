using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Projectiles.Weapons.Melee
{
    public class ScarabiteJawProjectile : ModProjectile
    {
        protected virtual float HoldoutRangeMin => 24f;
        protected virtual float HoldoutRangeMax => 170f;

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
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BoneTorch,
                    Projectile.velocity.X * 2f, Projectile.velocity.Y * 2f, Alpha: 70, Scale: 1.2f);

            if (Main.rand.NextBool(1000))
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BoneTorch,
                    Alpha: 70, Scale: 0.8f);
            // too much dust might change later
            return false;

        }
        public override void PostDraw(Color lightColor) //something is making the players arm glow??? idk what but fix it later
        {
            Main.instance.LoadProjectile(79);
            //Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            Texture2D starTex = TextureAssets.Projectile[79].Value;
            Texture2D bloomTex = TextureAssets.Projectile[540].Value;

            //Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, lightColor, 0, tex.Size() / 2f, Projectile.scale, 0f, 0f);


            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);

                Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(7, 5).RotatedBy(Projectile.rotation / 10) - Main.screenPosition,
                    null, new Color(255, 0, 131, 255), 0, starTex.Size() / 2f, 0.8f, 0f, 0f);
                Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(7, 5).RotatedBy(Projectile.rotation / 10) - Main.screenPosition,
                    null, new Color(204, 56, 132, 255), 0, starTex.Size() / 2f, 0.5f, 0f, 0f);

                Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(7, -5).RotatedBy(Projectile.rotation / 10) - Main.screenPosition,
                    null, new Color(255, 0, 131, 255), 0, starTex.Size() / 2f, 0.8f, 0f, 0f);
                Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(7, -5).RotatedBy(Projectile.rotation / 10) - Main.screenPosition,
                    null, new Color(204, 56, 132, 255), 0, starTex.Size() / 2f, 0.5f, 0f, 0f);

                //Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(23, 0).RotatedBy(0) - Main.screenPosition,
                    //null, new Color(0, 185, 255, 100), 0, bloomTex.Size() / 2f, 0.4f, 0f, 0f);
                //Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(23, 0).RotatedBy(0) - Main.screenPosition,
                    //null, new Color(50, 171, 217, 100), 0, bloomTex.Size() / 2f, 0.2f, 0f, 0f);

                //Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                //null, new Color(22, 136, 119, 255), Projectile.rotation, starTex.Size() / 2f, 0.5f, 0f, 0f);

                //Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                //null, new Color(39, 117, 117, 255), Projectile.rotation, bloomTex.Size() / 2f, 0.75f, 0f, 0f);

                //Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                //null, new Color(249, 255, 211, 255), Projectile.rotation, bloomTex.Size() / 2f, 1f, 0f, 0f);

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(default, default, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom, 420);
        }
    }

    /*public class VenomSludgeBall : ModProjectile
    {
        public override string Texture => "BeefsMod/Content/Weapons/Projectiles/Weapons/Melee/VenomSludgeBall";
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 300;

            Projectile.aiStyle = ProjAIStyleID.;

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

            SoundEngine.PlaySound(Terraria.ID.SoundID.Item171, Projectile.position);
            return true;
        }

        public override void PostDraw(Color lightColor)
        {
            Main.instance.LoadProjectile(540);
            //Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            Texture2D bloomTex = TextureAssets.Projectile[540].Value;

            //Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, lightColor, 0, tex.Size() / 2f, Projectile.scale, 0f, 0f);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);
            Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(23, 0).RotatedBy(0) - Main.screenPosition,
                null, new Color(152, 117, 233, 100), 0, bloomTex.Size() / 2f, 0.4f, 0f, 0f);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(default, default, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);
        }
       
    } */ //ill come back to this maybe
      
}

    

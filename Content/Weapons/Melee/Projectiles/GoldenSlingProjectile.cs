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
    public class GoldenSlingProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Type] = 17f;

            ProjectileID.Sets.YoyosMaximumRange[Type] = 280f;

            ProjectileID.Sets.YoyosTopSpeed[Type] = 15f;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.aiStyle = ProjAIStyleID.Yoyo;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.penetrate = -1;
        }

        public override void PostAI()
        {
            if (Main.rand.NextBool(5))
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IchorTorch);

        }
        public override void PostDraw(Color lightColor)
        {
            Main.instance.LoadProjectile(540);
            Texture2D bloomTex = TextureAssets.Projectile[540].Value;

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);

            Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(0, 0).RotatedBy(0) - Main.screenPosition,
                   null, new Color(255, 179, 0, 150), 0, bloomTex.Size() / 2f, 0.4f, 0f, 0f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Ichor, 420);
        }
    }
}

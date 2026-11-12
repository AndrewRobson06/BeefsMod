using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rail;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

// made with help from https://youtu.be/KwFkJ8DPXgI?si=fTp1JUzOPKH4oMdz
// still needs fixing, not centered on player on use :(

namespace BeefsMod.Content.Weapons.Magic.Projectiles
{
    public class CarnyLifeProjectile : ModProjectile
    {
        //public override string Texture => "BeefsMod/Content/Weapons/Magic/CarnyLife";
        public override void SetStaticDefaults()
        {
            Projectile.width = 8; // The width of projectile hitbox
            Projectile.height = 8; // The height of projectile hitbox
            Projectile.aiStyle = ProjAIStyleID.Arrow; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Magic; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            //Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;



            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }
        /*
        public override void AI()
        {
            float num = 40f;
            float num2 = 2f;
            float quarterPi = -(float)Math.PI / 4;

            float scaleFactor = 10f;

            Player player = Main.player[Projectile.owner];

            Vector2 relativePoint = player.RotatedRelativePoint(player.MountedCenter);
            if (player.dead)
            {
                Projectile.Kill();
                return;
            }

            Lighting.AddLight(player.Center, 1f, 1f, 1f);

            int sign = Math.Sign(Projectile.velocity.X);

            Projectile.velocity = new Vector2(sign, 0f);

            if (Projectile.ai[0] == 0f)
            {
                Projectile.rotation = new Vector2(sign, 0f - player.gravDir).ToRotation() +
                    quarterPi + (float)Math.PI;
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.rotation -= (float)Math.PI / 2f;
                }
            }

            Projectile.ai[0] += 1f;

            Projectile.rotation += (float)Math.PI * 2f * num2 / num * (float)sign;

            bool isDone = Projectile.ai[0] == (num / 2f);
            if (Projectile.ai[0] >= num || (isDone && !player.controlUseItem))
            {
                Projectile.Kill();
                player.reuseDelay = 0;

            }
            else if (isDone)
            {
                Vector2 mouseWorld = Main.MouseWorld;
                int dir = (player.DirectionTo(mouseWorld).X > 0f) ? 1 : -1;
                if ((float)dir != Projectile.velocity.X)
                {
                    player.ChangeDir(dir);
                    Projectile.velocity = new Vector2(dir, 0f);
                    Projectile.netUpdate = true;
                    Projectile.rotation -= (float)Math.PI;
                }
            }

            float rotationValue = Projectile.rotation - (float)Math.PI / 4f * (float)sign;
            Vector2 positionVector = (rotationValue + (sign == 1 ? (float)Math.PI :
                0f)).ToRotationVector2() * (Projectile.ai[0] / num) * scaleFactor;

            Projectile.position = relativePoint - Projectile.Size / 2f;
            Projectile.position += positionVector;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;

            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = MathHelper.WrapAngle(Projectile.rotation);

        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        /*public override void PostDraw(Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Main.spriteBatch.Draw(texture, Projectile.Center - new Vector2(-20, -25) - Main.screenPosition,
                    null, default, Projectile.rotation, texture.Size() / 2f, 1f, 0f, 0f);
        }*/

    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using BeefsMod.Content.Buffs;

namespace BeefsMod.Content.Weapons.Summon.Projectiles
{
    public class ShadowflameWhipProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.IsAWhip[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.DefaultToWhip();
            Projectile.WhipSettings.Segments = 15;
            Projectile.WhipSettings.RangeMultiplier = 0.3f;
        }

        private float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        /*public override void PostAI()
        {
            List<Vector2> list = new List<Vector2>();
            Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int resSamples, out float flags);
            ProjectileLoader.DrawLine(Projectile, list);
            for (int i = 2; i < list.Count; i++)
            {
                Vector2 segmentPosition = list[1];

                if (Main.rand.NextBool(3))
                {
                    int dustIndex = Dust.NewDust(segmentPosition - new Vector2(4, 4), 8, 8, DustID.Shadowflame, 0f, 0f,
                        100, default, 1.5f);

                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 0.5f;
                }
            }
        }*/

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
            Projectile.damage = (int)(Projectile.damage * 0.5f);
            target.AddBuff(BuffID.ShadowFlame, 420);
            target.AddBuff(ModContent.BuffType<Buffs.ShadowflameWhipTag>(), 420);
        }

        private void DrawLine(List<Vector2> list)
        {
            Texture2D texture = TextureAssets.FishingLine.Value;
            Rectangle frame = texture.Frame();
            Vector2 origin = new Vector2(frame.Width / 2, 2);

            Vector2 pos = list[0];

            for (int i = 0; i < list.Count - 1; i++)
            {
                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;
                Color color = Lighting.GetColor(element.ToTileCoordinates(), new Color(123, 89, 234));
                Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color,
                        rotation, origin, scale, SpriteEffects.None, 0);



                pos += diff;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> list = new List<Vector2>();
            Projectile.FillWhipControlPoints(Projectile, list);

            DrawLine(list);

            //Main.DrawWhip_WhipBland(Projectile, list);
            // The code below is for custom drawing.
            // If you don't want that, you can remove it all and instead call one of vanilla's DrawWhip methods, like above.
            // However, you must adhere to how they draw if you do.

            // This is a copy of Main.DrawWhip_WhipBland
            // This drawing assumes that each frame is projectile is equal size.
            // For more control over drawing each segment, see ExampleWhipProjectileAdvanced.

            SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            int totalSegments = Projectile.WhipSettings.Segments; // The number of segments this whip has.

            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle frame = texture.Frame(1, 5);
            int frameHeight = frame.Height;
            frame.Height -= 2;
            Vector2 originalOrigin = frame.Size() / 2f;
            Vector2 pos = list[0];

            for (int i = 0; i < list.Count - 1; i++)
            {
                Vector2 origin = originalOrigin;
                float scale = 1f;

                // Handle
                if (i == 0)
                {
                    origin.Y -= 4f; // This will move where the handle is drawn so it can be more in the player's hand.
                }
                // Divide the middle of the whip (after the handle and before the head) by approximately 3 and use the middle segments in each third.
                // ExampleWhipProjectile has 20 segments, so the following will result in 1 handle, 6 segment 1s, 6 segment 2s, 6 segment 3s, and 1 head.
                else
                {
                    // First Segment
                    // At the start of the whip after the handle, the first segment is used.
                    int segmentToDraw = 1;

                    if (i > totalSegments / 3)
                    { // At 1/3 of the way across the whip, the second segment is used.
                      // Second Segment
                        segmentToDraw = 2;
                    }

                    if (i > 2 * (totalSegments / 3))
                    { // At 2/3 of the way across the whip, the third segment is used.
                      // Third segment
                        segmentToDraw = 3;
                    }

                    frame.Y = frameHeight * segmentToDraw; // Set the frame to the correct segment.
                }

                if (i == list.Count - 2)
                {
                    // This is the head of the whip.
                    frame.Y = frameHeight * 4;
                    // For a more impactful look, this scales the tip of the whip up when fully extended, and down when curled up.
                    Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out _, out _);
                    float t = Timer / timeToFlyOut;
                    scale = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, t, clamped: true) * Utils.GetLerpValue(0.9f, 0.7f, t, clamped: true));
                }

                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2; // This projectile's sprite faces down, so PiOver2 is used to correct rotation.
                Color color = Lighting.GetColor(element.ToTileCoordinates());

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, flip, 0);

                if (Main.rand.NextBool(17))
                    Dust.NewDust(pos - new Vector2(4, 4), 8, 8, DustID.Shadowflame, 0f, 0f,
                        50, default, 1.2f);

                pos += diff;
            }
            return false;
        }

    }
}

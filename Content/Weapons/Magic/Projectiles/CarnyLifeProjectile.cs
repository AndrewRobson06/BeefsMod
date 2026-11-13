using BeefsMod.Content.Dusts;
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
        public bool CanLockOn = false;

        private NPC HomingTarget
        {
            get => Projectile.ai[0] == 0 ? null : Main.npc[(int)Projectile.ai[0] - 1];
            set { Projectile.ai[0] = value == null ? 0 : value.whoAmI + 1; }
        }
        public override void SetDefaults()
        {
            //Projectile.width = 30; // The width of projectile hitbox
            //Projectile.height = 30; // The height of projectile hitbox
            //Projectile.aiStyle = ProjAIStyleID.Arrow; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Magic; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 2; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            //Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            //Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
            Projectile.Size = new Vector2(30);
            Projectile.scale = 0.7f;



            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }

        public override void AI()
        {
            float maxDetectRadius = 250f;

            //Projectile.rotation += 0.3f;

            if (Main.rand.NextBool(2))
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BoneTorch);

                Dust dust = Main.dust[d];

                dust.noGravity = true;
            }

            if (HomingTarget == null && CanLockOn == false)
            {
                Projectile.velocity.Y += Projectile.ai[0];

                if (Projectile.timeLeft < 580)
                    Projectile.velocity.Y += 0.50f;

                if (Projectile.velocity.Y > 12f)
                    Projectile.velocity.Y = 12f;

            } else if (HomingTarget == null & CanLockOn == true)
            {
                Projectile.velocity.Y += Projectile.ai[0];

                if (Projectile.timeLeft < 580)
                    Projectile.velocity.Y += 0.50f;

                if (Projectile.velocity.Y > 12f)
                    Projectile.velocity.Y = 12f;
            }


            if (CanLockOn == true)
            {
                // First, we find a homing target if we don't have one
                if (HomingTarget == null)
                {
                    HomingTarget = FindClosestNPC(maxDetectRadius);
                }

                // If we have a homing target, make sure it is still valid. If the NPC dies or moves away, we'll want to find a new target
                if (HomingTarget != null && !IsValidTarget(HomingTarget))
                {
                    HomingTarget = null;
                }

                // If we don't have a target, don't adjust trajectory
                if (HomingTarget == null)
                    return;

                // If found, we rotate the projectile velocity in the direction of the target.
                // We only rotate by 3 degrees an update to give it a smooth trajectory. Increase the rotation speed here to make tighter turns
                float length = Projectile.velocity.Length();
                float targetAngle = Projectile.AngleTo(HomingTarget.Center);
                Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(90)).ToRotationVector2() * length * 1.05f;
                Projectile.rotation = Projectile.velocity.ToRotation();
            }
            
        }
        

        // Finding the closest NPC to attack within maxDetectDistance range
        // If not found then returns null
        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            // Loop through all NPCs
            foreach (var target in Main.ActiveNPCs)
            {
                // Check if NPC able to be targeted.
                if (IsValidTarget(target))
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }
            return closestNPC;
        }

        public bool IsValidTarget(NPC target)
        {
            // This method checks that the NPC is:
            // 1. active (alive)
            // 2. chaseable (e.g. not a cultist archer)
            // 3. max life bigger than 5 (e.g. not a critter)
            // 4. can take damage (e.g. moonlord core after all it's parts are downed)
            // 5. hostile (!friendly)
            // 6. not immortal (e.g. not a target dummy)
            // 7. doesn't have solid tiles blocking a line of sight between the projectile and NPC
            return target.CanBeChasedBy() && Collision.CanHit(Projectile.Center, 1, 1, target.position, target.width, target.height);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 10f));

                    Dust.NewDustPerfect(Projectile.Center, DustID.BoneTorch, velocity * 2f, 0, default, 1.5f).noGravity = true;
                }

                SoundEngine.PlaySound(SoundID.Item110, Projectile.position);
                Projectile.Kill();
            }
            else
            {
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;

                    // so it gets an extra bit of hight after hitting the floor late
                    if (Projectile.velocity.Y < 0)
                    {
                        Projectile.velocity.Y += -6;
                    }
                    else
                    {
                        Projectile.velocity.Y += 6;
                    }
                    
                }
                Projectile.velocity *= 0.75f;
                Projectile.ai[0] = Projectile.ai[1];
                CanLockOn = true;

                SoundEngine.PlaySound(SoundID.Item110, Projectile.position);

                for (int i = 0; i < 10; i++)
                {
                    Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 10f));

                    Dust.NewDustPerfect(Projectile.Center, DustID.BoneTorch, velocity * 2f, 0, default, 1.5f).noGravity = true;
                }
            }
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 10f));

                Dust.NewDustPerfect(Projectile.Center, DustID.BoneTorch, velocity * 2f, 0, default, 1.5f).noGravity = true;
            }

            SoundEngine.PlaySound(SoundID.Item110, Projectile.position);

            Projectile.Kill();
        }


        /*public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(79);
            Texture2D starTex = TextureAssets.Projectile[79].Value;
            Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);

            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, lightColor * 2, Projectile.rotation, tex.Size() / 2f, Projectile.scale, 0f, 0f);

            Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(0, -10).RotatedBy(Projectile.rotation) - Main.screenPosition,
                    null, new Color(142, 93, 117, 500), Projectile.rotation, starTex.Size(), 0.6f, 0f, 0f);

            Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2().RotatedBy(Projectile.rotation) - Main.screenPosition,
                null, new Color(141, 183, 213, 500), Projectile.rotation, starTex.Size(), 0.4f, 0f, 0f);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(default, default, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);

            return false;
        }*/

    }
}
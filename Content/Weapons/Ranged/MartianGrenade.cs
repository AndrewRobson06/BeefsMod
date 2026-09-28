using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace BeefsMod.Content.Weapons.Ranged
{
    public class MartianGrenade : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16, 16);

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 91;
            Item.knockBack = 3f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;

            Item.useAnimation = 25;
            Item.useTime = 25;

            Item.shoot = ModContent.ProjectileType<MartianGrenadeProjectile>();
            Item.shootSpeed = 12f;

            Item.rare = ItemRarityID.Yellow;

            Item.noMelee = true;
            Item.noUseGraphic = true;

            //Item.consumable = true;
            //Item.stack = Item.maxStack = 9999;

        }
    }

    public class MartianGrenadeProjectile : ModProjectile
    {
        int deathTimer;
        public override string Texture => "BeefsMod/Content/Weapons/Ranged/MartianGrenade";

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.Size = new Vector2(12);

            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.penetrate = -1;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;

            Projectile.timeLeft = 1200;

            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (deathTimer > 0)
            {
                deathTimer--;
                if (deathTimer == 1)
                {
                    Explode();
                    SpawnHomingProjectile();
                    Projectile.Kill();
                }


                Projectile.velocity.Y += 0.25f;
                Projectile.velocity *= 0.8f;
                Projectile.rotation += Projectile.velocity.Length() * 0.1f;

                Dust.NewDustPerfect(Projectile.Center + new Vector2(-5, -5).RotatedBy(Projectile.rotation), DustID.Vortex, Vector2.Zero, 0, default, 1.5f).noGravity = true;

                return;
            }

            if (Projectile.timeLeft < 1180)
                Projectile.velocity.Y += 0.05f;

            if (Projectile.velocity.Y > 16f)
                Projectile.velocity.Y = 16f;

            Projectile.rotation += Projectile.velocity.Length() + 0.05f;

        }

        public override bool? CanDamage()
        {
            return deathTimer <= 0;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Unlock with { Volume = 0.4f }, Projectile.Center);

            deathTimer = 25;

            Projectile.velocity = -oldVelocity.RotatedByRandom(0.3f) * Main.rand.NextFloat(0.5f, 1f);
            Projectile.velocity += Vector2.UnitY * -5f;

            Projectile.tileCollide = false;

            for (int i = 0; i < 10; i++)
            {
                Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 10f));

                Dust.NewDustPerfect(Projectile.Center, DustID.Smoke, velocity * 2f, 0, default, 1.5f).noGravity = true;
            }

            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(SoundID.Unlock with { Volume = 0.4f }, Projectile.Center);

            deathTimer = 25;

            Projectile.velocity = -Projectile.velocity.RotatedByRandom(0.3f) * Main.rand.NextFloat(0.5f, 1f);
            Projectile.velocity += Vector2.UnitY * -5f;

            Projectile.tileCollide = false;

            for (int i = 0; i < 10; i++)
            {
                Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 10f));

                Dust.NewDustPerfect(Projectile.Center, DustID.Smoke, velocity * 2f, 0, default, 1.5f).noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(79);

            Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            Texture2D starTex = TextureAssets.Projectile[79].Value;
            Texture2D bloomTex = TextureAssets.Projectile[540].Value;

            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, tex.Size() / 2f, Projectile.scale, 0f, 0f);

            if (deathTimer > 0)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);

                float fade = deathTimer / 25f;

                Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                    null, new Color(110, 254, 125, 255) * fade, Projectile.rotation, starTex.Size() / 2f, 1f, 0f, 0f);

                Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                    null, new Color(0, 80, 0, 255) * fade, Projectile.rotation, starTex.Size() / 2f, 0.5f, 0f, 0f);

                Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                    null, new Color(255, 251, 166, 255) * fade * 0.35f, Projectile.rotation, bloomTex.Size() / 2f, 0.75f, 0f, 0f);

                Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                    null, new Color(209, 131, 159, 255) * fade * 0.4f, Projectile.rotation, bloomTex.Size() / 2f, 1f, 0f, 0f);

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(default, default, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);
            }

            return false;
        }

        internal void Explode()
        {
            Main.instance.LoadProjectile(85);

            SoundEngine.PlaySound(SoundID.Item92 with { Volume = 0.5f }, Projectile.Center);

            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MartianGrenadeExplosion>(),
                    Projectile.damage + 35, Projectile.knockBack * 1.5f, Projectile.owner, 100);
            }

            for (int i = 0; i < 13; i++)
            {
                Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(45f, 45f), DustID.Smoke,
                    Main.rand.NextVector2CircularEdge(2f, 2f), Main.rand.Next(50, 100), default, 2f).noGravity = true;

                Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(45f, 45f), DustID.Smoke,
                    Main.rand.NextVector2CircularEdge(2f, 2f), Main.rand.Next(50, 100), default, 2f).noGravity = true;

                Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(45f, 45f), DustID.Smoke,
                    Main.rand.NextVector2CircularEdge(1f, 3f), Main.rand.Next(50, 100), default, 2f).noGravity = true;
            }

            for (int i = 0; i < 2; i++)
            {
                Gore.NewGorePerfect(Projectile.GetSource_Death(), Projectile.Center
                    + Main.rand.NextVector2Circular(5f, 5f), Main.rand.NextVector2Circular(5f, 5f), GoreID.Smoke1, Main.rand.NextFloat(0.8f, 1.1f));

                Gore.NewGorePerfect(Projectile.GetSource_Death(), Projectile.Center
                    + Main.rand.NextVector2Circular(5f, 5f), Main.rand.NextVector2Circular(5f, 5f), GoreID.Smoke1, Main.rand.NextFloat(0.8f, 1.1f));

                Gore.NewGorePerfect(Projectile.GetSource_Death(), Projectile.Center
                    + Main.rand.NextVector2Circular(5f, 5f), Main.rand.NextVector2Circular(5f, 5f), GoreID.Smoke1, Main.rand.NextFloat(0.8f, 1.1f));
            }

        }

        internal void SpawnHomingProjectile()
        {
            int NumProjectiles = 3;

            if (Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < NumProjectiles; i++)
                {
                    Vector2 newVelocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(360));

                    newVelocity *= 10f - Main.rand.NextFloat(1.5f);

                    Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.position, newVelocity,
                        ModContent.ProjectileType<MartianGrenadeHomingProjectile>(), Projectile.damage - 30, Projectile.knockBack, Projectile.owner, 100);
                }
            }


        }
    }

    public class MartianGrenadeExplosion : ModProjectile
    {
        public override string Texture => "BeefsMod/Content/Weapons/Ranged/MartianGrenade";

        private float Progress => Utils.Clamp(1 - Projectile.timeLeft / 10f, 0f, 1f);

        private float Radius => Projectile.ai[0] * Progress;

        public override void SetDefaults()
        {
            Projectile.alpha = 255;

            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10;


            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
            for (int k = 0; k < 6; k++)
            {
                float rot = Main.rand.NextFloat(0, 6.28f);

                Dust.NewDustPerfect(Projectile.Center + Vector2.One.RotatedBy(rot) * Radius, DustID.Vortex,
                    Vector2.One.RotatedBy(rot) * 0.5f, 0, default, Main.rand.NextFloat(3f, 4f)).noGravity = true;

                Dust.NewDustPerfect(Projectile.Center + Vector2.One.RotatedBy(rot) * Radius, DustID.Vortex,
                    Vector2.One.RotatedBy(rot) * 0.5f + Main.rand.NextVector2Circular(2f, 2f), 50, default, Main.rand.NextFloat(0.5f, 1f));
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 line = targetHitbox.Center.ToVector2() - Projectile.Center;
            line.Normalize();
            line *= Radius + 55;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + line);

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 420);
        }
    }

    public class MartianGrenadeHomingProjectile : ModProjectile 
    {
        public override string Texture => "BeefsMod/Content/Weapons/Ranged/MartianGrenade";
        private NPC HomingTarget
        {
            get => Projectile.ai[0] == 0 ? null : Main.npc[(int)Projectile.ai[0] - 1];
            set { Projectile.ai[0] = value == null ? 0 : value.whoAmI + 1; }
        }

        public ref float DelayTimer => ref Projectile.ai[1];

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 24; // The width of projectile hitbox
            Projectile.height = 24; // The height of projectile hitbox
            Projectile.aiStyle = ProjAIStyleID.Arrow; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;



            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }

        public override void AI()
        {

            for (int i = 0; i < 3; i++)
            {
                Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 3f));

                Dust.NewDustPerfect(Projectile.Center, DustID.Vortex, velocity * 1.3f, 0, default, 1.5f).noGravity = true;
            }

            float maxDetectRadius = 500f;

            if (DelayTimer < 5)
            {
                DelayTimer += 1;
                return;
            }

            if (HomingTarget == null)
                HomingTarget = FindClosestNPC(maxDetectRadius);

            if (HomingTarget != null && !IsValidTarget(HomingTarget))
                HomingTarget = null;

            if (HomingTarget == null)
                return;

            float length = Projectile.velocity.Length();
            float targetAngle = Projectile.AngleTo(HomingTarget.Center);
            Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(6)).ToRotationVector2() * length;
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

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
            SoundEngine.PlaySound(SoundID.Item92 with { Volume = 0.6f }, Projectile.Center);

            for (int i = 0; i < 12; i++)
            {
                Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 12f));

                Dust.NewDustPerfect(Projectile.Center, DustID.Vortex, velocity * 2f, 0, default, 1.5f).noGravity = false;
            }

            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            /*for (int i = 0; i < 25; i++)
            {
                Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 25f));

                Dust.NewDustPerfect(Projectile.Center, DustID.Sandnado, velocity * 2f, 0, default, 1.5f).noGravity = true;
            }*/

            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 13; i++)
            {
                Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 13f));

                Dust.NewDustPerfect(Projectile.Center, DustID.Vortex, velocity * 2f, 0, default, 1.5f).noGravity = false;
            }

        }
    }

}

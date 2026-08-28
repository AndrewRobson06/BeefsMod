using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Audio;

namespace BeefsMod.Content.Weapons.Ranged
{
    internal class ShroomiteGrenade : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16, 16);

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 150;
            Item.knockBack = 3f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;

            Item.useAnimation = 40;
            Item.useTime = 40;

            Item.shoot = ModContent.ProjectileType<ShroomiteGrenadeProjectile>();
            Item.shootSpeed = 12f;

            Item.rare = ItemRarityID.Yellow;

            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.consumable = true;
            Item.stack = Item.maxStack = 9999;

        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.Grenade, 333)
                .AddIngredient(ItemID.ShroomiteBar, 2)
                .AddTile(TileID.MythrilAnvil)
                .Register();

        }
    }

    public class ShroomiteGrenadeProjectile : ModProjectile
    {
        int deathTimer;
        public override string Texture => "BeefsMod/Content/Weapons/Ranged/ShroomiteGrenade";

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
        }

        public override void AI()
        {
            if (deathTimer > 0)
            {
                deathTimer--;
                if (deathTimer == 1)
                {
                    Explode();
                    //Spore();
                    Projectile.Kill();
                }


                Projectile.velocity.Y += 0.25f;
                Projectile.velocity *= 0.8f;
                Projectile.rotation += Projectile.velocity.Length() * 0.1f;

                Dust.NewDustPerfect(Projectile.Center + new Vector2(-5, -5).RotatedBy(Projectile.rotation), DustID.BlueTorch, Vector2.Zero, 0, default, 1.5f).noGravity = true;

                return;
            }

            if (Projectile.timeLeft < 1180)
                Projectile.velocity.Y += 0.35f;

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
            Texture2D bloomTex = TextureAssets.Projectile[549].Value;

            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, tex.Size() / 2f, Projectile.scale, 0f, 0f);

            if (deathTimer > 0)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);

                float fade = deathTimer / 25f;

                Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                    null, new Color(44, 26, 233, 255) * fade, Projectile.rotation, starTex.Size() / 2f, 1f, 0f, 0f);

                Main.spriteBatch.Draw(starTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                    null, new Color(34, 21, 164, 255) * fade, Projectile.rotation, starTex.Size() / 2f, 0.5f, 0f, 0f);

                Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                    null, new Color(85, 89, 255, 255) * fade * 0.35f, Projectile.rotation, bloomTex.Size() / 2f, 0.75f, 0f, 0f);

                Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                    null, new Color(107, 213, 255, 255) * fade * 0.4f, Projectile.rotation, bloomTex.Size() / 2f, 1f, 0f, 0f);

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(default, default, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);
            }

            return false;
        }

        internal void Explode()
        {
            Main.instance.LoadProjectile(85);

            SoundEngine.PlaySound(SoundID.Item14 with { Volume = 0.5f }, Projectile.Center);

            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ShroomiteGrenadeExplosion>(),
                    Projectile.damage * 3, Projectile.knockBack * 1.5f, Projectile.owner, 250);
            }

            for (int i = 0; i < 130; i++)
            {
                Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(45f, 45f), DustID.Smoke,
                    Main.rand.NextVector2CircularEdge(70f, 100f), Main.rand.Next(50, 100), default, 2f).noGravity = true;

                Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(45f, 45f), DustID.Smoke,
                    Main.rand.NextVector2CircularEdge(50f, 60f), Main.rand.Next(50, 100), default, 2f).noGravity = true;

                Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(45f, 45f), DustID.Smoke,
                    Main.rand.NextVector2CircularEdge(30f, 40f), Main.rand.Next(50, 100), default, 2f).noGravity = true;

                Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(45f, 45f), DustID.Smoke,
                    Main.rand.NextVector2CircularEdge(10f, 20f), Main.rand.Next(50, 100), default, 2f).noGravity = true;
            }

            for (int i = 0; i < 10; i++)
            {
                Gore.NewGorePerfect(Projectile.GetSource_Death(), Projectile.Center
                    + Main.rand.NextVector2Circular(5f, 5f), Main.rand.NextVector2Circular(5f, 5f), GoreID.Smoke1, Main.rand.NextFloat(0.8f, 1.1f));

                Gore.NewGorePerfect(Projectile.GetSource_Death(), Projectile.Center
                    + Main.rand.NextVector2Circular(5f, 5f), Main.rand.NextVector2Circular(5f, 5f), GoreID.Smoke1, Main.rand.NextFloat(0.8f, 1.1f));

                Gore.NewGorePerfect(Projectile.GetSource_Death(), Projectile.Center
                    + Main.rand.NextVector2Circular(5f, 5f), Main.rand.NextVector2Circular(5f, 5f), GoreID.Smoke1, Main.rand.NextFloat(0.8f, 1.1f));
            }

        }

       /*internal void Spore()
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ShroomiteGrenadeSpore>(),
                    Projectile.damage / 2, Projectile.knockBack * 1.5f, Projectile.owner, 250);
            }

        }*/
    }

    public class ShroomiteGrenadeExplosion : ModProjectile
    {
        public override string Texture => "BeefsMod/Content/Weapons/Ranged/ShroomiteGrenade";

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
            for (int k = 0; k < 19; k++)
            {
                float rot = Main.rand.NextFloat(0, 6.28f);

                Dust.NewDustPerfect(Projectile.Center + Vector2.One.RotatedBy(rot) * Radius, DustID.GlowingMushroom,
                    Vector2.One.RotatedBy(rot) * 0.5f, 0, default, Main.rand.NextFloat(3f, 4f)).noGravity = true;

                Dust.NewDustPerfect(Projectile.Center + Vector2.One.RotatedBy(rot) * Radius, DustID.BlueTorch,
                    Vector2.One.RotatedBy(rot) * 0.5f + Main.rand.NextVector2Circular(2f, 2f), 50, default, Main.rand.NextFloat(0.5f, 1f));
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 line = targetHitbox.Center.ToVector2() - Projectile.Center;
            line.Normalize();
            line *= Radius + 100;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + line);

        }

    }

    /*public class ShroomiteGrenadeSpore : ModProjectile
    {
        int deathTimer = 25;
        public override string Texture => "BeefsMod/Content/Weapons/Ranged/ShroomiteGrenade";

        private float Progress => Utils.Clamp(1 - Projectile.timeLeft / 1f, 0f, 1f);

        private float Radius => Projectile.ai[0] * Progress;

        public override void SetDefaults()
        {
            Main.instance.LoadProjectile(590);
            Projectile.alpha = 0;

            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 25;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;

        }

        public override void AI()
        {
            if (deathTimer > 0)
                deathTimer--;

            Projectile.velocity = Vector2.Zero;

            float rot = Main.rand.NextFloat(0, 6.28f);

            if (Main.rand.NextBool(3))
            {
                Dust.NewDustPerfect(Projectile.Center + Vector2.One.RotatedBy(rot) * Radius, DustID.GlowingMushroom,
                    Vector2.One.RotatedBy(rot) * 0.5f, 150, default, 1000f).noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(540);

            Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            Texture2D sporeTex = TextureAssets.Projectile[540].Value;

            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, tex.Size() / 2f, Projectile.scale, 0f, 0f);

            if (deathTimer > 0)
            {
                float fade = deathTimer / 25;

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(default, BlendState.Additive, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);

                Main.spriteBatch.Draw(sporeTex, Projectile.Center - new Vector2(0,0).RotatedBy(Projectile.rotation) - Main.screenPosition,
                    null, new Color(44, 26, 233, 255) * fade * 3f, Projectile.rotation, sporeTex.Size() /2, 15f, 0f, 0f);

                Main.spriteBatch.Draw(sporeTex, Projectile.Center - new Vector2(0, 0).RotatedBy(Projectile.rotation) - Main.screenPosition,
                    null, new Color(8, 35, 97, 255) * fade * 3f, Projectile.rotation, sporeTex.Size() / 2, 7f, 0f, 0f);

                //Main.spriteBatch.Draw(sporeTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                //null, new Color(34, 21, 164, 100) * fade, Projectile.rotation, sporeTex.Size() / 2f, 0.5f, 0f, 0f);

                //Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                //null, new Color(85, 89, 255, 255) * fade * 0.35f, Projectile.rotation, bloomTex.Size() / 2f, 0.75f, 0f, 0f);

                //Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(5, 5).RotatedBy(Projectile.rotation) - Main.screenPosition,
                //null, new Color(107, 213, 255, 255) * fade * 0.4f, Projectile.rotation, bloomTex.Size() / 2f, 1f, 0f, 0f);

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(default, default, default, default, default, null, Main.GameViewMatrix.TransformationMatrix);
            }

            return false;
        } */
    }



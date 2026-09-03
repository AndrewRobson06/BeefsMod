using BeefsMod.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Projectiles.Minions
{
    public class DiamondBladeStaffMinion : ModProjectile
    {
        const int MAX_DIST = 900 * 900;
        public enum MinionState
        {
            Idle = 0,
            FlyToTarget = 1,

            Dash = 2
        }

        public int Timer
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public MinionState State
        {
            get => (MinionState)Projectile.ai[1];
            set => Projectile.ai[1] = (float)value;
        }

        public int TargetWhoAmI
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }

        Vector2 attackOffset;

        public NPC Target => Owner.MinionAttackTargetNPC > 0 && Main.npc[Owner.MinionAttackTargetNPC].CanBeChasedBy(this)
            && Owner.DistanceSQ(Main.npc[Owner.MinionAttackTargetNPC].Center) < MAX_DIST ? Main.npc[Owner.MinionAttackTargetNPC] :
            (TargetWhoAmI < 0 ? null : Main.npc[TargetWhoAmI]);

        public Player Owner => Main.player[Projectile.owner];

        public override void SetStaticDefaults()
        {
            Main.projPet[Type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Type] = true;
            ProjectileID.Sets.MinionSacrificable[Type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(18);
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.ArmorPenetration = 5;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override bool MinionContactDamage()
        {
            return State == MinionState.Dash;
        }

        public override void AI()
        {
            if (Owner.HasBuff<DiamondBladeStaffBuff>())
                Projectile.timeLeft = 2;

            switch (State) 
            {
                case MinionState.Idle:
                    Idle();
                    break;

                case MinionState.FlyToTarget:
                    FlyToTarget();
                    break;

                case MinionState.Dash:
                    Dash();
                    break;
            }

        }

        internal void Idle()
        {
            if (Target is null || !Target.active)
                TargetWhoAmI = GetTargetIndex();

            Vector2 idlePosition = Owner.Center + new Vector2(0f * Owner.direction - 25 * Projectile.minionPos * Owner.direction, -50 - 12 * Projectile.minionPos);

            float distance = Vector2.Distance(Projectile.Center, idlePosition);

            Vector2 toIdlePosition = idlePosition - Projectile.Center;

            Projectile.rotation = 0;

            if (toIdlePosition.Length() < 0.0001f)
            {
                toIdlePosition = Vector2.Zero;
            }
            else
            {
                float speed = 100f;
                if (speed < 1000f)
                    speed = MathHelper.Lerp(25f, 45f, distance / 1000f);
                if (speed < 100f)
                    speed = MathHelper.Lerp(0.1f, 5f, distance / 100f);

                toIdlePosition.Normalize();
                toIdlePosition *= speed;
            }

            Projectile.velocity = (Projectile.velocity * 24f + toIdlePosition) / 25f;

            if (distance > 2000f)
            {
                Projectile.Center = idlePosition;
                Projectile.velocity = Main.rand.NextVector2Circular(1f, 1f);
                Projectile.netUpdate = true;
            }

            if (Target is not null)
            {
                Timer = -60;
                attackOffset = Main.rand.NextVector2CircularEdge(100f, 100f);

                State = MinionState.FlyToTarget;
            }
        }

        internal void FlyToTarget()
        {
            if (Target is null  || !Target.CanBeChasedBy(this) || Target.DistanceSQ(Projectile.Center) > MAX_DIST)
            {
                Timer = 0;
                TargetWhoAmI = -1;
                State = MinionState.Idle;
                return;
            }

            Vector2 targetPosition = Target.Center + attackOffset;
            Vector2 direction = Projectile.DirectionTo(targetPosition);

            direction *= 10f;
            Projectile.velocity = Vector2.Lerp(Projectile.velocity, direction, 0.09f);

            Projectile.rotation += Projectile.velocity.Length() * 0.03f;
            Projectile.rotation += MathHelper.Lerp(0.01f, 0.5f, 1f - Math.Abs(Timer) / 60);

            const int dist = 50 * 50;

            if ((Projectile.DistanceSQ(targetPosition) < dist || ++ Timer > 0) && Projectile.DistanceSQ(targetPosition) < dist * 2)
            {
                Timer = 0;
                State = MinionState.Dash;

                Vector2 dashDistance = Projectile.DirectionTo(Target.Center);
                Projectile.velocity = dashDistance * Main.rand.NextFloat(5f, 6.5f);
            }
        }

        internal void Dash()
        {
            if (Target is null || !Target.CanBeChasedBy(this) || Target.DistanceSQ(Projectile.Center) > MAX_DIST)
            {
                Timer = 0;
                TargetWhoAmI = -1;
                State = MinionState.Idle;
                return;
            }

            Vector2 targetPosition = Target.Center + attackOffset;

            Vector2 direction = targetPosition - Projectile.Center;

            float targetAngle = direction.ToRotation();

            Projectile.rotation = targetAngle + MathHelper.PiOver2;

            Timer++;

            if (Timer > 1)
                Projectile.velocity *= 1.02f;
            Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(45f, 45f), DustID.GemDiamond, default, 255, default, 0).noGravity = true;


            if (Timer > 20)
            {
                attackOffset = Main.rand.NextVector2CircularEdge(100f, 100f);

                State = MinionState.FlyToTarget;
                Timer = -60;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var tex = ModContent.Request<Texture2D>(Texture).Value;
            Texture2D bloomTex = TextureAssets.Projectile[540].Value;

            SpriteBatch sb = Main.spriteBatch;

            sb.Draw(tex, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, tex.Size() / 2f,
                Projectile.scale, 0f, 0f);

            Main.spriteBatch.Draw(bloomTex, Projectile.Center - new Vector2(0, 1).RotatedBy(Projectile.rotation) - Main.screenPosition,
                null, new Color(64, 63, 118, 175), 0, bloomTex.Size() / 2f, 0.43f, 0f, 0f);

            return true;
        }
        internal int GetTargetIndex()
        {
            NPC target = Main.npc.Where(n => n.CanBeChasedBy(this) &&
                n.DistanceSQ(Projectile.Center) < MAX_DIST).OrderBy(n => n.DistanceSQ(Projectile.Center)).FirstOrDefault();

            if (target == null)
                return -1;

            return target.whoAmI;
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
    }
}

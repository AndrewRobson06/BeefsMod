using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Buffs
{
    public class ShadowflameWhipTag : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }

    public class ShadowflameWhipTagGlobalNPC : GlobalNPC
    {
        public override void ModifyHitByProjectile(NPC target, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff<ShadowflameWhipTag>() && (projectile.minion ||
                projectile.sentry || ProjectileID.Sets.MinionShot[projectile.type]))
                    modifiers.FlatBonusDamage += 8;
        }
    }
}

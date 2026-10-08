using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Buffs
{
    public class ElbaiteWhipTag : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }

    public class ElbaiteWhipTagGlobalNPC : GlobalNPC
    {
        public override void ModifyHitByProjectile(NPC target, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff<ShadowflameWhipTag>() && (projectile.minion ||
                projectile.sentry || ProjectileID.Sets.MinionShot[projectile.type]))
                modifiers.FlatBonusDamage += 6;
        }
    }
}

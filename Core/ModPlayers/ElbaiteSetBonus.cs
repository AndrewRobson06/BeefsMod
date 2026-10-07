using BeefsMod.Content.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BeefsMod.Content.Buffs;

namespace BeefsMod.Core.ModPlayers
{
    public class ElbaiteSetBonus : ModPlayer
    {
        public bool hasElbaite;

        public override void ResetEffects()
        {
            hasElbaite = false;
        }
    }

    public class ElbaiteTagGlobal : GlobalNPC
    {
        public override void ModifyHitByProjectile(NPC target, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff<ElbaiteTagBuff>() && (projectile.minion ||
                projectile.sentry || ProjectileID.Sets.MinionShot[projectile.type]))
                modifiers.FlatBonusDamage += 9;
        }
    }

    public class ElbaiteApplyTag : GlobalItem
    {
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.GetModPlayer<ElbaiteSetBonus>().hasElbaite && hit.DamageType == DamageClass.Melee)
                target.AddBuff(ModContent.BuffType<ElbaiteTagBuff>(), 420);
        }
    }

    public class ElbaiteApplyTagProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.player[projectile.owner].GetModPlayer<ElbaiteSetBonus>().hasElbaite && projectile.CountsAsClass(DamageClass.Melee))
                target.AddBuff(ModContent.BuffType<ElbaiteTagBuff>(), 420);
        }
    }
}
  

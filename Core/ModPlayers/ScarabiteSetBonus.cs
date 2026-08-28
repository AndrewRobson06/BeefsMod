using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Core.ModPlayers
{
    public class ScarabiteSetBonus : ModPlayer
    {
        public bool hasScarabite;

        public override void ResetEffects()
        {
            hasScarabite = false;
        }
    }

    public class ScarabiteApplyVenomGlobal : GlobalItem
    {
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.GetModPlayer<ScarabiteSetBonus>().hasScarabite)
                target.AddBuff(BuffID.Venom, 420);
        }
    }

    public class ScarabiteApplyVenomGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.player[projectile.owner].GetModPlayer<ScarabiteSetBonus>().hasScarabite)
                target.AddBuff(BuffID.Venom, 420);
        }   

        }
    }



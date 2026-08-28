using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using BeefsMod.Content.Weapons.Ranged.Projectiles;

namespace BeefsMod.Core.GlobalNPCs
{
    public class DamageOverTimeGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool ScarabiteJavelinDebuff;

        public override void ResetEffects(NPC npc)
        {
            ScarabiteJavelinDebuff = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (ScarabiteJavelinDebuff)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // Count how many ExampleJavelinProjectile are attached to this npc.
                int ScarabiteJavelinCount = 0;
                foreach (var p in Main.ActiveProjectiles)
                {
                    if (p.type == ModContent.ProjectileType<ScarabiteJavelinProjectile>() && p.ai[0] == 1f && p.ai[1] == npc.whoAmI)
                    {
                        ScarabiteJavelinCount++;
                    }
                }
                // Remember, lifeRegen affects the actual life loss, damage is just the text.
                // The logic shown here matches how vanilla debuffs stack in terms of damage numbers shown and actual life loss.
                npc.lifeRegen -= ScarabiteJavelinCount * 2 * 3;
                if (damage < ScarabiteJavelinCount * 9)
                {
                    damage = ScarabiteJavelinCount * 9;
                }
            }
        }
    }
}

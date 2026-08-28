using BeefsMod.Content.Weapons.Melee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace BeefsMod.Content.NPCs
{
    internal class MyGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.Vampire){

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VampireTooth>(),30, 1, 1));
            }

            if (npc.type == NPCID.BloodNautilus)
            {

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BloodlustSaber>(), 2, 1, 1));
            }

        }
    }
}

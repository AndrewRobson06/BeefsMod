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

namespace BeefsMod.Core.GlobalNPCs
{
    internal class AddDrops : GlobalNPC // chanceDemoninator calc is 100 / X
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

            if (npc.type == NPCID.PirateDeckhand)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheCannonBall>(), 180, 1, 1));

            if (npc.type == NPCID.PirateCorsair)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheCannonBall>(), 180, 1, 1));

            if (npc.type == NPCID.PirateDeadeye)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheCannonBall>(), 180, 1, 1));

            if (npc.type == NPCID.PirateCrossbower)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheCannonBall>(), 180, 1, 1));

            if (npc.type == NPCID.PirateCaptain)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheCannonBall>(), 50, 1, 1));

            if (npc.type == NPCID.PirateShip)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheCannonBall>(), 10, 1, 1));

            if (npc.type == NPCID.Pumpking)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PumpkinHarvester>(), 35, 1, 1));


        }
    }
}

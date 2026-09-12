using BeefsMod.Content.Accessories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Core.GlobalNPCs
{
    public class AddToShop : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.ArmsDealer)
                shop.Add(ModContent.ItemType<MuzzleBrake>());
        }
    }
}

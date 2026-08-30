using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BeefsMod.Content.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class CouncilHorseEars : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;

            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(silver: 75);
            Item.vanity = true;
            Item.maxStack = 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LadyBug)
                .AddIngredient(ItemID.Daybloom, 3)
                .AddIngredient(ItemID.Moonglow, 3)
                .AddIngredient(ItemID.Silk, 10)
                .AddTile(TileID.Loom)
                .Register();
        }

    }

}
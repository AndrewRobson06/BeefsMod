using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static BeefsMod.Content.Tiles.PargasiteStoneBlock;

namespace BeefsMod.Content.Accessories
{
    public class PargasiteCharm : ModItem
    {
        public static readonly int ManaCostReductionPercent = 12;
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;

            Item.value = Item.sellPrice(gold: 12, silver: 19);

            Item.rare = ItemRarityID.LightRed;

            Item.accessory = true;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaCost -= ManaCostReductionPercent / 100f;
            player.huntressAmmoCost90 = true;
            hideVisual = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AdamantiteBar, 8)
                .AddIngredient(ModContent.ItemType<PargasiteItem>(), 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TitaniumBar, 8)
                .AddIngredient(ModContent.ItemType<PargasiteItem>(), 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

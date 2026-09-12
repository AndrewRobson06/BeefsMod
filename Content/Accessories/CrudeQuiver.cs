using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Accessories
{
    public class CrudeQuiver : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 38;

            Item.value = Item.sellPrice(silver: 60);

            Item.rare = ItemRarityID.Blue;

            Item.accessory = true;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.arrowDamage += 0.5f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 15)
                .AddRecipeGroup("Wood", 10)
                .AddIngredient(ItemID.IronBar, 8)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.Silk, 15)
                .AddRecipeGroup("Wood", 10)
                .AddIngredient(ItemID.LeadBar, 8)
                .AddTile(TileID.Anvils)
                .Register();

        }
    }
}


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
            hideVisual = true;
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

            //add a modded recipe to magic quiver prob in wrong spot so move this code later
            Recipe recipe = Recipe.Create(ItemID.MagicQuiver);
            recipe.AddIngredient(ModContent.ItemType<CrudeQuiver>());
            recipe.AddIngredient(ItemID.FallenStar, 15);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();


        }
    }
}


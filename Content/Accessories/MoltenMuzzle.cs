using BeefsMod.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Accessories
{
    public class MoltenMuzzle : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 20;

            Item.value = Item.buyPrice(gold: 3, silver: 20);

            Item.rare = ItemRarityID.Orange;

            Item.accessory = true;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.bulletDamage += 0.6f;
            player.GetModPlayer<MoltenMuzzleBonus>().hasMoltenMuzzle = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<MuzzleBrake>()
                .AddIngredient(ItemID.MagmaStone)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}



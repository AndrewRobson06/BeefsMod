using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class PygmyShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;

            Item.value = Item.sellPrice(gold: 5, silver: 75);

            Item.rare = ItemRarityID.Lime;

            Item.defense = 1;
            Item.accessory = true;
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.maxMinions += 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CobaltShield)
                .AddIngredient(ItemID.PygmyNecklace)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
                
        }
    }
}

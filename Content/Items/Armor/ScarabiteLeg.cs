using BeefsMod.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BeefsMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    internal class ScarabiteLeg : ModItem
    {
        public static readonly int MoveSpeedBonus = 7;
        public static readonly int CritBonus = 9;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CritBonus, MoveSpeedBonus);

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += CritBonus;
            player.moveSpeed += MoveSpeedBonus / 100f; //7% increase movement speed
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ScarabiteBarItem>(18)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
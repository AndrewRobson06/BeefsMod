using BeefsMod.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BeefsMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class ScarabiteChest : ModItem
    {
        public static readonly int CritBonus = 7;
        public static readonly int DamageBonus = 7;
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 22, silver: 80);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += CritBonus;
            player.GetDamage(DamageClass.Generic) += DamageBonus / 100f; //4% increase damage

        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ScarabiteBarItem>(24)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

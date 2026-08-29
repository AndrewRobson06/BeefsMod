using BeefsMod.Content.Tiles;
using BeefsMod.Core.ModPlayers;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BeefsMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class ScarabiteMelee : ModItem
    {
        public static readonly int CritBonus = 8;
        public static readonly int DamageBonus = 20;
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 11, silver: 40);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 22;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ScarabiteChest>() && legs.type == ModContent.ItemType<ScarabiteLeg>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Attacks inflict venom";
            player.GetModPlayer<ScarabiteSetBonus>().hasScarabite = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Melee) += CritBonus;
            player.GetDamage(DamageClass.Melee) += DamageBonus / 100f; //20% increase damage
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ScarabiteBarItem>(12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

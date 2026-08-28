using BeefsMod.Content.Tiles;
using BeefsMod.Core.ModPlayers;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BeefsMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class ScarabiteMagic : ModItem
    {
        public static readonly int CritBonus = 4;
        public static readonly int DamageBonus = 20;

        public static readonly int ManaCostReductionPercent = 19;
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 12;
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
            player.GetCritChance(DamageClass.Ranged) += CritBonus;
            player.GetDamage(DamageClass.Ranged) += DamageBonus / 100f; //20% increase damage
            player.manaCost -= ManaCostReductionPercent / 100f;
            player.statManaMax2 += 100;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static BeefsMod.Content.Tiles.ElbaiteStoneBlock;

namespace BeefsMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class ElbaiteLegs : ModItem
    {
        //public override string Texture => "BeefsMod/Content/Placeholder";
        public static readonly int AttackSpeedBonus = 12;
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 14, silver: 48);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Melee) += AttackSpeedBonus / 100f; //12% bonus
            player.maxMinions += 1;

        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AdamantiteBar, 16)
                .AddIngredient(ModContent.ItemType<ElbaiteItem>(), 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TitaniumBar, 16)
                .AddIngredient(ModContent.ItemType<ElbaiteItem>(), 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

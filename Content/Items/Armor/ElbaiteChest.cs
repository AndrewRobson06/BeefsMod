using BeefsMod.Content.Tiles;
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
    [AutoloadEquip(EquipType.Body)]
    public class ElbaiteChest : ModItem
    {
        public override string Texture => "BeefsMod/Content/Placeholder";
        public static readonly int DamageBonus = 8;
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 17, silver: 20);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += DamageBonus / 100f; //8% increase damage
            player.GetDamage(DamageClass.Summon) += DamageBonus / 100f; //8% increase damage
            player.maxMinions += 1;

        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AdamantiteBar, 20)
                .AddIngredient(ModContent.ItemType<ElbaiteItem>(), 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TitaniumBar, 20)
                .AddIngredient(ModContent.ItemType<ElbaiteItem>(), 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

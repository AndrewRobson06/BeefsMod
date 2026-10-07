using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static BeefsMod.Content.Tiles.ElbaiteStoneBlock;

namespace BeefsMod.Content.Accessories
{
    public class ElbaiteAmulet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;

            Item.value = Item.sellPrice(gold: 12, silver: 19);

            Item.rare = ItemRarityID.LightRed;

            Item.accessory = true;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += 8 / 100f;
            player.GetDamage(DamageClass.Summon) += 8 / 100f;
            player.GetAttackSpeed(DamageClass.Melee) += 8 / 100f;
            hideVisual = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AdamantiteBar, 8)
                .AddIngredient(ModContent.ItemType<ElbaiteItem>(), 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TitaniumBar, 8)
                .AddIngredient(ModContent.ItemType<ElbaiteItem>(), 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

using BeefsMod.Core.ModPlayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static BeefsMod.Content.Tiles.ElbaiteStoneBlock;
using BeefsMod.Core.ModPlayers;

namespace BeefsMod.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ElbaiteHead : ModItem
    {
        //public override string Texture => "BeefsMod/Content/Placeholder";

        public static readonly int AttackSpeedBonus = 6;

        public static readonly int DamageBonus = 12;
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 10, silver: 40);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 13;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ElbaiteChest>() && legs.type == ModContent.ItemType<ElbaiteLegs>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Melee damage applies 9 summon tag damage";
            player.GetModPlayer<ElbaiteSetBonus>().hasElbaite = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Melee) += AttackSpeedBonus / 100f; //6% bonus
            player.GetDamage(DamageClass.Melee) += DamageBonus / 100f; //12% increase damage
            player.GetDamage(DamageClass.Summon) += DamageBonus / 100f; //12% increase damage

        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AdamantiteBar, 10)
                .AddIngredient(ModContent.ItemType<ElbaiteItem>(), 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TitaniumBar, 10)
                .AddIngredient(ModContent.ItemType<ElbaiteItem>(), 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

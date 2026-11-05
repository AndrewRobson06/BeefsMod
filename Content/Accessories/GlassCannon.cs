using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Accessories
{
    public class GlassCannon : ModItem
    {
        public override string Texture => "BeefsMod/Content/Placeholder";
        public float DamageBonus = 50;
        public int CritBonus = 15;
        public float DamageTakenIncrease = -100;
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 44;

            Item.value = Item.sellPrice(silver: 60);

            Item.rare = ItemRarityID.LightRed;

            Item.accessory = true;
            Item.defense = -50;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += DamageBonus / 100; //50%
            player.GetCritChance(DamageClass.Generic) += CritBonus; //15%
            player.endurance = DamageTakenIncrease / 100; //100%
            player.AddBuff(BuffID.PotionSickness, -1, true, false);

            hideVisual = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AdamantiteBar, 15)
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddIngredient(ItemID.Glass, 20)
                .AddIngredient(ItemID.Bone, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TitaniumBar, 15)
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddIngredient(ItemID.Glass, 20)
                .AddIngredient(ItemID.Bone, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();

        }
    }
}

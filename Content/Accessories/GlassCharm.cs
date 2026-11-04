using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Accessories
{
    public class GlassCharm : ModItem
    {
        public override string Texture => "BeefsMod/Content/Placeholder";
        public float DamageBonus = 25;
        public int CritBonus = 10;
        public float DamageTakenIncrease = -55;
            public override void SetDefaults()
            {
                Item.width = 36;
                Item.height = 38;

                Item.value = Item.sellPrice(silver: 60);

                Item.rare = ItemRarityID.Blue;

                Item.accessory = true;
                Item.defense = -20;

            }

            public override void UpdateAccessory(Player player, bool hideVisual)
            {
                player.GetDamage(DamageClass.Generic) += DamageBonus / 100; //25%
                player.GetCritChance(DamageClass.Generic) += CritBonus; //10%
                player.endurance = DamageTakenIncrease / 100; //55%
            
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

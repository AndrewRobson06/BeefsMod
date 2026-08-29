using BeefsMod.Content.Tiles;
using BeefsMod.Content.Items.Tools.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Items.Tools
{
    public class ScarabiteDrill : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsDrill[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee;
            Item.width = 50;
            Item.height = 18;
            Item.useTime = 4;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0.5f;
            Item.value = Item.buyPrice(gold: 17, silver: 10);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item23;
            Item.shootSpeed = 32f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.tileBoost = -1;

            Item.shoot = ModContent.ProjectileType<ScarabiteDrillProjectile>();


            Item.pick = 200;
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
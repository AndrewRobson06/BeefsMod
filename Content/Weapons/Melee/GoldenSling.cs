using BeefsMod.Content.Weapons.Melee.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Melee
{
    public class GoldenSling : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;

            Item.damage = 38;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 2.3f;
            Item.channel = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 5, silver: 35);

            Item.shoot = ModContent.ProjectileType<GoldenSlingProjectile>();
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HelFire)
                .AddIngredient(ItemID.Ichor, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}

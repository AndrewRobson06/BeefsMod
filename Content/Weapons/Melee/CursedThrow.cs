using BeefsMod.Content.Weapons.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Melee
{
    public class CursedThrow : ModItem
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

            Item.damage = 44;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 2.3f;
            Item.channel = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 5, silver: 20);

            Item.shoot = ModContent.ProjectileType<CursedThrowProjectile>();
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HelFire)
                .AddIngredient(ItemID.CursedFlame, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}

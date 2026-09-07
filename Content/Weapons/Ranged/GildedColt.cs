using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Ranged
{
    public class GildedColt : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 24;

            Item.rare = ItemRarityID.Blue;

            Item.value = Item.sellPrice(gold: 3, silver: 39);

            Item.useTime = 8;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.reuseDelay = 20;
            Item.consumeAmmoOnLastShotOnly = true;
            Item.autoReuse = false;
            Item.UseSound = SoundID.Item11;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 7;
            Item.knockBack = 6f;
            Item.noMelee = true;

            Item.shoot = ProjectileID.PurificationPowder; //i dunno why but do this
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item11 with { Volume = 0.7f });
            Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 12)
                .AddIngredient(ItemID.DemoniteBar, 5)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 12)
                .AddIngredient(ItemID.CrimtaneBar, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}

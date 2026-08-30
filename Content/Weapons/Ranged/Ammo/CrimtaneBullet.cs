using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BeefsMod.Content.Weapons.Ranged.Ammo;

namespace BeefsMod.Content.Weapons.Ranged.Ammo
{
    public class CrimtaneBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
            Item.knockBack = 1.5f;
            Item.value = 10;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<CrimtaneBulletProjectile>(); // The projectile that weapons fire when using this item as ammunition.
            Item.shootSpeed = 5f; // The speed of the projectile. This value equivalent to Silver Bullet since ExampleBullet's Projectile.extraUpdates is 1.
            Item.ammo = AmmoID.Bullet; // The ammo class this ammo belongs to.
        }

        public override void AddRecipes()
        {
            CreateRecipe(70)
                .AddIngredient(ItemID.MusketBall, 70)
                .AddIngredient(ItemID.CrimtaneBar)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}

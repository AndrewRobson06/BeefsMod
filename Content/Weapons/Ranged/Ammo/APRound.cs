using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BeefsMod.Content.Weapons.Ranged.Ammo;

namespace BeefsMod.Content.Weapons.Ranged.Ammo
{
    public class APRound : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.HighVelocityBullet);
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 16;
            Item.height = 36;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
            Item.knockBack = 1.5f;
            Item.value = Item.sellPrice(gold: 2, silver: 49);
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ModContent.ProjectileType<APRoundProjectile>(); // The projectile that weapons fire when using this item as ammunition.
            Item.shootSpeed = 10f; // The speed of the projectile. This value equivalent to Silver Bullet since ExampleBullet's Projectile.extraUpdates is 1.
            Item.ammo = AmmoID.Bullet; // The ammo class this ammo belongs to.
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.EmptyBullet, 100)
                .AddIngredient(ItemID.ChlorophyteBar, 2)
                .AddIngredient(ItemID.AdamantiteBar)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe(100)
                .AddIngredient(ItemID.EmptyBullet, 100)
                .AddIngredient(ItemID.ChlorophyteBar, 2)
                .AddIngredient(ItemID.TitaniumBar)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

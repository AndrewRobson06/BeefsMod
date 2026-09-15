using BeefsMod.Content.Weapons.Ranged.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Ranged
{
    public class UnstableBalista : ModItem
    {


        public const int HoldOutDistance = 25;

        public override void SetDefaults()
        {
            Item.width = 74;
            Item.height = 76;

            Item.rare = ItemRarityID.LightPurple;

            Item.value = Item.sellPrice(gold: 30, silver:20);

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 70;
            Item.knockBack = 6f;
            Item.noMelee = true;
            Item.channel = true;
            Item.useAmmo = AmmoID.Arrow;
            Item.consumeAmmoOnFirstShotOnly = true;

            Item.shoot = ModContent.ProjectileType<UnstableBalistaProjectile>(); //i dunno why but do this
            Item.shootSpeed = 10f;
            Item.noUseGraphic = true;
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<UnstableBalistaProjectile>();

            velocity = Vector2.Normalize(velocity) * HoldOutDistance;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 20)
                .AddIngredient(ItemID.AdamantiteBar, 15)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 20)
                .AddIngredient(ItemID.TitaniumBar, 15)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

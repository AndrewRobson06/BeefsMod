using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BeefsMod.Content.Projectiles.Minions;
using BeefsMod.Content.Buffs;


namespace BeefsMod.Content.Weapons.Summon
{
    public class DiamondBladeStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.knockBack = 0.5f;
            Item.width = 46;
            Item.height = 46;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item44;
            Item.ArmorPenetration = 5;

            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<DiamondBladeStaffBuff>();
            Item.shoot = ModContent.ProjectileType<DiamondBladeStaffMinion>();
            Item.shootSpeed = 2f;

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.MouseWorld;
            player.LimitPointToPlayerReachableArea(ref position);

            player.AddBuff(ModContent.BuffType<DiamondBladeStaffBuff>(), 2);

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, -1);

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Diamond, 5)
                .AddIngredient(ItemID.GoldBar, 8)
                .AddIngredient(ItemID.DemoniteBar, 3)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.Diamond, 5)
                .AddIngredient(ItemID.GoldBar, 8)
                .AddIngredient(ItemID.CrimtaneBar, 3)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.Diamond, 5)
                .AddIngredient(ItemID.PlatinumBar, 8)
                .AddIngredient(ItemID.DemoniteBar, 3)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.Diamond, 5)
                .AddIngredient(ItemID.PlatinumBar, 8)
                .AddIngredient(ItemID.CrimtaneBar, 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}

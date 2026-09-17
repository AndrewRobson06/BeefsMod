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
using Microsoft.Xna.Framework;

namespace BeefsMod.Content.Weapons.Ranged
{
    public class ChlorophytePipe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 24;

            Item.rare = ItemRarityID.Lime;

            Item.value = Item.sellPrice(gold: 18, silver: 39);

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item63;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 65;
            Item.knockBack = 6f;
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.shoot = ProjectileID.PurificationPowder; //i dunno why but do this
            Item.useAmmo = AmmoID.Dart;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.15f; // 15% chance to not consume ammo
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Blowgun)
                .AddIngredient(ItemID.ChlorophyteBar, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

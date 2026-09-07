using BeefsMod.Content.Weapons.Magic.Projectiles;
using BeefsMod.Content.Weapons.Ranged.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace BeefsMod.Content.Weapons.Magic
{
    public class VenomSludgeTome : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;

            Item.rare = ItemRarityID.Orange;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 60;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item43;
            Item.knockBack = 1f;
            Item.value = 250000; //sells for about 5 gold 50 silver
            Item.shootSpeed = 3f;
            Item.shoot = ModContent.ProjectileType<VenomSludgeTomeProjectile>();
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 12;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SpellTome)
                .AddIngredient(ItemID.VialofVenom, 25)
                .AddTile(TileID.Bookcases)
                .Register();
        }
    }
}

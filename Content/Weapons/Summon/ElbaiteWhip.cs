using BeefsMod.Content.Weapons.Summon.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static BeefsMod.Content.Tiles.ElbaiteStoneBlock;

namespace BeefsMod.Content.Weapons.Summon
{
    public class ElbaiteWhip : ModItem
    {
        public static readonly int ElbaiteWhipTagDamage = 6;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ElbaiteWhipTagDamage);

        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<ElbaiteWhipProjectile>(), 37, 2, 22);

            Item.rare = ItemRarityID.LightRed;

            Item.value = Item.sellPrice(gold: 6, silver: 75);

            //Item.useTime = 12;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float swingDirection = 0.6f + (0.4f * Main.rand.NextFloat());

            if (Main.rand.NextBool(3))
            {
                swingDirection *= -2.5f;
            }

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, swingDirection);
            return false;
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ElbaiteItem>(15)
                .AddTile(TileID.MythrilAnvil)
                .Register();

        }
    }
}

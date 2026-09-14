using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace BeefsMod.Content.Weapons.Ranged
{
    public class ShroomiteRepeater : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 32;

            Item.rare = ItemRarityID.Yellow;

            Item.value = Item.sellPrice(gold: 15);

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item5;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 60;
            Item.knockBack = 6f;
            Item.noMelee = true;

            Item.shoot = ProjectileID.PurificationPowder; //i dunno why but do this
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }


        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.20f; // 20% chance to not consume ammo
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ShroomiteBar, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

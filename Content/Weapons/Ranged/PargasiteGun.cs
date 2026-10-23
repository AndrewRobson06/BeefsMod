using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static BeefsMod.Content.Tiles.PargasiteStoneBlock;

namespace BeefsMod.Content.Weapons.Ranged
{
    public class PargasiteGun : ModItem
    {
        public int timer = 0;
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 32;

            Item.rare = ItemRarityID.LightRed;

            Item.value = Item.sellPrice(gold: 3, silver: 39);

            Item.useTime = 30;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 54;
            Item.knockBack = 6f;
            Item.noMelee = true;

            Item.shoot = ProjectileID.PurificationPowder; //i dunno why but do this
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //lowk buggy but works
            if (timer < 30)
            {
                SoundEngine.PlaySound(SoundID.Item14 with { Volume = 0.5f });
                Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
                Item.useTime = 30;
                
            }

            if (timer == 30)
            {
                SoundEngine.PlaySound(SoundID.Item14 with { Volume = 0.5f });
                Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
                Item.useTime = 8;
                timer = 0;
            }
                
            return false;
        }

        public override void UpdateInventory(Player player)
        {
            timer++;

            if (timer > 30)
                timer = 30;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AdamantiteBar, 12)
                .AddIngredient(ModContent.ItemType<PargasiteItem>(), 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TitaniumBar, 12)
                .AddIngredient(ModContent.ItemType<PargasiteItem>(), 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(5f, -0.5f);
        }
    }
}

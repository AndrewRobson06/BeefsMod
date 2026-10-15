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
using static BeefsMod.Content.Tiles.PargasiteStoneBlock;

namespace BeefsMod.Content.Weapons.Ranged
{
    public class PargasiteBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 32;

            Item.rare = ItemRarityID.LightRed;

            Item.value = Item.sellPrice(gold: 11, silver: 76);

            Item.useTime = 3;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.reuseDelay = 20;
            Item.consumeAmmoOnLastShotOnly = true;
            Item.autoReuse = false;
            //Item.UseSound = SoundID.Item11;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 50;
            Item.knockBack = 6f;
            Item.noMelee = true;

            Item.shoot = ProjectileID.PurificationPowder; //i dunno why but do this
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item5 with { Volume = 0.7f });
            Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
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
    }
}

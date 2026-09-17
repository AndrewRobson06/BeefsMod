using BeefsMod.Content.Weapons.Ranged.Projectiles;
using Microsoft.Xna.Framework;
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
    public class ChlorophytePipe : ModItem
    {
        public const int HoldOutDistance = 25;
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 24;

            Item.rare = ItemRarityID.Lime;

            Item.value = Item.sellPrice(gold: 18, silver: 39);

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item63;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 65;
            Item.knockBack = 6f;
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            //Item.shoot = ProjectileID.PurificationPowder;
            Item.shoot = ModContent.ProjectileType<ChlorophytePipeProjectile>(); //i dunno why but do this
            Item.useAmmo = AmmoID.Dart;
            Item.noUseGraphic = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<ChlorophytePipeProjectile>();

            velocity = Vector2.Normalize(velocity) * HoldOutDistance;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);

            return true;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.15f; // 15% chance to not consume ammo
        }

        /*public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Type].Value;
            spriteBatch.Draw(texture, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f),
                new Rectangle(0, 0, texture.Width, texture.Height), Color.White, rotation, texture.Size(), scale, SpriteEffects.None, 0f);
            
            return true;
        }*/

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

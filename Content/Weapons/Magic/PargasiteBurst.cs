using BeefsMod.Content.Weapons.Magic.Projectiles;
using Microsoft.Xna.Framework;
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
using static BeefsMod.Content.Tiles.PargasiteStoneBlock;

namespace BeefsMod.Content.Weapons.Magic
{
    public class PargasiteBurst : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }
        public override void SetDefaults()
        {
            //Item.DefaultToStaff(ModContent.ProjectileType<PargasiteBurstProjectile>(), 16, 25, 12);
            Item.width = 48;
            Item.height = 48;

            Item.rare = ItemRarityID.LightRed;

            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            //Item.reuseDelay = 50;
            //Item.consumeAmmoOnLastShotOnly = true;
            Item.autoReuse = false;
            //Item.UseSound = SoundID.Item36;

            Item.DamageType = DamageClass.Magic;
            Item.damage = 23;
            Item.knockBack = 6f;
            Item.noMelee = true;

            Item.shoot = ProjectileID.PurificationPowder; //i dunno why but do this
            Item.shootSpeed = 10f;
            //Item.useAmmo = AmmoID.Bullet;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
           SoundEngine.PlaySound(SoundID.Item43 with { Volume = 0.7f });

            int NumProjectiles = Main.rand.Next(3, 4);

            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(8));

                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                Projectile.NewProjectileDirect(source, position, newVelocity, ModContent.ProjectileType<PargasiteBurstProjectile>(), damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 1f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<PargasiteItem>(), 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0f, 0f);
        }
    }
}

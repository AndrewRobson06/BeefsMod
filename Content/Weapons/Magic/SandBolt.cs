using BeefsMod.Content.Weapons.Magic.Projectiles;
using BeefsMod.Content.Weapons.Ranged.Projectiles;
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


namespace BeefsMod.Content.Weapons.Magic
{
    public class SandBolt : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;

            Item.rare = ItemRarityID.Yellow;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 80;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item60;
            Item.knockBack = 4.5f;
            Item.value = Item.sellPrice(gold: 8);
            Item.shootSpeed = 15f;
            Item.shoot = ModContent.ProjectileType<SandBoltProjectile>();
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 9;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            int NumProjectiles = 3;

            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));

                //newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}

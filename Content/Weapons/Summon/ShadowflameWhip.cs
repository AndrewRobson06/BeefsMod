using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Items;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using BeefsMod.Content.Weapons.Summon.Projectiles;

namespace BeefsMod.Content.Weapons.Summon
{
    public class ShadowflameWhip : ModItem
    {
        public static readonly int ShadowflameWhipTagDamage = 8;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ShadowflameWhipTagDamage);

        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<ShadowflameWhipProjectile>(), 44, 2, 27);

            Item.rare = ItemRarityID.Pink;

            Item.value = Item.sellPrice(gold: 2);
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
    }
}

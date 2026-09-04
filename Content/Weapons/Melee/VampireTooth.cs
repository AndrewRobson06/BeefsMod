using BeefsMod.Content.Weapons.Melee.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Melee
{
    internal class VampireTooth : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(32, 32);

            Item.rare = ItemRarityID.LightPurple;

            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.damage = 80;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.shootSpeed = 2.1f;
            Item.value = 150000; //sells for about 5 gold 50 silver
            Item.shoot = ModContent.ProjectileType<VampireToothProjectile>();
            Item.autoReuse = true;


        }


        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += 16;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
            {
                Item.NewItem(player.GetSource_OnHit(target), target.getRect(), ModContent.ItemType<HealingBlood>());

            }
        }

    }

    public class HealingBlood : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(12, 12);
        }

        public override bool OnPickup(Player player)
        {
            player.Heal(10);
            SoundEngine.PlaySound(Terraria.ID.SoundID.Item171, Item.position);
            return false;
        }
    }
}

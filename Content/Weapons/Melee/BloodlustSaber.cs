using BeefsMod.Content.Weapons.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;

namespace BeefsMod.Content.Weapons.Melee
{
    internal class BloodlustSaber : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(80, 80);

            Item.rare = ItemRarityID.LightRed;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 100;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useTurn = true;
            Item.UseSound = SoundID.NPCHit9;
            Item.knockBack = 6;
            Item.value = 250000; //sells for about 5 gold 50 silver
            Item.shootsEveryUse = false;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<BloodlustSaberProjectile>();
            Item.autoReuse = true;

            


        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                int d = Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.Blood);

                Dust dust = Main.dust[d];

                dust.noGravity = true;
            }
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                int damageAmount = 5;

                player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} bled to death using the Bloodloss Blade, What a dummy."), damageAmount, 0, true, false, 0, false, 10000);
            }

            return true;
        }

    }
}

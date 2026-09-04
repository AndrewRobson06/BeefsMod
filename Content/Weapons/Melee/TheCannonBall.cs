using BeefsMod.Content.Weapons.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Melee
{
    public class TheCannonBall : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;

            Item.damage = 50;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 4.5f;
            Item.channel = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 3, silver: 60);

            Item.shoot = ModContent.ProjectileType<TheCannonBallProjectile>();
            Item.shootSpeed = 8f;
        }

    }
}

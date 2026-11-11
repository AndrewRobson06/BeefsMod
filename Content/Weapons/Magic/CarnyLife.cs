using BeefsMod.Content.Weapons.Magic.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Magic
{
    public class CarnyLife : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

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
            Item.shoot = ModContent.ProjectileType<CarnyLifeProjectile>();
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 9;
        }



    }
}

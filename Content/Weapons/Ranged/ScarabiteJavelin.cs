using BeefsMod.Content.Tiles;
using BeefsMod.Content.Weapons.Ranged.Projectiles;
using Microsoft.Xna.Framework;
using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Ranged
{
    public class ScarabiteJavelin : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(silver: 5); //change later
            Item.maxStack = 9999;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.damage = 70;
            Item.knockBack = 3f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;

            Item.shootSpeed = 22f;
            Item.shoot = ModContent.ProjectileType<ScarabiteJavelinProjectile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(333)
                .AddIngredient<ScarabiteBarItem>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

}

       



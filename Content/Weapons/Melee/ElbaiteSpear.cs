using BeefsMod.Content.Tiles;
using BeefsMod.Content.Weapons.Melee.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static BeefsMod.Content.Tiles.ElbaiteStoneBlock;

namespace BeefsMod.Content.Weapons.Melee
{
    public class ElbaiteSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SkipsInitialUseSound[Type] = true;
            ItemID.Sets.Spears[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 11, silver: 76);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;

            Item.damage = 66;
            Item.knockBack = 3f;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;

            Item.shootSpeed = 3.7f;
            Item.shoot = ModContent.ProjectileType<ElbaiteSpearProjectile>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool? UseItem(Player player)
        {
            if (!Main.dedServ && Item.UseSound.HasValue)
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AdamantiteBar, 12)
                .AddIngredient<ElbaiteItem>(8)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TitaniumBar, 12)
                .AddIngredient<ElbaiteItem>(8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using BeefsMod.Content.Tiles;
using BeefsMod.Content.Weapons.Projectiles.Weapons.Melee;

namespace BeefsMod.Content.Weapons.Melee
{
    public class ScarabiteJaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SkipsInitialUseSound[Type] = true;
            ItemID.Sets.Spears[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 5);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;

            Item.damage = 70;
            Item.knockBack = 2.5f;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;

            Item.shootSpeed = 3.7f;
            Item.shoot = ModContent.ProjectileType<ScarabiteJawProjectile>();
            //Item.shoot = ModContent.ProjectileType<VenomSludgeBall>();
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
                .AddIngredient<ScarabiteBarItem>(12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

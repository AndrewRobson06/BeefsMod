using BeefsMod.Content.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Items.Tools
{
    public class ScarabitePickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 17, silver: 10);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.pick = 200;
            Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(10))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.BoneTorch);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ScarabiteBarItem>(18)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

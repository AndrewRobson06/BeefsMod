using BeefsMod.Content.Weapons.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Melee
{
    internal class GemCutter : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(64, 64);

            Item.rare = ItemRarityID.Blue;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 36;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item1;
            Item.ArmorPenetration = 10;
            Item.knockBack = 7;
            Item.value = 30000;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                int d = Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.BlueCrystalShard);

                Dust dust = Main.dust[d];

                dust.noGravity = true;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.StoneBlock, 50)
                .AddIngredient(ItemID.GoldBar, 10)
                .AddIngredient(ItemID.Diamond, 2)
                .AddIngredient(ItemID.Ruby, 1)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.StoneBlock, 50)
                .AddIngredient(ItemID.PlatinumBar, 10)
                .AddIngredient(ItemID.Diamond, 2)
                .AddIngredient(ItemID.Ruby, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(Terraria.ID.SoundID.Item50, target.position);
        }

    }
}

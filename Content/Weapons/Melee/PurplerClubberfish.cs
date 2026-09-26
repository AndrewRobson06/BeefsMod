using BeefsMod.Content.Tiles;
using BeefsMod.Content.Weapons.Melee.Projectiles;
using BeefsMod.Content.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Weapons.Melee
{
    public class PurplerClubberfish : ModItem //help from example mod
    {
        public int slamCooldown = 0;

        public bool soundPlayed = true;
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.damage = 70;
            Item.knockBack = 8f;
            Item.height = 62;
            Item.scale = 1.6f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 11, silver: 40); // Sell price is 5 times less than the buy price.
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<PurplerClubberfishProjectile>();
            //Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
            Item.autoReuse = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, number: player.whoAmI); // Sync the changes in multiplayer.

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void UpdateInventory(Player player)
        {
            if (slamCooldown != 0)
                slamCooldown--;

            if (slamCooldown == 0 && soundPlayed == false)
            {
                SoundEngine.PlaySound(SoundID.MaxMana);
                soundPlayed = true;
            }

                

        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            if (Collision.SolidCollision(hitbox.BottomLeft(), hitbox.Width, hitbox.Height - 99)) //hitbox.height is subbed by item.height * item.scale with decimal removed
            {
                if (slamCooldown == 0)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 30f));

                        Dust.NewDustPerfect(Item.position, DustID.Corruption, velocity * 20f, 0, default, 4f).noGravity = true;
                    }

                    for (int i = 0; i < 25; i++)
                    {
                        Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 25f));

                        Dust.NewDustPerfect(Item.position, DustID.Corruption, velocity * 15f, 0, default, 4f).noGravity = true;
                    }

                    for (int i = 0; i < 50; i++)
                    {
                        Vector2 velocity = Vector2.One.RotatedBy(MathHelper.TwoPi * (i / 30f));

                        Dust.NewDustPerfect(Item.position, DustID.Corruption, velocity * 10f, 0, default, 4f).noGravity = true;
                    }

                    slamCooldown = 120; // 2 seconds
                    SoundEngine.PlaySound(SoundID.Item14);
                    soundPlayed = false;
                }




            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PurpleClubberfish)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.SoulofNight, 15)
                .AddTile(TileID.MythrilAnvil) // This includes both the Mythril and Orichalcum Anvils.
                .Register();
        }

    }
}

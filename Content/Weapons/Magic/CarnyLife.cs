using BeefsMod.Content.Weapons.Magic.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static BeefsMod.Content.Weapons.Magic.SpinUseStylePlayer;

//thank you example mod 
namespace BeefsMod.Content.Weapons.Magic
{
    public class CarnyLife : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

            Item.DamageType = DamageClass.Magic;
            Item.damage = 30;
            Item.knockBack = 4f;

            Item.useAnimation = 20;
            Item.useTime = 20;

            Item.noMelee = true;
            //Item.noUseGraphic = true;
            Item.channel = true;
            Item.autoReuse = true;

            Item.useStyle = SpinUseStyleGlobalItem.SpinUseStyle;

            Item.shootSpeed = 15f;
            Item.UseSound = SoundID.Item1;

            //SoundEngine.PlaySound(SoundID.Item109);
            Item.shoot = ProjectileID.PurificationPowder; //i dunno why but do this
            //Item.shoot = ModContent.ProjectileType<CarnyLifeProjectile>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item4 with { Volume = 0.4f });

            Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<CarnyLifeProjectile>(), damage, knockback, player.whoAmI);
            
            return false;
        }

    }

    public class SpinUseStylePlayer : ModPlayer
    {
        public float swingAngle;

        public void SyncDirection(int whoAmI)
        {
            ModPacket packet = Mod.GetPacket();
            //packet.Write((byte)BeefsMod.MessageType.SendCustomUseStylePlayerDirection);
            packet.Write((byte)whoAmI);
            packet.Write(swingAngle);
            packet.Send(ignoreClient: whoAmI);
        }

        public static void ReceiveDirection(BinaryReader reader, int whoAmI)
        {
            int player = reader.ReadByte();
            if (Main.netMode == NetmodeID.Server)
            {
                player = whoAmI;
            }

            SpinUseStylePlayer useStylePlayer = Main.player[player].GetModPlayer<SpinUseStylePlayer>();
            useStylePlayer.swingAngle = reader.ReadSingle();

            if (Main.netMode == NetmodeID.Server)
            {
                useStylePlayer.SyncDirection(player);
            }
        }

        [ReinitializeDuringResizeArrays]
        public static class SpinUseStyleItemSets
        {
            // Stores custom hold offsets if needed. Defaults to 0.
            // Cutlass, for example, is held slightly closer to the player.
            // ExampleCustomUseStyleWeapon is included here as an example even though it would default to 0 anyway.
            public static int[] HandOffsets = ItemID.Sets.Factory.CreateIntSet(0, ModContent.ItemType<CarnyLife>(), -27);
        }
        public class SpinUseStyleGlobalItem : GlobalItem
        {
            public static int SpinUseStyle;

            public override void Load()
            {
                // We register a custom use style ID in Load so that the value is set and ready to use in ModItem/GlobalItem.SetDefaults.
                SpinUseStyle = ItemLoader.RegisterUseStyle(Mod, "SpinUseStyle");
            }

            // Rather than checking item.useStyle in each method, we use AppliesToEntity to have the logic in this class only run for items set to use our custom use style.
            // Checking ItemID.Cutlass is necessary here because SetDefaults below won't run for Cutlass to change its Item.useStyle otherwise.
            public override bool AppliesToEntity(Item item, bool lateInstantiation)
            {
                return lateInstantiation && (item.useStyle == SpinUseStyle);
            }

            public override void SetDefaults(Item item)
            {
                // Cutlass will now use out custom use style, making it swing up instead of the normal swing.
                if (item.type == ModContent.ItemType<CarnyLife>())
                {
                    item.useStyle = SpinUseStyle;
                }
            }

            // We use the UseStyle method to determine where the item will be drawn during the weapon animation
            public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
            {
                // Due to this use style being able to swing in any direction, we need to use a ModPlayer to store and sync the swing direction to properly sync the weapon animation.
                SpinUseStylePlayer useStylePlayer = player.GetModPlayer<SpinUseStylePlayer>();

                // Find how far through out swing we are, between 0 and 1 (0% to 100%)
                // player.itemAnimation starts at its highest value (player.itemAnimationMax), and ticks down to 0
                // When it hits 0, the player is (usually) finished with their item animation
                float percentDone = 1 - (float)player.itemAnimation / player.itemAnimationMax;

                // The total angle that the item will cover throughout its swing
                float swingArcRange = MathHelper.ToRadians(-360);

                // When the animation starts, determine the swing direction. This code must only run on the local player since it involves the mouse cursor location.
                if (player.ItemAnimationJustStarted && player.whoAmI == Main.myPlayer)
                {
                    // Calculate the angle towards the cursor. Note that this code properly handles reverse gravity.
                    useStylePlayer.swingAngle = ((Main.MouseWorld - player.MountedCenter) * new Vector2(1, player.gravDir)).ToRotation();

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        // Send this value to other players so they see the correct swing angle.
                        useStylePlayer.SyncDirection(Main.myPlayer);
                    }
                }

                // If the item is being held by a mannequin, set the swing angle to straight ahead of the mannequin.
                if (player.isDisplayDollOrInanimate)
                {
                    useStylePlayer.swingAngle = player.direction > 0 ? 0 : MathHelper.Pi;
                }
                else
                {
                    // Set the player facing left or right depending on the target angle.
                    player.direction = Utils.ToDirectionInt(useStylePlayer.swingAngle.ToRotationVector2().X >= 0);
                }

                // Calculate start and end rotational values
                float start = useStylePlayer.swingAngle + (swingArcRange * .5f * player.direction);
                float end = useStylePlayer.swingAngle - (swingArcRange * .5f * player.direction);

                // and use them to calculate the current rotational value based on how long the weapon animation has been playing
                float currentAngle = MathHelper.Lerp(start, end, percentDone);

                // Here we set the rotation of the item. We need to add 45 degrees (PiOver4) because the weapon sprite is oriented that way. When facing left we add more rotation to account for the sprite being flipped.
                if (player.direction > 0)
                {
                    player.itemRotation = currentAngle + MathHelper.PiOver4;
                }
                else
                {
                    player.itemRotation = currentAngle + (MathHelper.PiOver4 * 3);
                }

                // Here we set the front arm drawing parameters. This uses the newer arm rendering approach.
                // The normal vanilla Swing doesn't use this approach and instead only uses the old approach of setting player.bodyFrame.Y during UseItemFrame.
                // The old approach has discrete arm positions while the new approach can draw the arm at any angle.
                player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, currentAngle - MathHelper.PiOver2);
                // We could also use SetCompositeArmBack to set the drawing parameters of the other arm.

                // We set itemLocation to indicate where the item should be drawn
                player.itemLocation = player.MountedCenter + currentAngle.ToRotationVector2() * new Vector2(SpinUseStyleItemSets.HandOffsets[item.type]);

                // This FlipItemLocationAndRotationForGravity method handles adjusting itemRotation and itemLocation to account for reversed gravity.
                player.FlipItemLocationAndRotationForGravity();
            }

        }

    }

}

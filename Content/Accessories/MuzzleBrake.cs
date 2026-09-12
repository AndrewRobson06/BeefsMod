using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Accessories
{
    public class MuzzleBrake : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 20;

            Item.value = Item.buyPrice(gold: 6);

            Item.rare = ItemRarityID.Blue;

            Item.accessory = true;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.bulletDamage += 0.5f;
        }

        
    }
}


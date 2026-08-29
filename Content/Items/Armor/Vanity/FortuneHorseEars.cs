using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BeefsMod.Content.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class FortuneHorseEars : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;

            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(silver: 75);
            Item.vanity = true;
            Item.maxStack = 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CrystalBall)
                .AddIngredient(ItemID.Silk, 10)
                .AddTile(TileID.Loom)
                .Register();
        }

        /*public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Quote", "Oho, what's this I see? Today's divination: great fortune!");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Dedicated", "Dedicated Item")
            {
                Color = new Color(230, 100, 255)
            };
            tooltips.Add(line);

            // Here we give the item name a rainbow effect.
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.Color = Main.DiscoColor;
                }
            }

            // Another method of hiding can be done if you want to hide just one line.
            // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();
        } */
    }

}

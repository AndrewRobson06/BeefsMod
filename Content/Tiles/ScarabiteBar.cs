using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static BeefsMod.Content.Tiles.ScarabiteOre;

namespace BeefsMod.Content.Tiles
{
    public class ScarabiteBar : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            VanillaFallbackOnModDeletion = TileID.MetalBars;

            AddMapEntry(new Color(107, 73, 176), Language.GetText("MapObject.MetalBar"));
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            if (!WorldGen.SolidTileAllowBottomSlope(i, j + 1))
                WorldGen.KillTile(i, j);

            return true;
        }
    }

    public class ScarabiteBarItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
            ItemID.Sets.SortingPriorityMaterials[Type] = 80;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<ScarabiteBar>());
            Item.width = 20;
            Item.height = 20;
            Item.value = 950;
            Item.rare = ItemRarityID.Yellow;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ScarabiteOreItem>(4)
                .AddTile(TileID.AdamantiteForge)
                .Register();
        }
    }

}

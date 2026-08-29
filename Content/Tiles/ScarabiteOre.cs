using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.IO;
using System.Collections.Generic;
//using BeefsMod.Core.Systems;

namespace BeefsMod.Content.Tiles
{
    public class ScarabiteOre : ModTile
    {
        public class ScarabiteOreItem : ModItem
        {
            public override void SetStaticDefaults()
            {
                Item.ResearchUnlockCount = 100;
                ItemID.Sets.SortingPriorityMaterials[Type] = 58;
            }

            public override void SetDefaults()
            {
                Item.DefaultToPlaceableTile(ModContent.TileType<ScarabiteOre>());
                Item.Size = new(12);
                Item.value = Item.sellPrice(silver: 19);
                Item.rare = ItemRarityID.Yellow;

            }
        }

        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 400;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 975;
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(42, 16, 79), name);

            DustType = DustID.BoneTorch;
            VanillaFallbackOnModDeletion = TileID.Platinum;

            HitSound = SoundID.Tink;
            MineResist = 5f;

            MinPick = 200; //pickaxe axe
        }
    }

    public class ScarabiteOreSystem : ModSystem
    {
        public static LocalizedText ScarabiteOrePassMessage { get; private set; }

        public override void SetStaticDefaults()
        {
            ScarabiteOrePassMessage = Mod.GetLocalization($"WorldGen.{nameof(ScarabiteOrePassMessage)}");
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int index = tasks.FindIndex(pass => pass.Name.Equals("Shinies"));

            if (index != 1)
                tasks.Insert(index + 1, new ScarabiteOrePass("Generating Scarabite", 237.4298f));
        }
    }

    public class ScarabiteOrePass : GenPass
    {
        public ScarabiteOrePass(string name, float loadWeight) : base(name, loadWeight)
        {

        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 0.0013); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)GenVars.worldSurface, Main.maxTilesY);

                Tile tile = Framing.GetTileSafely(x, y);
                
                if (tile.HasTile && tile.TileType == TileID.Sandstone)
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 13), WorldGen.genRand.Next(15, 25), 
                        ModContent.TileType<ScarabiteOre>());

                if (tile.HasTile && tile.TileType == TileID.HardenedSand)
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 13), WorldGen.genRand.Next(15, 25),
                        ModContent.TileType<ScarabiteOre>());

            }
        }
    }
}

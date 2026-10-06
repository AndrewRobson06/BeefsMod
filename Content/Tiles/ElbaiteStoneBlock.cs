using BeefsMod.Content.Dusts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace BeefsMod.Content.Tiles
{
    public class ElbaiteStoneBlock : ModTile
    {
        public class ElbaiteStoneBlockItem : ModItem 
        {
            public override void SetStaticDefaults()
            {
                Item.ResearchUnlockCount = 100;
                ItemID.Sets.SortingPriorityMaterials[Type] = 58;
            }

            public override void SetDefaults()
            {
                Item.DefaultToPlaceableTile(ModContent.TileType<ElbaiteStoneBlock>());
                Item.Size = new(12);
                Item.value = Item.sellPrice(silver: 1);
                Item.rare = ItemRarityID.LightRed;

            }
        }

        public class ElbaiteItem : ModItem 
        {
            public override void SetStaticDefaults()
            {
                Item.ResearchUnlockCount = 100;
                ItemID.Sets.SortingPriorityMaterials[Type] = 58;
            }

            public override void SetDefaults()
            {
                //Item.DefaultToPlaceableTile(ModContent.TileType<ScarabiteOre>());
                Item.Size = new(12);
                Item.value = Item.sellPrice(silver: 19);
                Item.rare = ItemRarityID.LightRed;

                Item.maxStack = 9999;

            }
        }


        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
            Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
            Main.tileOreFinderPriority[Type] = 635; // Metal Detector value, see https://terraria.wiki.gg/wiki/Metal_Detector
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileShine[Type] = 975; // How often tiny dust appear off this tile. Larger is less frequently
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(128, 41, 67), name);

            DustType = ModContent.DustType<ElbaiteDust>();
            VanillaFallbackOnModDeletion = TileID.AmberStoneBlock;
            HitSound = SoundID.Tink;
            MineResist = 2f;
            MinPick = 165; 
        }
    }

    public class ElbaiteSystem : ModSystem 
    {
        public static LocalizedText BlessedWithExampleOreMessage { get; private set; }

        public static bool WoFKilled = false;

        public override void ClearWorld()
        {
            WoFKilled = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (WoFKilled)
                tag["WoFKilled"] = true;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            WoFKilled = tag.ContainsKey("WoFKilled");
        }

        public override void PostUpdateWorld()
        {
            if(Main.hardMode && !WoFKilled)
            {
                GenerateElbaiteStoneBlock();
                WoFKilled = true;
            }
        }

        private void GenerateElbaiteStoneBlock()
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText("Your world has been blessed with Elbaite!", 128, 41, 67);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                Terraria.Chat.ChatHelper.BroadcastChatMessage(
                    Terraria.Localization.NetworkText.FromLiteral("Your world has been blessed with Elbaite!"),
                    new Color(128, 41, 67));
            }

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 0.0001); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, Main.maxTilesY) + 900;

                Tile tile = Framing.GetTileSafely(x, y);

                if (tile.HasTile && tile.TileType == TileID.Stone)
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 5), WorldGen.genRand.Next(2, 5),
                        ModContent.TileType<ElbaiteStoneBlock>());
            }
        }

        public override void SetStaticDefaults()
        {
            BlessedWithExampleOreMessage = Mod.GetLocalization($"WorldGen.{nameof(BlessedWithExampleOreMessage)}");
        }
    }

}

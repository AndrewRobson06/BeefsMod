using BeefsMod.Content.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using static BeefsMod.Content.Tiles.ElbaiteStoneBlock;

namespace BeefsMod.Core.GlobalTiles
{
    public class ElbaiteDrop : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!fail && !effectOnly && type == ModContent.TileType<ElbaiteStoneBlock>())
            {
                noItem = true;

                Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), new Vector2(i * 16, j * 16),
                    ModContent.ItemType<ElbaiteItem>());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using BeefsMod.Content.Items.Armor;

namespace BeefsMod.Core.ModPlayers
{
    public class BeefsModPlayer : ModPlayer
    {
        public int shakeTimer;

        public override void ModifyScreenPosition()
        {
            if (shakeTimer > 0)
            {
                shakeTimer--;
                Vector2 shake = new Vector2(Main.rand.NextFloat(shakeTimer), Main.rand.NextFloat(shakeTimer));
                //Main.screenPosition += shake;
            }
        }

        public void AddShake(int amount, bool clamped = true)
        {
            if (clamped)
            {
                if (shakeTimer < amount)
                    shakeTimer = amount;
            }
            else
                shakeTimer += amount;
        }
    }

}

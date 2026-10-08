using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BeefsMod.Content.Dusts
{
    public class ElbaiteDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.8f; // Multiply the dust's start velocity by 0.4, slowing it down
            dust.noGravity = true; // Makes the dust have gravity.
            dust.noLight = true; // Makes the dust emit no light.
            dust.scale *= 1f; // Multiplies the dust's initial scale by 1.5.
        }

        public override bool Update(Dust dust)
        { // Calls every frame the dust is active
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.15f;
            dust.scale *= 0.99f;

            float light = 0.35f * dust.scale;

            Lighting.AddLight(dust.position, light, light, light);

            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }

            return false; // Return false to prevent vanilla behavior.
        }
    }
}

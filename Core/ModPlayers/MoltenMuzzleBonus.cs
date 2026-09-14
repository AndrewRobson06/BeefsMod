using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Core.ModPlayers
{
    public class MoltenMuzzleBonus : ModPlayer
    {
        public bool hasMoltenMuzzle;

        public override void ResetEffects()
        {
            hasMoltenMuzzle = false;
        }
    }

    /*public class MoltenMuzzleApplyFireGlobal : GlobalItem
    {
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.GetModPlayer<MoltenMuzzleBonus>().hasMoltenMuzzle && Item.ammo = AmmoID.Bullet)
                target.AddBuff(BuffID.Venom, 420);
        }
    }*/

    public class MoltenMuzzleApplyFireGlobalProjectile : GlobalProjectile //fix so only bullets are set on fire
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.player[projectile.owner].GetModPlayer<MoltenMuzzleBonus>().hasMoltenMuzzle && projectile.CountsAsClass(DamageClass.Ranged))
                target.AddBuff(BuffID.OnFire3, 420);
        }
    }

}



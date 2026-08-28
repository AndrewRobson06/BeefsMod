using BeefsMod.Core.GlobalNPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BeefsMod.Content.Buffs
{
    public class ScarabiteJavelinBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // NPCs will automatically be immune to this buff if they are immune to BoneJavelin. SkeletronHead and SkeletronPrime are immune to BoneJavelin.
            BuffID.Sets.GrantImmunityWith[Type].Add(BuffID.BoneJavelin);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DamageOverTimeGlobalNPC>().ScarabiteJavelinDebuff = true;
        }
    }
}
namespace GemstonesDefense.Content.Buffs.Cabochon;

public class OnyxArmorBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        base.Update(player, ref buffIndex);

        player.immune = true;
        player.stoned = true;

        player.buffTime[buffIndex] = 2;
    }
}
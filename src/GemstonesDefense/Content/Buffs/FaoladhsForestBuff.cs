using GemstonesDefense.Content.Mounts;

namespace GemstonesDefense.Content.Buffs;

public class FaoladhsForestBuff : ModBuff
{
    public override void SetStaticDefaults() {
        base.SetStaticDefaults();

        Main.buffNoTimeDisplay[Type] = true;
        Main.buffNoSave[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex) {
        player.mount.SetMount(ModContent.MountType<FaoladhsForestMount>(), player);

        player.buffTime[buffIndex] = 2;
    }
}
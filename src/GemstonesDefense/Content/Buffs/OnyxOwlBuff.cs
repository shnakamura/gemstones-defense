using GemstonesDefense.Content.Projectiles;

namespace GemstonesDefense.Content.Buffs;

public class OnyxOwlBuff : ModBuff
{
    public override void SetStaticDefaults() {
        base.SetStaticDefaults();

        Main.buffNoTimeDisplay[Type] = true;
        Main.lightPet[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex) {
        base.Update(player, ref buffIndex);

        var unused = false;

        player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<OnyxOwlProjectile>());
    }
}
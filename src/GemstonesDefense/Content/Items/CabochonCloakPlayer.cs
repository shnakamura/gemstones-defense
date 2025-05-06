using System.Reflection;

namespace GemstonesDefense.Content.Items;

/// <summary>
/// </summary>
public sealed class CabochonCloakPlayer : ModPlayer
{
    private static readonly MethodInfo NPCLoot_DropMoney_Info = typeof(NPC).GetMethod("NPCLoot_DropMoney", BindingFlags.Instance | BindingFlags.NonPublic);

    static CabochonCloakPlayer() {
        if (NPCLoot_DropMoney_Info != null) {
            return;
        }

        throw new MissingMethodException(nameof(NPC), "NPCLoot_DropMoney");
    }

    /// <summary>
    ///     Whether the effects of this player are enabled or not.
    /// </summary>
    public bool Enabled { get; set; }

    public override void ResetEffects() {
        base.ResetEffects();

        Enabled = false;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
        base.OnHitNPC(target, hit, damageDone);

        if (!Enabled || target.life > 0) {
            return;
        }

        NPCLoot_DropMoney_Info.Invoke(target, [Player]);
    }
}
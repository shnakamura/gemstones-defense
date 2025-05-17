namespace GemstonesDefense.Content.Items;

/// <summary>
/// </summary>
public sealed class CabochonCowlPlayer : ModPlayer
{
    /// <summary>
    ///     Whether the effects of this player are enabled or not.
    /// </summary>
    public bool Enabled { get; set; }

    public override void ResetEffects()
    {
        base.ResetEffects();

        Enabled = false;
    }
}
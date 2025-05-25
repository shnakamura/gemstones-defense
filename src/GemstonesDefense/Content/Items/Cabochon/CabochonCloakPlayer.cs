using System.Reflection;
using Terraria.DataStructures;

namespace GemstonesDefense.Content.Items.Cabochon;

/// <summary>
/// </summary>
public sealed class CabochonCloakPlayer : ModPlayer
{
    private static readonly MethodInfo NPCLoot_DropMoney_Info = typeof(NPC).GetMethod("NPCLoot_DropMoney", BindingFlags.Instance | BindingFlags.NonPublic);

    static CabochonCloakPlayer()
    {
        if (NPCLoot_DropMoney_Info != null)
        {
            return;
        }

        throw new MissingMethodException(nameof(NPC), "NPCLoot_DropMoney");
    }

    /// <summary>
    ///     Gets or sets whether the cabochon cloak is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    public int WingFrame { get; private set; } = 4;
    
    public int WingFrameCounter { get; private set; }

    public override void ResetEffects()
    {
        base.ResetEffects();

        Enabled = false;
    }

    public override void PostUpdate()
    {
        base.PostUpdate();
        
        UpdateAnimation();
    }
    
    private void UpdateAnimation()
    {
        WingFrameCounter++;

        if (WingFrameCounter < 5f)
        {
            return;
        }

        var flying = Player.position != Player.oldPosition && Player.velocity.Y != 0f && Player.wingTime > 0f;

        if (flying)
        {
            UpdateFlyingAnimation();
        }
        else
        {
            UpdateIdleAnimation();
        }

        WingFrameCounter = 0;
    }

    private void UpdateFlyingAnimation()
    {
        if (WingFrame == 4)
        {
            WingFrame = 0;
        }
        else
        {
            WingFrame++;
        
            if (WingFrame < 4)
            {
                return;
            }
            
            WingFrame = 1;
        }
    }

    private void UpdateIdleAnimation()
    {
        if (Player.velocity.Y != 0f)
        {
            WingFrame = 0;
        }
        else
        {
            WingFrame = WingFrame != 0 && WingFrame != 4 ? 0 : 4;
        }
    }

    public override void HideDrawLayers(PlayerDrawSet drawInfo)
    {
        base.HideDrawLayers(drawInfo);

        if (!Enabled && !Main.gameMenu)
        {
            return;
        }
        
        PlayerDrawLayers.Wings.Hide();
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitNPC(target, hit, damageDone);

        if (!Enabled || target.life > 0)
        {
            return;
        }

        NPCLoot_DropMoney_Info.Invoke(target, [Player]);
    }
}
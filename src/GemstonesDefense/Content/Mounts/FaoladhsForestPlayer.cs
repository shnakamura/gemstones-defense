using GemstonesDefense.Common.Keybinds;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;

namespace GemstonesDefense.Content.Mounts;

public sealed class FaoladhsForestPlayer : ModPlayer
{
    /// <summary>
    ///     The cooldown of this player's mount ability, in ticks.
    /// </summary>
    public const int ABILITY_COOLDOWN = 20 * 60;

    /// <summary>
    ///     The duration of this player's mount ability, in ticks.
    /// </summary>
    public const int ABILITY_DURATION = 2 * 60;
    
    public static readonly SoundStyle AbilitySound = new($"{nameof(GemstonesDefense)}/Assets/Sounds/TheHauntingHowl") {
        PitchVariance = 0.25f,
        SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
    };

    public bool Enabled { get; set; }
    
    private int Cooldown {
        get => cooldown;
        set => cooldown = (int)MathHelper.Clamp(value, 0f, ABILITY_COOLDOWN);
    }

    private int cooldown;

    private int Duration {
        get => duration;
        set => duration = (int)MathHelper.Clamp(value, 0f, ABILITY_DURATION);
    }
    
    private int duration;

    public override void PostUpdateMiscEffects() {
        base.PostUpdateMiscEffects();

        if (Enabled) {
            Duration++;
            
            if (Duration >= ABILITY_DURATION) {
                Enabled = false;
                return;
            }
        }

        var mount = Player.mount;

        Cooldown++;

        if (!MountKeybindSystem.HowlAbility.JustPressed || Cooldown < ABILITY_COOLDOWN || !mount.Active || mount.Type != ModContent.MountType<FaoladhsForestMount>()) {
            return;
        }

        Duration = 0;
        Enabled = true;
        
        if (mount._mountSpecificData is not FaoladhsForestMount.AnimationData data) {
            return;
        }

        Cooldown = 0;
        
        data.Ability = true;

        SoundEngine.PlaySound(in AbilitySound, Player.Center);
            
        Main.instance.CameraModifiers.Add(new PunchCameraModifier(Player.Center, Vector2.UnitY, 2f, 6f, 60, 1000f));
    }
}
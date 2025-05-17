using GemstonesDefense.Common.Keybinds;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;

namespace GemstonesDefense.Content.Mounts;

public sealed class FaoladhsForestPlayer : ModPlayer
{
    /// <summary>
    ///     The cooldown of the player's mount ability, in ticks.
    /// </summary>
    public const int ABILITY_COOLDOWN = 20 * 60;

    /// <summary>
    ///     The duration of the player's mount ability, in ticks.
    /// </summary>
    public const int ABILITY_DURATION = 2 * 60;

    /// <summary>
    ///     The sound style played when the player's mount ability is used.
    /// </summary>
    public static readonly SoundStyle AbilitySound = new($"{nameof(GemstonesDefense)}/Assets/Sounds/TheHauntingHowl")
    {
        PitchVariance = 0.25f,
        SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
    };

    private int cooldown = ABILITY_COOLDOWN;

    private int duration;

    /// <summary>
    ///     Gets or sets whether the player's ability is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    ///     Gets or sets the cooldown of the player's ability.
    /// </summary>
    public int Cooldown
    {
        get => cooldown;
        set => cooldown = (int)MathHelper.Clamp(value, 0f, ABILITY_COOLDOWN);
    }

    /// <summary>
    ///     Gets or sets the duration of the player's ability.
    /// </summary>
    public int Duration
    {
        get => duration;
        set => duration = (int)MathHelper.Clamp(value, 0f, ABILITY_DURATION);
    }

    public override void PostUpdateMiscEffects()
    {
        base.PostUpdateMiscEffects();

        UpdateCollision();
        UpdateAbility();
        UpdateCooldown();

        Player.oldVelocity = Player.velocity;
    }

    private void UpdateCollision()
    {
        var mount = Player.mount;

        if (!mount.Active || mount.Type != ModContent.MountType<FaoladhsForestMount>())
        {
            return;
        }

        var landed = Player.velocity.Y == 0f && Player.oldVelocity.Y != 0f;

        if (!landed)
        {
            return;
        }

        foreach (var npc in Main.ActiveNPCs)
        {
            var hitbox = new Rectangle((int)Player.Center.X - 80, (int)Player.Center.Y - 40, 160, 80);
            var collides = npc.Hitbox.Intersects(hitbox);

            if (!collides || npc.friendly || npc.townNPC)
            {
                continue;
            }

            var info = new NPC.HitInfo
            {
                Damage = 100,
                Knockback = 2f,
                HitDirection = Player.direction
            };
            
            npc.StrikeNPC(info);
            
            NetMessage.SendStrikeNPC(npc, in info);
        }
    }

    private void UpdateAbility()
    {
        if (!Enabled)
        {
            return;
        }
        
        if (Duration <= ABILITY_DURATION / 2)
        {
            Player.controlLeft = false;
            Player.controlRight = false;
            
            Player.velocity = Vector2.Zero;
        }
            
        Duration++;

        if (Duration <= ABILITY_DURATION)
        {
            return;
        }
        
        Enabled = false;
    }

    private void UpdateCooldown()
    {
        if (Enabled)
        {
            return;
        }
        
        var mount = Player.mount;

        Cooldown++;

        if (!MountKeybind.Ability.JustPressed || Player.velocity.Y != 0f || !mount.Active || mount.Type != ModContent.MountType<FaoladhsForestMount>())
        {
            return;
        }

        Duration = 0;
        Enabled = true;

        if (mount._mountSpecificData is not FaoladhsForestMount.AnimationData data)
        {
            return;
        }

        Cooldown = 0;

        data.Ability = true;

        SoundEngine.PlaySound(in AbilitySound, Player.Center);

        Main.instance.CameraModifiers.Add(new PunchCameraModifier(Player.Center, Vector2.UnitY, 2f, 6f, 60, 1000f));
    }
}
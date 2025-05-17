using System.Collections.Generic;
using GemstonesDefense.Content.Buffs;
using ReLogic.Content;
using Terraria.DataStructures;

namespace GemstonesDefense.Content.Mounts;

public class FaoladhsForestMount : ModMount
{
    /// <summary>
    ///     The frame where the spawn animation begins.
    /// </summary>
    public const int ANIMATION_SPAWN_START = 0;

    /// <summary>
    ///     The frame where the standing animation begins.
    /// </summary>
    public const int ANIMATION_STANDING_START = 3;

    /// <summary>
    ///     The frame where the walking animation begins.
    /// </summary>
    public const int ANIMATION_WALKING_START = 8;

    /// <summary>
    ///     The frame where the running animation begins.
    /// </summary>
    public const int ANIMATION_RUNNING_START = 14;

    /// <summary>
    ///     The frame where the in air animation begins.
    /// </summary>
    public const int ANIMATION_IN_AIR_START = 19;

    /// <summary>
    ///     The frame where the ability animation begins.
    /// </summary>
    public const int ANIMATION_ABILITY_START = 21;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        MountData.buff = ModContent.BuffType<FaoladhsForestBuff>();

        MountData.spawnDustNoGravity = true;
        MountData.blockExtraJumps = false;
        MountData.constantJump = true;

        MountData.spawnDust = DustID.JungleGrass;

        MountData.jumpHeight = 16;
        MountData.jumpSpeed = 6f;
        MountData.heightBoost = 32;

        MountData.swimSpeed = 6f;
        MountData.runSpeed = 8f;

        MountData.acceleration = 0.125f;

        MountData.fallDamage = 0f;
        MountData.flightTimeMax = 0;

        MountData.totalFrames = 32;

        MountData.yOffset = -8;

        MountData.playerXOffset = 14;

        var offsets = new int[MountData.totalFrames];

        offsets[0] = 0;
        offsets[1] = 10;
        offsets[2] = 25;

        for (var i = 3; i < MountData.totalFrames; i++)
        {
            offsets[i] = 50;
        }

        MountData.playerYOffsets = offsets;

        MountData.playerHeadOffset = 22;
        MountData.bodyFrame = 3;

        MountData.standingFrameCount = 6;
        MountData.standingFrameDelay = 12;
        MountData.standingFrameStart = ANIMATION_STANDING_START;

        MountData.runningFrameCount = 6;
        MountData.runningFrameDelay = 12;
        MountData.runningFrameStart = ANIMATION_WALKING_START;

        MountData.inAirFrameCount = 1;
        MountData.inAirFrameDelay = 12;
        MountData.inAirFrameStart = ANIMATION_IN_AIR_START;

        MountData.idleFrameLoop = true;
        MountData.idleFrameCount = MountData.standingFrameCount;
        MountData.idleFrameDelay = MountData.standingFrameDelay;
        MountData.idleFrameStart = MountData.standingFrameStart;

        MountData.swimFrameCount = MountData.inAirFrameCount;
        MountData.swimFrameDelay = MountData.inAirFrameDelay;
        MountData.swimFrameStart = MountData.inAirFrameStart;

        if (Main.dedServ)
        {
            return;
        }

        MountData.textureWidth = MountData.backTexture.Width();
        MountData.textureHeight = MountData.backTexture.Height();
    }

    public override void SetMount(Player player, ref bool skipDust)
    {
        base.SetMount(player, ref skipDust);

        player.mount._mountSpecificData = new AnimationData(true, false);
    }

    public override void UpdateEffects(Player player)
    {
        base.UpdateEffects(player);

        player.fullRotation = player.velocity.Y == 0f ? 0f : player.fullRotation.AngleLerp(-player.velocity.X * 0.025f, 0.1f);

        if (MathF.Abs(player.velocity.X) <= 8f || !Main.rand.NextBool(2))
        {
            return;
        }
        
        Collision.HitTiles(player.Bottom, player.velocity, 1, 1);
    }
    
    public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
    {
        var mount = mountedPlayer.mount;

        if (mount._mountSpecificData is AnimationData data)
        {
            if (data.Spawn)
            {
                return UpdateSpawnAnimation(mount, data);
            }

            if (data.Ability)
            {
                return UpdateAbilityAnimation(mount, data);
            }
        }

        var running = Math.Abs(velocity.X) >= mountedPlayer.mount.RunSpeed * 0.9f && state == 1;

        return running ? UpdateRunningAnimation(mount) : base.UpdateFrame(mountedPlayer, state, velocity);
    }

    private static bool UpdateSpawnAnimation(Mount mount, AnimationData data)
    {
        mount._frameCounter++;

        if (mount._frame < ANIMATION_SPAWN_START)
        {
            mount._frame = ANIMATION_SPAWN_START;
        }

        if (mount._frame > ANIMATION_SPAWN_START + 2)
        {
            data.Spawn = false;
        }
        else if (mount._frameCounter > 3f)
        {
            mount._frame++;
            mount._frameCounter = 0f;
        }

        return false;
    }

    private bool UpdateAbilityAnimation(Mount mount, AnimationData data)
    {
        mount._frameCounter++;

        if (mount._frame < ANIMATION_ABILITY_START)
        {
            mount._frame = ANIMATION_ABILITY_START;
        }

        if (mount._frame > MountData.totalFrames - 2)
        {
            data.Ability = false;
        }
        else if (mount._frameCounter > 5f)
        {
            mount._frame++;
            mount._frameCounter = 0f;
        }

        return false;
    }

    private static bool UpdateRunningAnimation(Mount mount)
    {
        mount._frameCounter++;

        if (mount._frame > ANIMATION_RUNNING_START + 5)
        {
            mount._frame = ANIMATION_RUNNING_START;
        }

        if (mount._frameCounter <= 3f)
        {
            return false;
        }
        
        mount._frame++;
        mount._frameCounter = 0f;

        return false;
    }

    public override bool Draw
    (
        List<DrawData> playerDrawData,
        int drawType,
        Player drawPlayer,
        ref Texture2D texture,
        ref Texture2D glowTexture,
        ref Vector2 drawPosition,
        ref Rectangle frame,
        ref Color drawColor,
        ref Color glowColor,
        ref float rotation,
        ref SpriteEffects spriteEffects,
        ref Vector2 drawOrigin,
        ref float drawScale,
        float shadow
    )
    {
        var ability = drawType == 0 && drawPlayer.mount._mountSpecificData is AnimationData data && data.Ability;
        
        var result = base.Draw
        (
            playerDrawData,
            drawType,
            drawPlayer,
            ref texture,
            ref glowTexture,
            ref drawPosition,
            ref frame,
            ref drawColor,
            ref glowColor,
            ref rotation,
            ref spriteEffects,
            ref drawOrigin,
            ref drawScale,
            shadow
        );
        
        if (!ability)
        {
            return result;
        }

        var progress = (drawPlayer.mount._frame - ANIMATION_ABILITY_START) / (float)(MountData.totalFrames - 2 - ANIMATION_ABILITY_START);
        var scale = progress * 1.5f;
            
        FaoladhsForestRenderer.Draw(progress, scale);

        return result;
    }

    public sealed class AnimationData(bool spawn, bool ability)
    {
        public bool Spawn { get; set; } = spawn;

        public bool Ability { get; set; } = ability;
    }
}
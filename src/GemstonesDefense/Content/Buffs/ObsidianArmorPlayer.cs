using GemstonesDefense.Content.Items;
using GemstonesDefense.Utilities;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria.Graphics;
using Terraria.Graphics.CameraModifiers;
using Terraria.Graphics.Renderers;

namespace GemstonesDefense.Content.Buffs;

public sealed class ObsidianArmorPlayer : ModPlayer
{
    public static Asset<Texture2D> OverlayTexture { get; private set; }

    public static Asset<Texture2D> OverlayOutlineTexture { get; private set; }

    public override void Load()
    {
        base.Load();

        On_LegacyPlayerRenderer.DrawPlayerStoned += LegacyPlayerRenderer_DrawPlayerStoned_Hook;

        if (Main.dedServ)
        {
            return;
        }

        OverlayTexture = ModContent.Request<Texture2D>($"{nameof(GemstonesDefense)}/Assets/Textures/PlayerObsidianObelisk");
        OverlayOutlineTexture = ModContent.Request<Texture2D>($"{nameof(GemstonesDefense)}/Assets/Textures/PlayerObsidianObelisk_Outline");
    }

    public override void PostUpdateMiscEffects()
    {
        base.PostUpdateMiscEffects();

        var hasCowl = Player.TryGetModPlayer(out CabochonCowlPlayer cowlPlayer) && cowlPlayer.Enabled;
        var hasCloak = Player.TryGetModPlayer(out CabochonCloakPlayer cloakPlayer) && cloakPlayer.Enabled;

        if (!hasCowl || !hasCloak || Player.mount.Active)
        {
            return;
        }

        if (Player.HasBuff<ObsidianArmorBuff>())
        {
            if (!InputUtils.IsKeyDown(Keys.W))
            {
                Player.TryRemoveBuff<ObsidianArmorBuff>();

                SpawnDustEffects();
                return;
            }

            if (!Player.JustLanded())
            {
                return;
            }

            Main.instance.CameraModifiers.Add(new PunchCameraModifier(Player.Center, Vector2.UnitY, 2.5f, 6f, 20, 1000f));
        }
        else if (Player.JustDoubleTappedUp())
        {
            Player.AddBuff(ModContent.BuffType<ObsidianArmorBuff>(), 60);

            SpawnDustEffects();
        }

        Player.oldVelocity = Player.velocity;
    }

    private void SpawnDustEffects()
    {
        for (var i = 0; i < 20; i++)
        {
            var dust = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Obsidian, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));

            dust.scale = Main.rand.NextFloat(1f, 1.3f);

            dust.noGravity = true;
        }
    }

    private static void LegacyPlayerRenderer_DrawPlayerStoned_Hook(On_LegacyPlayerRenderer.orig_DrawPlayerStoned orig, LegacyPlayerRenderer self, Camera camera, Player drawPlayer, Vector2 position)
    {
        if (drawPlayer.HasBuff<ObsidianArmorBuff>())
        {
            var effects = drawPlayer.direction != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            // This is code directly adapted from how Vanilla renders the player while stoned. This could surely be cleaned up, but I prefer not to touch it just in case.
            camera.SpriteBatch.Draw
            (
                OverlayTexture.Value,
                new Vector2
                (
                    (int)(position.X - camera.UnscaledPosition.X - drawPlayer.bodyFrame.Width / 2 + drawPlayer.width / 2),
                    (int)(position.Y - camera.UnscaledPosition.Y + drawPlayer.height - drawPlayer.bodyFrame.Height + 8f)
                ) +
                drawPlayer.bodyPosition +
                new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2) -
                new Vector2(0f, 4f),
                null,
                Lighting.GetColor((int)(position.X + drawPlayer.width * 0.5) / 16, (int)(position.Y + drawPlayer.height * 0.5) / 16, Color.White),
                0f,
                OverlayTexture.Size() / 2f,
                1f,
                effects,
                0f
            );

            camera.SpriteBatch.Draw
            (
                OverlayOutlineTexture.Value,
                new Vector2
                (
                    (int)(position.X - camera.UnscaledPosition.X - drawPlayer.bodyFrame.Width / 2 + drawPlayer.width / 2),
                    (int)(position.Y - camera.UnscaledPosition.Y + drawPlayer.height - drawPlayer.bodyFrame.Height + 8f)
                ) +
                drawPlayer.bodyPosition +
                new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2) -
                new Vector2(0f, 4f),
                null,
                Lighting.GetColor((int)(position.X + drawPlayer.width * 0.5) / 16, (int)(position.Y + drawPlayer.height * 0.5) / 16, new Color(87, 81, 173, 0)) * 0.5f,
                0f,
                OverlayOutlineTexture.Size() / 2f,
                1f,
                effects,
                0f
            );
        }
        else
        {
            orig(self, camera, drawPlayer, position);
        }
    }
}
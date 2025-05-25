using ReLogic.Content;
using Terraria.DataStructures;

namespace GemstonesDefense.Content.Items.Cabochon;

public sealed class CabochonCloakWingsDrawLayer : PlayerDrawLayer
{
    /// <summary>
    ///     Gets or sets the body texture.
    /// </summary>
    public static Asset<Texture2D> WingsTexture { get; private set; }

    public override void Load()
    {
        base.Load();

        if (Main.dedServ)
        {
            return;
        }

        WingsTexture = ModContent.Request<Texture2D>($"{nameof(GemstonesDefense)}/Content/Items/Cabochon/CabochonCloakItem_Wings");
    }
    
    public override Position GetDefaultPosition()
    {
        return new BeforeParent(PlayerDrawLayers.BackAcc);
    }

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
        var player = drawInfo.drawPlayer;
        
        return player.TryGetModPlayer(out CabochonCloakPlayer modPlayer) && modPlayer.Enabled;
    }

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        var player = drawInfo.drawPlayer;

        if (!player.TryGetModPlayer(out CabochonCloakPlayer modPlayer) || !modPlayer.Enabled)
        {
            return;
        }

        var frame = WingsTexture.Value.Frame(1, 5, 0, modPlayer.WingFrame);

        var origin = frame.Size() / 2f;

        var offset = new Vector2
        (
            (int)(drawInfo.Position.X + player.width / 2f - player.bodyFrame.Width / 2f - Main.screenPosition.X),
            (int)(drawInfo.Position.Y + player.height - player.bodyFrame.Height + 4f - Main.screenPosition.Y)
        );
        
        var position = player.bodyPosition + drawInfo.bodyVect + offset;

        position.X -= 10f * player.direction;
        
        var effects = player.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        drawInfo.DrawDataCache.Add
        (
            new DrawData
            (
                WingsTexture.Value,
                position,
                frame,
                drawInfo.colorArmorBody,
                player.bodyRotation,
                origin,
                1f,
                effects
            )
        );
    }
}
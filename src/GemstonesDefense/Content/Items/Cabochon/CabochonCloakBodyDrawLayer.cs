using ReLogic.Content;
using Terraria.DataStructures;

namespace GemstonesDefense.Content.Items.Cabochon;

public sealed class CabochonCloakBodyDrawLayer : PlayerDrawLayer
{
    /// <summary>
    ///     Gets or sets the body texture.
    /// </summary>
    public static Asset<Texture2D> BodyTexture { get; private set; }

    public override void Load()
    {
        base.Load();

        if (Main.dedServ)
        {
            return;
        }

        BodyTexture = ModContent.Request<Texture2D>($"{nameof(GemstonesDefense)}/Content/Items/Cabochon/CabochonCloakItem_Body");
    }

    
    public override Position GetDefaultPosition()
    {
        return new AfterParent(PlayerDrawLayers.Torso);
    }

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
        var player = drawInfo.drawPlayer;

        return player.TryGetModPlayer(out CabochonCloakPlayer modPlayer) && modPlayer.Enabled;
    }

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        var player = drawInfo.drawPlayer;
        
        var origin = drawInfo.bodyVect;

        var offset = new Vector2
        (
            (int)(drawInfo.Position.X + player.width / 2f - player.bodyFrame.Width / 2f - Main.screenPosition.X),
            (int)(drawInfo.Position.Y + player.height - player.bodyFrame.Height + 4f - Main.screenPosition.Y)
        );
        
        var position = player.bodyPosition + drawInfo.bodyVect + offset;

        var effects = player.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        drawInfo.DrawDataCache.Add
        (
            new DrawData
            (
                BodyTexture.Value,
                position,
                player.bodyFrame,
                drawInfo.colorArmorHead,
                player.bodyRotation,
                origin,
                1f,
                effects
            )
        );
    }
}
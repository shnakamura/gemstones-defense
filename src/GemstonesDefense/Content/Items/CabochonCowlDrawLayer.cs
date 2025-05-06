using ReLogic.Content;
using Terraria.DataStructures;

namespace GemstonesDefense.Content.Items;

public sealed class CabochonCowlDrawLayer : PlayerDrawLayer
{
    public static readonly Asset<Texture2D> HeadTexture = ModContent.Request<Texture2D>($"{nameof(GemstonesDefense)}/Content/Items/CabochonCowlItem_Head_Alt");
    
    public override Position GetDefaultPosition() {
        return new AfterParent(PlayerDrawLayers.Head);
    }

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
        var player = drawInfo.drawPlayer;

        return player.armor[0].type == ModContent.ItemType<CabochonCowlItem>() && player.direction == -1;
    }

    protected override void Draw(ref PlayerDrawSet drawInfo) {
        var player = drawInfo.drawPlayer;
        
        var headOrigin = drawInfo.headVect;
        var headPosition = player.headPosition + drawInfo.headVect + new Vector2((int)(drawInfo.Position.X + (float)player.width / 2f - (float)player.bodyFrame.Width / 2f - Main.screenPosition.X), (int)(drawInfo.Position.Y + (float)player.height - (float)player.bodyFrame.Height + 4f - Main.screenPosition.Y));
        
        drawInfo.DrawDataCache.Add(new DrawData(
            HeadTexture.Value,
            headPosition,
            player.bodyFrame,
            Color.White,
            player.headRotation,
            headOrigin,
            1f,
            SpriteEffects.FlipHorizontally
        ));
    }
}
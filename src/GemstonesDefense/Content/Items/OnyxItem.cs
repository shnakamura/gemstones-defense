using GemstonesDefense.Content.Tiles;

namespace GemstonesDefense.Content.Items;

public class OnyxItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.DefaultToPlaceableTile(ModContent.TileType<OnyxTile>());
    }
}
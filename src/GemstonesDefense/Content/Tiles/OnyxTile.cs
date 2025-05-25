namespace GemstonesDefense.Content.Tiles;

public class OnyxTile : ModTile
{
    /// <summary>
    ///     The map color of the tile.
    /// </summary>
    public static readonly Color Color = new();
    
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        
        AddMapEntry(Color);
        
        MineResist = 1f;
        
        HitSound = SoundID.Tink;
        DustType = DustID.Obsidian;
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        base.NumDust(i, j, fail, ref num);

        num = fail ? 1 : 3;
    }
}
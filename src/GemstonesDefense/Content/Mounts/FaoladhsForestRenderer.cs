using System.Collections.Generic;
using ReLogic.Content;

namespace GemstonesDefense.Content.Mounts;

[Autoload(Side = ModSide.Client)]
public sealed class FaoladhsForestRenderer : ILoadable
{
    /// <summary>
    ///     Gets or sets the texture of the animation.
    /// </summary>
    public static Asset<Texture2D> InverseMaskTexture { get; private set; }

    /// <summary>
    ///     Gets or sets the progress of the animation.
    /// </summary>
    public static float Progress { get; private set; }
    
    /// <summary>
    ///     Gets or sets the scale of the animation.
    /// </summary>
    public static float Scale { get; private set; }
    
    void ILoadable.Load(Mod mod)
    {
        On_Main.DoDraw_DrawNPCsBehindTiles += Main_DrawNPCsBehindTiles_Hook;
        
        if (Main.dedServ)
        {
            return;
        }
        
        InverseMaskTexture = ModContent.Request<Texture2D>($"{nameof(GemstonesDefense)}/Assets/Textures/InverseMask");
    }

    void ILoadable.Unload() { }

    public static void Draw(float progress, float scale)
    {
        Progress = progress;
        Scale = scale;
    }
    
    private static void Main_DrawNPCsBehindTiles_Hook(On_Main.orig_DoDraw_DrawNPCsBehindTiles orig, Main self)
    {
        if (Progress > 0f && Scale > 0f)
        {
            Main.spriteBatch.Begin();
            
            var player = Main.LocalPlayer;

            var texture = InverseMaskTexture.Value;

            var position = player.Center - Main.screenPosition + new Vector2(0f, player.gfxOffY);
            var color = new Color(97, 162, 26, 0) * (1f - Progress * Progress);

            var origin = texture.Size() / 2f;
        
            Main.EntitySpriteDraw(texture, position, null, color, 0f, origin, Scale, SpriteEffects.None);
            
            Main.spriteBatch.End();
        }
        
        orig(self);
    }
}
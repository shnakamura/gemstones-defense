using Microsoft.Xna.Framework.Input;

namespace GemstonesDefense.Common.Keybinds;

public sealed class MountKeybind : ILoadable
{
    /// <summary>
    ///     The name of the keybind.
    /// </summary>
    public const string KEYBIND_NAME = $"{nameof(GemstonesDefense)}:{nameof(Ability)}";

    /// <summary>
    ///     The default keybind.
    /// </summary>
    public static readonly Keys Default = Keys.T;
    
    /// <summary>
    ///     Gets or sets the keybind.
    /// </summary>
    public static ModKeybind Ability { get; private set; }

    void ILoadable.Load(Mod mod)
    {
        Ability = KeybindLoader.RegisterKeybind(mod, KEYBIND_NAME, Default);
    }
    
    void ILoadable.Unload()
    {
        Ability = null;
    }
}
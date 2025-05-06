using Microsoft.Xna.Framework.Input;

namespace GemstonesDefense.Common.Keybinds;

public sealed class MountKeybindSystem : ModSystem
{
    public static ModKeybind HowlAbility { get; private set; }

    public override void Load() {
        base.Load();

        HowlAbility = KeybindLoader.RegisterKeybind(Mod, $"{nameof(GemstonesDefense)}:{nameof(HowlAbility)}", Keys.T);
    }
}
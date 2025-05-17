using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Input;

namespace GemstonesDefense.Utilities;

/// <summary>
///     Provides input utilities.
/// </summary>
public static class InputUtils
{
    /// <summary>
    ///     Checks whether the client has a key pressed or not.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns><c>true</c> if the client has the specified key pressed; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsKeyDown(Keys key)
    {
        return Main.keyState.IsKeyDown(key);
    }

    /// <summary>
    ///     Checks whether the client had a key pressed or not.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns><c>true</c> if the client had the specified key pressed; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool WasKeyDown(Keys key)
    {
        return Main.oldKeyState.IsKeyDown(key);
    }
}
namespace GemstonesDefense.Common.Recipes;

public sealed class RecipeGroupSystem : ModSystem
{
    public static RecipeGroup GoldBar { get; private set; }

    public override void AddRecipeGroups() {
        base.AddRecipeGroups();

        GoldBar = new RecipeGroup(() => Mod.GetLocalization("Recipes.Groups.GoldBar").Value, ItemID.GoldBar, ItemID.PlatinumBar);

        RecipeGroup.RegisterGroup(nameof(ItemID.GoldBar), GoldBar);
    }

    public override void Unload() {
        base.Unload();

        GoldBar = null;
    }
}
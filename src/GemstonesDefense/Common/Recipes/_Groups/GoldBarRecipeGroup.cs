namespace GemstonesDefense.Common.Recipes;

public sealed class GoldBarRecipeGroup : ModSystem
{
    /// <summary>
    ///     Gets or sets the recipe group.
    /// </summary>
    public static RecipeGroup Group { get; private set; } 

    public override void AddRecipeGroups()
    {
        base.AddRecipeGroups();

        Group = new RecipeGroup(() => Mod.GetLocalization("Recipes.Groups.GoldBar").Value, ItemID.GoldBar, ItemID.PlatinumBar);

        RecipeGroup.RegisterGroup(nameof(ItemID.GoldBar), Group);
    }

    public override void Unload()
    {
        base.Unload();

        Group = null;
    }
}
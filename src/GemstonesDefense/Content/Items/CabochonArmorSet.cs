using System.Collections.Generic;
using GemstonesDefense.Common.Recipes;

namespace GemstonesDefense.Content.Items;

[AutoloadEquip(EquipType.Head)]
public class CabochonCowlItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.defense = 10;

        Item.width = 28;
        Item.height = 30;
    }

    public override void UpdateEquip(Player player) {
        base.UpdateEquip(player);

        player.luck += 0.01f;

        if (!player.TryGetModPlayer(out CabochonCowlPlayer modPlayer)) {
            return;
        }

        modPlayer.Enabled = true;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient(ItemID.Obsidian, 10)
            .AddIngredient(ItemID.Diamond, 10)
            .AddRecipeGroup(RecipeGroupSystem.GoldBar)
            .Register();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips) {
        base.ModifyTooltips(tooltips);

        tooltips.Add(
            new TooltipLine(Mod, $"{nameof(CabochonCowlItem)}:Ability", this.GetLocalizedValue("Ability")) {
                OverrideColor = new Color(112, 144, 219)
            }
        );
    }
}

[AutoloadEquip(EquipType.Legs)]
public class CabochonBootsItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.width = 22;
        Item.height = 20;

        Item.defense = 10;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient(ItemID.Diamond, 5)
            .AddRecipeGroup(RecipeGroupSystem.GoldBar, 10)
            .AddTile(TileID.Anvils)
            .Register();
    }
}

[AutoloadEquip(EquipType.Body)]
public class CabochonChestplateItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.width = 40;
        Item.height = 18;

        Item.defense = 10;
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient(ItemID.Emerald, 10)
            .AddRecipeGroup(RecipeGroupSystem.GoldBar, 10)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
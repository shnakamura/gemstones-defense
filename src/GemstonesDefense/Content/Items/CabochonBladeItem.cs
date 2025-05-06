using GemstonesDefense.Common.Recipes;

namespace GemstonesDefense.Content.Items;

public class CabochonBladeItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.autoReuse = true;

        Item.DamageType = DamageClass.Melee;
        Item.knockBack = 5f;
        Item.damage = 50;
        Item.crit = 10;

        Item.width = 60;
        Item.height = 60;

        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.buyPrice(gold: 10);
    }

    public override void AddRecipes() {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient(ItemID.Diamond, 5)
            .AddIngredient(ItemID.Ruby, 5)
            .AddIngredient(ItemID.Sapphire, 5)
            .AddIngredient(ItemID.Emerald, 5)
            .AddIngredient(ItemID.Amethyst, 5)
            .AddIngredient(ItemID.Topaz, 5)
            .AddRecipeGroup(RecipeGroupSystem.GoldBar)
            .AddTile(TileID.Anvils)
            .Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox) {
        base.MeleeEffects(player, hitbox);

        if (Main.dedServ) {
            return;
        }

        var brightness = 0.5f;

        Lighting.AddLight(player.itemLocation, brightness, brightness, brightness);
    }

    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) {
        base.PostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);

        if (Main.dedServ) {
            return;
        }

        var brightness = 0.5f;

        Lighting.AddLight(Item.Center, brightness, brightness, brightness);
    }
}
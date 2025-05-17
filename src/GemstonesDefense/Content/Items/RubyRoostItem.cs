using GemstonesDefense.Content.Buffs;
using GemstonesDefense.Content.Projectiles;

namespace GemstonesDefense.Content.Items;

public class RubyRoostItem : ModItem
{
    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.noUseGraphic = true;
        Item.noMelee = true;

        Item.width = 32;
        Item.height = 30;

        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.UseSound = SoundID.Item2;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.buffType = ModContent.BuffType<OnyxOwlBuff>();

        Item.shoot = ModContent.ProjectileType<OnyxOwlProjectile>();
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient(ItemID.Ruby, 10)
            .AddIngredient(ItemID.Diamond)
            .AddTile(TileID.Anvils)
            .Register();
    }

    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        base.UseStyle(player, heldItemFrame);

        if (player.whoAmI != Main.myPlayer || player.itemTime != 0)
        {
            return;
        }

        player.AddBuff(Item.buffType, 3600);
    }
}
using System.Collections.Generic;
using GemstonesDefense.Common.Recipes;
using Terraria.DataStructures;

namespace GemstonesDefense.Content.Items;

[AutoloadEquip(EquipType.Wings)]
public class CabochonCloakItem : ModItem
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(210, 2f);
    }

    public override void SetDefaults()
    {
        base.SetDefaults();

        Item.accessory = true;

        Item.defense = 10;

        Item.width = 38;
        Item.height = 32;
    }

    public override void AddRecipes()
    {
        base.AddRecipes();

        CreateRecipe()
            .AddIngredient(ItemID.Diamond, 10)
            .AddIngredient(ItemID.Ruby, 10)
            .AddIngredient(ItemID.Sapphire, 10)
            .AddIngredient(ItemID.Emerald, 10)
            .AddIngredient(ItemID.Amethyst, 10)
            .AddIngredient(ItemID.Topaz, 10)
            .AddRecipeGroup(GoldBarRecipeGroup.Group)
            .AddIngredient(ItemID.Silk)
            .Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);

        if (!player.TryGetModPlayer(out CabochonCloakPlayer modPlayer))
        {
            return;
        }

        modPlayer.Enabled = true;

        player.lifeRegen += (int)(player.lifeRegen * 0.1f);
        player.manaRegen += (int)(player.manaRegen * 0.1f);

        player.pickSpeed += 0.1f;

        player.GetCritChance(DamageClass.Generic) += 0.1f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        base.ModifyTooltips(tooltips);

        tooltips.Add
        (
            new TooltipLine(Mod, $"{nameof(CabochonCloakItem)}:Ability", this.GetLocalizedValue("Ability"))
            {
                OverrideColor = new Color(112, 144, 219)
            }
        );
    }
}
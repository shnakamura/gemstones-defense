namespace GemstonesDefense.Content.Mounts;

public sealed class FaoladhsForestGlobalNPC : GlobalNPC
{
    public const float DISTANCE = 32f * 16f;

    public override bool PreAI(NPC npc)
    {
        var player = Main.player[npc.target];

        var enabled = player.TryGetModPlayer(out FaoladhsForestPlayer modPlayer) && modPlayer.Enabled;

        var hasRange = npc.DistanceSQ(player.Center) <= DISTANCE * DISTANCE;
        var hasAttributes = !npc.townNPC && !npc.friendly && npc.damage > 0;

        if (!enabled || !hasRange || !hasAttributes)
        {
            return true;
        }
        
        npc.velocity = Vector2.SmoothStep(npc.velocity, player.DirectionTo(npc.Center) * 2f, 0.2f);

        Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY);

        return false;
    }
}
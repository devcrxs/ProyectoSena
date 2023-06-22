using UnityEngine;
public class UpgradeDash : CheckCollisionPlayer
{
    protected override void TriggerPlayer(Collider2D player)
    {
        PlayerDash.instance.CanDash = true;
    }

    protected override void CollisionPlayer(Collision2D player)
    {
        throw new System.NotImplementedException();
    }
}

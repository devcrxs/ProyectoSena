using UnityEngine;
public class UpgradeDash : CheckCollisionPlayer
{
    protected override void TriggerPlayerEnter(Collider2D player)
    {
        PlayerDash.instance.CanDash = true;
    }

    protected override void TriggerPlayerStay(Collider2D player)
    {

    }

    protected override void TriggerPlayerExit(Collider2D player)
    {

    }

    protected override void CollisionPlayer(Collision2D player)
    {

    }
}

using UnityEngine;
public class DangerZone : CheckCollisionPlayer
{
    protected override void TriggerPlayerEnter(Collider2D player)
    {
        GameManager.instance.CanActivePowerUpCat = false;
    }

    protected override void TriggerPlayerStay(Collider2D player)
    {
        GameManager.instance.CanActivePowerUpCat = false;
    }

    protected override void TriggerPlayerExit(Collider2D player)
    {
        GameManager.instance.CanActivePowerUpCat = true;
    }

    protected override void CollisionPlayer(Collision2D player)
    {
        throw new System.NotImplementedException();
    }
}

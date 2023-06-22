using UnityEngine;
public class LimitPlayer : CheckCollisionPlayer
{
    protected override void TriggerPlayer(Collider2D player)
    {
        TransitionManager.instance.TransitionDead();
    }

    protected override void CollisionPlayer(Collision2D player)
    {
        throw new System.NotImplementedException();
    }
}

using UnityEngine;

public class DeadPlayer : CheckCollisionPlayer
{
    protected override void TriggerPlayer(Collider2D player)
    {
        throw new System.NotImplementedException();
    }

    protected override void CollisionPlayer(Collision2D player)
    {
        
    }
}

using UnityEngine;
public class GameOver : CheckCollisionPlayer
{
    protected override void TriggerPlayerEnter(Collider2D player)
    {
        PlayerAnimations.instance.AnimationDead();
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

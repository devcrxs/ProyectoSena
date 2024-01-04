using UnityEngine;
enum TypeTutorial
{
    Jump,
    Dash
}
public class ActiveTutorial : CheckCollisionPlayer
{
    [SerializeField] private TypeTutorial typeTutorial;
    protected override void TriggerPlayerEnter(Collider2D player)
    {
        if (typeTutorial == TypeTutorial.Jump)
        {
            GameManager.instance.CanActiveJump = true;
            PersistenDataManager.instance.SetUnlockJump();
            return;
        }

        GameManager.instance.CanActiveDash = true;
        PersistenDataManager.instance.SetUnlockDash();
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

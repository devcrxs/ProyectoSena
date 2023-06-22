using UnityEngine;
public abstract class CheckCollisionPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        TriggerPlayer(col);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        CollisionPlayer(col);
    }

    protected abstract void TriggerPlayer(Collider2D player);
    protected abstract void CollisionPlayer(Collision2D player);
}

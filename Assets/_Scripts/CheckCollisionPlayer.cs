using UnityEngine;
public abstract class CheckCollisionPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        TriggerPlayerEnter(col);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        TriggerPlayerStay(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        TriggerPlayerExit(other);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        CollisionPlayer(col);
    }

    protected abstract void TriggerPlayerEnter(Collider2D player);
    protected abstract void TriggerPlayerStay(Collider2D player);
    protected abstract void TriggerPlayerExit(Collider2D player);
    protected abstract void CollisionPlayer(Collision2D player);
}

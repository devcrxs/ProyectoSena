using UnityEngine;
public class AlwaysViewPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Update()
    {
        Flip();
    }

    private void Flip()
    {
        if (player == null) return;
        if (player.position.x > transform.position.x)
        {
            _spriteRenderer.flipX = false;
            return;
        }

        _spriteRenderer.flipX = true;
    }
}

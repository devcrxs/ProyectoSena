using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerProperties : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float gravityScaleNormalized;
    [Header("Checks")] 
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize = new(0.49f, 0.03f);
    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;
    public float Gravity => gravityScaleNormalized;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public static PlayerProperties instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        SetGravityScale(gravityScaleNormalized);
    }

    public void SetGravityScale(float scale)
    {
        _rigidbody2D.gravityScale = scale;
    }

    public bool IsTouchGround()
    {
        return Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer);
    }

    public bool IsDynamicBody()
    {
        return _rigidbody2D.bodyType == RigidbodyType2D.Dynamic;
    }

    public void GoPlayerInstant()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    public void StopPlayerInstant(RigidbodyType2D rigidbodyType2D)
    {
        _rigidbody2D.bodyType = rigidbodyType2D;
    }
    public IEnumerator StopPlayerAsync(float timeStop)
    {
        StopPlayerInstant(RigidbodyType2D.Static);
        yield return new WaitForSeconds(timeStop);
        GoPlayerInstant();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
    }
}

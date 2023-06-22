using UnityEngine;
public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private float speedMove;
    private Vector2 _offset;
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void LateUpdate()
    {
        _offset = new Vector2(speedMove * Time.deltaTime,0);
        _material.mainTextureOffset += _offset;
    }
}

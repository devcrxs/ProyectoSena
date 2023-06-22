using Cinemachine;
using UnityEngine;
public class CameraTrigger : MonoBehaviour
{
    private BoxCollider2D _collider;
    [SerializeField] private CinemachineVirtualCamera cameraLeft;
    [SerializeField] private CinemachineVirtualCamera cameraRight;
    [SerializeField] private Transform safeZoneLeft;
    [SerializeField] private Transform safeZoneRight;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Vector2 exitDirection = (other.transform.position - _collider.bounds.center).normalized;
        CinemachineVirtualCamera newCurrentCamera = exitDirection.x > 0 ? cameraRight : cameraLeft;
        CameraManager.instance.SwapCamera(newCurrentCamera);
        GameManager.instance.LastPositionSafe = exitDirection.x > 0 ? safeZoneRight : safeZoneLeft;
    }
}

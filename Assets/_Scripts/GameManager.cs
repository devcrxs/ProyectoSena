using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Transform playerRef;
    private PlayerMove _playerMove;
    private Transform _lastPositionSafe;
    public bool DesactiveInputs { set; get; }
    public bool CanActivePowerUpCat { set; get; }
    public Transform LastPositionSafe
    {
        set => _lastPositionSafe = value;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        _playerMove = playerRef.GetComponent<PlayerMove>();
    }

    public void ResetPlayerDead()
    {
        DesactiveInputs = true;
        _playerMove.Turn(1);
        PlayerProperties.instance.StopPlayerInstant(RigidbodyType2D.Static);
        PlayerAnimations.instance.ResetAnimations();
    }
    public void SetSafePositionPlayer()
    {
        playerRef.position = _lastPositionSafe.position;
    }
}

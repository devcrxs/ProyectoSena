using System;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event Action OnShowPauseUI;
    public event Action OnHidePauseUI;
    [SerializeField] private Transform playerRef;
    [SerializeField] private KeyCode keyPause;
    private PlayerMove _playerMove;
    private Transform _lastPositionSafe;
    private bool isPause;
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

    private void Update()
    {
        if (!Input.GetKeyDown(keyPause)) return;
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
            DesactiveInputs = true;
            OnShowPauseUI?.Invoke();
            return;
        }
        ContinueGame();
    }

    public void ContinueGame()
    {
        isPause = false;
        Time.timeScale = 1;
        DesactiveInputs = false;
        OnHidePauseUI?.Invoke();
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

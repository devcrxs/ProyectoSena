using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event Action OnShowPauseUI;
    public event Action OnHidePauseUI;
    [SerializeField] private Transform playerRef;
    [SerializeField] private KeyCode keyPause;
    private PlayerMove _playerMove;
    private Vector2 _lastPositionSafe;
    private bool _isPause;
    public bool DesactiveInputs { set; get; }
    public bool CanActiveJump { set; get; }
    public bool CanActiveDash { set; get; }
    public bool CanActivePowerUpCat { set; get; }
    public Vector2 LastPositionSafe
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
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            playerRef.position = PersistenDataManager.instance.GetSavePositionPlayer();
        }
        _lastPositionSafe = playerRef.position;
        CanActiveJump = PersistenDataManager.instance.IsUnlockJump();
        CanActiveDash = PersistenDataManager.instance.IsUnlockDash();
        CanActivePowerUpCat = PersistenDataManager.instance.IsUnlockCat();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(keyPause)) return;
        _isPause = !_isPause;
        if (_isPause)
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
        _isPause = false;
        Time.timeScale = 1;
        DesactiveInputs = false;
        OnHidePauseUI?.Invoke();
    }

    public void GoMainMenu()
    {
        ContinueGame();
        TransitionManager.instance.TransitionChangeScene("MainMenu");
    }
    public void ResetPlayerDead()
    {
        DesactiveInputs = true;
        PlayerProperties.instance.StopPlayerInstant(RigidbodyType2D.Static);
        PlayerAnimations.instance.ResetAnimations();
    }
    public void SetSafePositionPlayer()
    {
        playerRef.position = _lastPositionSafe;
        _playerMove.Turn(1);
    }
}

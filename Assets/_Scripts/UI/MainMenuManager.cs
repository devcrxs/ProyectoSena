using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image transition;
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        Application.targetFrameRate = 220;
        DOTween.Init();
        transition.DOFade(1,0)
            .OnComplete(() => transition.DOFade(0,2f));
    }

    public void PlayGame(string nameScene)
    {
        transition.DOFade(1, 0.5f).OnComplete(() =>
        {
            if (PlayerPrefs.HasKey("Level"))
            {
                nameScene = PlayerPrefs.GetString("Level") == "House" ? nameScene : PlayerPrefs.GetString("Level");
            }
            SceneManager.LoadScene(nameScene);
        });
    }

    public void OpenSettings()
    {
        Debug.Log("settings");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

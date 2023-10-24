using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PlayGame(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
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

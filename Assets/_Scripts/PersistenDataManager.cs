using UnityEngine;
using UnityEngine.SceneManagement;
public class PersistenDataManager : MonoBehaviour
{
    public static PersistenDataManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        PlayerPrefs.SetString("Level",SceneManager.GetActiveScene().name);
    }

    public void SetUnlockJump()
    {
        PlayerPrefs.SetInt("JumpUnlock",1);
    }

    public void SetUnlockDash()
    {
        PlayerPrefs.SetInt("DashUnlock",1);
    }
    
    public void SetUnlockCat()
    {
        PlayerPrefs.SetInt("CatUnlock",1);
    }

    public bool IsUnlockJump()
    {
        return PlayerPrefs.GetInt("JumpUnlock") != 0;
    }

    public bool IsUnlockDash()
    {
        return PlayerPrefs.GetInt("DashUnlock") != 0;
    }
    
    public bool IsUnlockCat()
    {
        return PlayerPrefs.GetInt("CatUnlock") != 0;
    }

    public void SavePositionPlayer(Vector2 position)
    {
        PlayerPrefs.SetFloat("PositionX",position.x);
        PlayerPrefs.SetFloat("PositionY",position.y);
    }

    public Vector2 GetSavePositionPlayer()
    {
        if (!PlayerPrefs.HasKey("PositionX")) return FindObjectOfType<PlayerProperties>().transform.position;
        if (new Vector2(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY")) == Vector2.zero)
        {
            return FindObjectOfType<PlayerProperties>().transform.position;
        }
        Vector2 position = new Vector2(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"));
        return position;
    }

    public void SaveCurrentCamera(int index)
    {
        PlayerPrefs.SetInt("Camera",index);
    }

    public int GetSaveCamera()
    {
        return PlayerPrefs.HasKey("Camera") ? PlayerPrefs.GetInt("Camera") : 0;
    }
}

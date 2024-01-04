using UnityEngine;
using UnityEditor;/**
public class DebugMenu : EditorWindow
{
    [MenuItem("Tools/DebugMenu")]
    public static void ShowDebugMenu()
    {
        GetWindow(typeof(DebugMenu));
    }

    private void OnGUI()
    {
        GUILayout.Label("Data",EditorStyles.boldLabel);
        if (GUILayout.Button("DeletAllData"))
        {
            DeletAllData();
        }
        if (GUILayout.Button("DeletLastPosition"))
        {
            DeletLastPosition();
        }
        if (GUILayout.Button("ActiveJump"))
        {
            ActiveJump();
        }
        if (GUILayout.Button("ActiveDash"))
        {
            ActiveDash();
        }
        if (GUILayout.Button("ActiveCat"))
        {
            ActiveCat();
        }
        if (GUILayout.Button("ResetLevelSave"))
        {
            ResetLevelSave();
        }
    }

    private void DeletAllData()
    {
        PlayerPrefs.SetFloat("PositionX",0);
        PlayerPrefs.SetFloat("PositionY",0);
        PlayerPrefs.SetInt("JumpUnlock",0);
        PlayerPrefs.SetInt("DashUnlock",0);
        PlayerPrefs.SetInt("CatUnlock",0);
        PlayerPrefs.SetInt("Camera",0);
        PlayerPrefs.SetString("Level","House");
    }
    private void DeletLastPosition()
    {
        PlayerPrefs.SetFloat("PositionX",0);
        PlayerPrefs.SetFloat("PositionY",0);
    }
    private void ActiveJump()
    {
        PlayerPrefs.SetInt("JumpUnlock",1);
    }
    private void ActiveDash()
    {
        PlayerPrefs.SetInt("DashUnlock",1);
    }
    private void ActiveCat()
    {
        PlayerPrefs.SetInt("CatUnlock",1);
    }
    private void ResetLevelSave()
    {
        PlayerPrefs.SetString("Level","House");
    }
}**/

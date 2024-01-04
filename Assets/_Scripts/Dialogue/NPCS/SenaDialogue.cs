using System.Collections;
using UnityEngine;
public class SenaDialogue : TriggerDialogue
{
    [SerializeField] private GameObject cameraMain;
    [SerializeField] private GameObject cameraFinal;
    [SerializeField] private TextDialogue textDialogueFinal;
    [SerializeField] private string nameScene;
    protected override void Start()
    {
        base.Start();
        cameraMain.SetActive(true);
        cameraFinal.SetActive(false);
    }

    protected override void FinishDialogue()
    {
        GameManager.instance.DesactiveInputs = true;
        DialogueManager.instance.OnFinishDialogue -= FinishDialogue;
        textDialogue = textDialogueFinal;
        StartCoroutine(WaitFinishGame());
    }

    private IEnumerator WaitFinishGame()
    {
        yield return new WaitForSeconds(0.6f);
        cameraFinal.SetActive(true);
        cameraMain.SetActive(false);
        yield return new WaitForSeconds(2f);
        IntroductionText.instance.ShowTextIntroduction();
        IntroductionText.instance.OnFinishIntroduction += FinishGame;
    }

    private void FinishGame()
    {
        TransitionManager.instance.TransitionChangeScene(nameScene);
        PlayerPrefs.SetFloat("PositionX",0);
        PlayerPrefs.SetFloat("PositionY",0);
        PlayerPrefs.SetInt("JumpUnlock",0);
        PlayerPrefs.SetInt("DashUnlock",0);
        PlayerPrefs.SetInt("CatUnlock",0);
        PlayerPrefs.SetInt("Camera",0);
        PlayerPrefs.SetString("Level","House");
        IntroductionText.instance.OnFinishIntroduction -= FinishGame;
    }
}

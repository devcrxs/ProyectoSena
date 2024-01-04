using UnityEngine;
public class MountainDialogue : TriggerDialogue
{
    [SerializeField] private GameObject collisionStopPlayer;
    protected override void Start()
    {
        base.Start();
        collisionStopPlayer.SetActive(!PersistenDataManager.instance.IsUnlockCat());
    }

    protected override void FinishDialogue()
    {
        collisionStopPlayer.SetActive(false);
        GameManager.instance.CanActivePowerUpCat = true;
        PersistenDataManager.instance.SetUnlockCat();
        DialogueManager.instance.OnFinishDialogue -= FinishDialogue;
    }
}

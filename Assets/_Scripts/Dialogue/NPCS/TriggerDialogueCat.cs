using UnityEngine;
public class TriggerDialogueCat : DialogueTrigger
{
    [SerializeField] private GameObject collisionStopPlayer;
    private void Start()
    {
        DialogueManager.instance.OnFinishDialogue += ActivatePowerUp;
    }

    private void ActivatePowerUp()
    {
        collisionStopPlayer.SetActive(false);
        GameManager.instance.CanActivePowerUpCat = true;
        DialogueManager.instance.OnFinishDialogue -= ActivatePowerUp;
    }
}

using UnityEngine;
public abstract class TriggerDialogue : Interaction
{
    [SerializeField] protected TextDialogue textDialogue;
    protected virtual void Start()
    {
        DialogueManager.instance.OnFinishDialogue += FinishDialogue;
    }

    protected override void LaunchInteraction()
    {
        DialogueManager.instance.StartDialogue(textDialogue);
    }
    protected abstract void FinishDialogue();
}

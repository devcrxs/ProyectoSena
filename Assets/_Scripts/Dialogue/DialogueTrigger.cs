using UnityEngine;
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private float offsetYInteraction;
    [SerializeField] private TextDialogue textDialogue;
    private bool _isEnter;

    private void Update()
    {
        if (Input.GetKeyDown(DialogueManager.instance.KeyDialogue) && _isEnter)
        {
            LaunchDialogue();
        }
    }

    private void LaunchDialogue()
    {
        DialogueManager.instance.StartDialogue(textDialogue);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        _isEnter = true;
        var position = transform.position;
        position.y += offsetYInteraction;
        InteractableManager.instance.ShowCanvas(position);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _isEnter = false;
        InteractableManager.instance.HideCanvas();
    }
}

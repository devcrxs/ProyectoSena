using UnityEngine;
public abstract class Interaction : CheckCollisionPlayer
{
    [SerializeField] private float offsetYInteraction;
    private bool _isEnter;

    private void Update()
    {
        if (Input.GetKeyDown(DialogueManager.instance.KeyDialogue) && _isEnter)
        {
            LaunchInteraction();
        }
    }

    protected abstract void LaunchInteraction();
    protected override void TriggerPlayerStay(Collider2D player)
    {
        _isEnter = true;
        var position = transform.position;
        position.y += offsetYInteraction;
        InteractableManager.instance.ShowCanvas(position);
    }

    protected override void TriggerPlayerExit(Collider2D player)
    {
        _isEnter = false;
        InteractableManager.instance.HideCanvas();
    }
    protected override void TriggerPlayerEnter(Collider2D player)
    {
    }

    protected override void CollisionPlayer(Collision2D player)
    {
    }
}

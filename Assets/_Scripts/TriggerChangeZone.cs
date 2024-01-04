using UnityEngine;
public class TriggerChangeZone : Interaction
{
    [SerializeField] private string nameScene;
    protected override void LaunchInteraction()
    {
        TransitionManager.instance.TransitionChangeScene(nameScene);
    }
}

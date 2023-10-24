using UnityEngine;
public class AnimationsEvents : MonoBehaviour
{
    [SerializeField] private GameObject graphicsToChange;

    public void StopPlayer()
    {
        PlayerProperties.instance.Rigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    public void Change()
    {
        gameObject.SetActive(false);
        graphicsToChange.SetActive(true);
        PlayerProperties.instance.Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        CameraManager.instance.ResetSizeCamera();
    }

    public void PlayCameraShake()
    {
        CameraManager.instance.PlayCameraShake(2);
    }

    public void StopCameraShake()
    {
        CameraManager.instance.StopCameraShake();
    }

    public void TouchGround()
    {
        if(!PlayerProperties.instance.IsTouchGround()) return;
        PlayerEffects.instance.PlayLandEffect();
    }

    public void DeadPlayerTransition()
    {
        TransitionManager.instance.TransitionDead();
    }
}

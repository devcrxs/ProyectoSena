using UnityEngine;
public class PlayerChange : MonoBehaviour
{
    [SerializeField] private KeyCode keyChange;
    [SerializeField] private GameObject graphicsHuman;
    [SerializeField] private GameObject graphicsCat;

    private void Start()
    {
        graphicsHuman.SetActive(true);
        graphicsCat.SetActive(false);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(keyChange) || !PlayerProperties.instance.IsDynamicBody() ||
            GameManager.instance.DesactiveInputs || !GameManager.instance.CanActivePowerUpCat) return;
        Change();
        CameraManager.instance.ZoomCamera(7,0.5f);
    }

    private void Change()
    {
        PlayerAnimations.instance.CallConvertAnimation();
        PlayerEffects.instance.PlayTransformationEffect();
        AudioManager.instance.PlayTransformation();
    }
}

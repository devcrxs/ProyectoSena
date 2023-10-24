using UnityEngine;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject canvasPause;
    private void Start()
    {
        canvasPause.SetActive(false);
        GameManager.instance.OnShowPauseUI += InstanceOnOnShowPauseUI;
        GameManager.instance.OnHidePauseUI += InstanceOnOnHidePauseUI;
    }

    private void InstanceOnOnHidePauseUI()
    {
        canvasPause.SetActive(false);
    }

    private void InstanceOnOnShowPauseUI()
    {
        canvasPause.SetActive(true);
    }
}

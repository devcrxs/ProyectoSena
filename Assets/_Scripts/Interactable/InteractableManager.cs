using UnityEngine;
using UnityEngine.UI;
public class InteractableManager : MonoBehaviour
{
    [SerializeField] private Sprite spriteKey;
    [SerializeField] private Image imageKey;
    [SerializeField] private Transform canvasInteractable;
    public static InteractableManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        canvasInteractable.gameObject.SetActive(false);
    }

    public void ShowCanvas(Vector2 positionShow)
    {
        Vector2 position = Vector2.Distance(canvasInteractable.position, positionShow) < 0.1f
            ? canvasInteractable.position
            : positionShow;
        canvasInteractable.position = position;
        imageKey.sprite = spriteKey;
        canvasInteractable.gameObject.SetActive(true);
    }

    public void HideCanvas()
    {
        canvasInteractable.gameObject.SetActive(false);
    }
}

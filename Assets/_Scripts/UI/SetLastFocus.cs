using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SetLastFocus : MonoBehaviour
{
    private GameObject _lastSelected;

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == null)
        {
            if (_lastSelected && _lastSelected.gameObject.activeSelf && _lastSelected.GetComponent<Button>() != null && _lastSelected.GetComponent<Button>().interactable)
            {
                EventSystem.current.SetSelectedGameObject(_lastSelected);
            }            
        }
        else
        {
            _lastSelected = EventSystem.current.currentSelectedGameObject;
        }
    }
}

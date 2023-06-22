using System.Collections;
using UnityEngine;
public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    [SerializeField] private SpriteRenderer spriteRendererTransitionCircular;
    [SerializeField] private SpriteRenderer spriteRendererTransitionPanels;
    [SerializeField] private float transitionSpeedCircularIn;
    [SerializeField] private float transitionSpeedCircularOut;
    [SerializeField] private float transitionSpeedPanels;
    private float _currentValueTransition;
    private const string NameTransition = "_CutOff";
    private static readonly int CutOff = Shader.PropertyToID(NameTransition);
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void TransitionDead()
    {
        StartCoroutine(PlayAnimationDead());
    }
    IEnumerator PlayAnimationDead()
    {
        GameManager.instance.ResetPlayerDead();
        while (_currentValueTransition < 1.1f)
        {
            _currentValueTransition += Time.deltaTime * transitionSpeedPanels;
            spriteRendererTransitionPanels.material.SetFloat(CutOff,_currentValueTransition);
            yield return null;
        }
        GameManager.instance.SetSafePositionPlayer();
        yield return new WaitForSeconds(1);
        spriteRendererTransitionCircular.material.SetFloat(CutOff,_currentValueTransition);
        spriteRendererTransitionPanels.material.SetFloat(CutOff,0);
        while (_currentValueTransition > 0.975)
        {
            _currentValueTransition -= Time.deltaTime * transitionSpeedCircularIn;
            spriteRendererTransitionCircular.material.SetFloat(CutOff,_currentValueTransition);
            yield return null;
        }
        
        yield return new WaitForSeconds(1.2f);
        while (_currentValueTransition > 0.667)
        {
            _currentValueTransition -= Time.deltaTime * transitionSpeedCircularOut;
            spriteRendererTransitionCircular.material.SetFloat(CutOff,_currentValueTransition);
            yield return null;
        }
        spriteRendererTransitionCircular.material.SetFloat(CutOff,0);
        GameManager.instance.DesactiveInputs = false;
        PlayerProperties.instance.GoPlayerInstant();
    }
}

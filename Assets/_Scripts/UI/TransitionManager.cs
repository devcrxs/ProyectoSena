using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        StartCoroutine(TransitionCircle(1.099f));
    }

    public void TransitionChangeScene(string nameScene)
    {
        StartCoroutine(ChangeScene(nameScene));
    }
    public void TransitionDead()
    {
        StartCoroutine(PlayAnimationDead());
    }
    private IEnumerator PlayAnimationDead()
    {
        while (_currentValueTransition < 1.1f)
        {
            _currentValueTransition += Time.deltaTime * transitionSpeedPanels;
            spriteRendererTransitionPanels.material.SetFloat(CutOff,_currentValueTransition);
            yield return null;
        }
        GameManager.instance.SetSafePositionPlayer();
        yield return new WaitForSeconds(1);
        PlayerProperties.instance.StopPlayerInstant(RigidbodyType2D.Static);
        GameManager.instance.DesactiveInputs = true;
        spriteRendererTransitionCircular.material.SetFloat(CutOff, _currentValueTransition);
        spriteRendererTransitionPanels.material.SetFloat(CutOff, 0);
        while (_currentValueTransition > 0.975)
        {
            _currentValueTransition -= Time.deltaTime * transitionSpeedCircularIn;
            spriteRendererTransitionCircular.material.SetFloat(CutOff, _currentValueTransition);
            yield return null;
        }

        yield return new WaitForSeconds(1.2f);
        while (_currentValueTransition > 0.667)
        {
            _currentValueTransition -= Time.deltaTime * transitionSpeedCircularOut;
            spriteRendererTransitionCircular.material.SetFloat(CutOff, _currentValueTransition);
            yield return null;
        }

        spriteRendererTransitionCircular.material.SetFloat(CutOff, 0);
        GameManager.instance.DesactiveInputs = false;
        PlayerProperties.instance.GoPlayerInstant();
    }

    private IEnumerator TransitionCircle(float valueTransition)
    {
        spriteRendererTransitionPanels.material.SetFloat(CutOff,1.1f);
        yield return new WaitForSeconds(0.5f);
        spriteRendererTransitionPanels.material.SetFloat(CutOff,0);
        PlayerProperties.instance.StopPlayerInstant(RigidbodyType2D.Static);
        GameManager.instance.DesactiveInputs = true;
        spriteRendererTransitionCircular.material.SetFloat(CutOff, valueTransition);
        spriteRendererTransitionPanels.material.SetFloat(CutOff, 0);
        while (valueTransition > 0.975)
        {
            valueTransition -= Time.deltaTime * transitionSpeedCircularIn;
            spriteRendererTransitionCircular.material.SetFloat(CutOff, valueTransition);
            yield return null;
        }

        yield return new WaitForSeconds(1.2f);
        while (valueTransition > 0.667)
        {
            valueTransition -= Time.deltaTime * transitionSpeedCircularOut;
            spriteRendererTransitionCircular.material.SetFloat(CutOff, valueTransition);
            yield return null;
        }

        spriteRendererTransitionCircular.material.SetFloat(CutOff, 0);
        GameManager.instance.DesactiveInputs = false;
        PlayerProperties.instance.GoPlayerInstant();
    }

    private IEnumerator ChangeScene(string nameScene)
    {
        GameManager.instance.DesactiveInputs = true;
        while (_currentValueTransition < 1.1f)
        {
            _currentValueTransition += Time.deltaTime * transitionSpeedPanels;
            spriteRendererTransitionPanels.material.SetFloat(CutOff,_currentValueTransition);
            yield return null;
        }
        SceneManager.LoadScene(nameScene);
    }
}

using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IntroductionText : MonoBehaviour
{
    [SerializeField] private bool isActiveStart;
    [SerializeField] private Image imageBackground;
    [SerializeField] private Image imageFade;
    [SerializeField] private TextMeshProUGUI textIntroduction;
    [SerializeField] private GameObject canvasIntroduction;
    [SerializeField] private TextDialogue _textDialogue;
    public static IntroductionText instance;
    public event Action OnFinishIntroduction;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        if (isActiveStart)
        {
            ShowTextIntroduction();
            return;
        }
        canvasIntroduction.SetActive(false);
    }

    public void ShowTextIntroduction()
    {
        canvasIntroduction.SetActive(true);
        imageFade.DOFade(1, 0);
        StartCoroutine(WaitShowIntroduction());
    }

    IEnumerator WaitShowIntroduction()
    {
        for (int i = 0; i < _textDialogue.Dialogue.Length; i++)
        {
            imageFade.DOFade(0, 1f);
            textIntroduction.text = _textDialogue.Dialogue[i];
            yield return new WaitForSeconds(2f);
            imageFade.DOFade(1, 1f);
            yield return new WaitForSeconds(1.1f);
        }

        if (isActiveStart)
        {
            textIntroduction.gameObject.SetActive(false);
            imageFade.DOFade(0, 0);
            imageBackground.DOFade(0, 0.5f).OnComplete(()=>canvasIntroduction.SetActive(false));
        }
        else
        {
            OnFinishIntroduction?.Invoke();
        }
    }
}

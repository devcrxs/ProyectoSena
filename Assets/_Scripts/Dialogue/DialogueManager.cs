using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public event Action OnFinishDialogue;
    [SerializeField] private KeyCode keyDialogue;
    [SerializeField] private GameObject canvasDialogue;
    [SerializeField] private TextMeshProUGUI textDialogueTMP;
    [SerializeField] private Image imageDialogue;
    [SerializeField] private float speedDialogue;
    private bool _inDialogue;
    private bool _isFinishLine = true;
    private int _index;
    public KeyCode KeyDialogue => keyDialogue;
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        canvasDialogue.SetActive(false);
    }

    public void StartDialogue(TextDialogue textDialogue)
    {
        GameManager.instance.DesactiveInputs = true;
        PlayerProperties.instance.StopPlayerInstant(RigidbodyType2D.Kinematic);
        PlayerProperties.instance.Rigidbody2D.velocity = Vector2.zero;
        if (!_inDialogue)
        {
            _index = 0;
            _inDialogue = true;
            imageDialogue.sprite = textDialogue.IconDialogue;
            canvasDialogue.SetActive(true);
            StartCoroutine(TypingText(textDialogue));
        }
        else
        {
            NextLine(textDialogue);  
        }
       
    }

    private void StopDialogue()
    {
        _inDialogue = false;
        textDialogueTMP.text = "";
        canvasDialogue.SetActive(false);
        GameManager.instance.DesactiveInputs = false;
        PlayerProperties.instance.GoPlayerInstant();
        OnFinishDialogue?.Invoke();
    }
    
    private void NextLine(TextDialogue textDialogue)
    {
        if (!_isFinishLine) return;
        if (_index < textDialogue.Dialogue.Length - 1)
        {
            _index++;
            StartCoroutine(TypingText(textDialogue));
        }
        else
        {
            StopDialogue();
        }
    }
    
    private IEnumerator TypingText(TextDialogue textDialogue)
    {
        AudioManager.instance.PlayAudioDialogue(textDialogue.AudioClipVoice);
        bool isTag = false;
        bool isFinish = false;
        string tag = "";
        if (_isFinishLine)
        {
            textDialogueTMP.text = "";
            _isFinishLine = false;
            foreach (var letter in textDialogue.Dialogue[_index].ToCharArray())
            {
                if (letter == '<')
                {
                    isTag = true;
                    isFinish = false;
                }
    
                if (letter == '>')
                {
                    isTag = false;
                    tag += letter;
                }

                if (isTag)
                {
                    tag += letter;
                    yield return null;
                }
                else
                {
                    if (tag.Length > 0 && isFinish == false)
                    {
                        textDialogueTMP.text += tag;
                        tag = "";
                        isFinish = true;
                    }
                    else
                    {
                        textDialogueTMP.text += letter;
                        yield return new WaitForSeconds(speedDialogue);     
                    }
                }
                
            }
            AudioManager.instance.StopAudioDialogue();
            _isFinishLine = true;
        }
    }
}

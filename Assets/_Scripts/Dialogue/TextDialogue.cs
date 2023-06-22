using UnityEngine;
[CreateAssetMenu]
public class TextDialogue : ScriptableObject
{
    [SerializeField] private string[] dialogue;
    [SerializeField] private Sprite iconDialogue;
    [SerializeField] private AudioClip _audioClipVoice;
    public Sprite IconDialogue => iconDialogue;
    public string[] Dialogue => dialogue;
    public AudioClip AudioClipVoice=> _audioClipVoice;
}

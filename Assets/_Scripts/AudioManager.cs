using System.Collections;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private float _volumeDialogues;
    [SerializeField] private AudioSource audioSourceDialogues;
    [SerializeField] private AudioSource audioSourcePlayer;
    [SerializeField] private AudioClip audioClipDash;
    [SerializeField] private AudioClip audioClipFootsGrass;
    [SerializeField] private AudioClip audioClipJump;
    [SerializeField] private AudioClip audioClipDead;
    [SerializeField] private AudioClip audioClipTransformation;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        _volumeDialogues = audioSourceDialogues.volume;
    }

    public void PlayAudioDialogue(AudioClip audioClip)
    {
        audioSourceDialogues.volume = _volumeDialogues;
        audioSourceDialogues.loop = true;
        audioSourceDialogues.clip = audioClip;
        audioSourceDialogues.Play();
    }

    public void PlayAudioDash()
    {
        audioSourcePlayer.loop = false;
        audioSourcePlayer.PlayOneShot(audioClipDash);
    }

    public void PlayFootsGrass()
    {
        audioSourcePlayer.loop = false;
        audioSourcePlayer.PlayOneShot(audioClipFootsGrass);
    }
    public void PlayJump()
    {
        audioSourcePlayer.loop = false;
        audioSourcePlayer.PlayOneShot(audioClipJump);
    }

    public void PlayDead()
    {
        audioSourcePlayer.loop = false;
        audioSourcePlayer.PlayOneShot(audioClipDead);
    }
    public void PlayTransformation()
    {
        audioSourcePlayer.loop = false;
        audioSourcePlayer.PlayOneShot(audioClipTransformation);
    }
    public void StopAudioDialogue()
    {
        StartCoroutine(trye());
    }
    private IEnumerator trye ()
    {
        while (audioSourceDialogues.volume > 0)
        {
            audioSourceDialogues.volume -= 0.01f;
            yield return null;
        }
        audioSourceDialogues.loop = false;
        audioSourceDialogues.Stop();
    }
}

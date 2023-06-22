using System;
using System.Collections;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private float volumeDialogues;
    [SerializeField] private AudioSource _audioSourceDialogues;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        volumeDialogues = _audioSourceDialogues.volume;
    }

    public void PlayAudioDialogue(AudioClip audioClip)
    {
        _audioSourceDialogues.volume = volumeDialogues;
        _audioSourceDialogues.loop = true;
        _audioSourceDialogues.clip = audioClip;
        _audioSourceDialogues.Play();
    }

    public void StopAudioDialogue()
    {
        StartCoroutine(trye());
    }
    private IEnumerator trye ()
    {
        while (_audioSourceDialogues.volume > 0)
        {
            _audioSourceDialogues.volume -= 0.01f;
            yield return null;
        }
        _audioSourceDialogues.loop = false;
        _audioSourceDialogues.Stop();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;

    [SerializeField] List<AudioClip> menuSelectSounds = new List<AudioClip>();
    [SerializeField] List<AudioClip> menuSubmitSounds = new List<AudioClip>();

    public void PlayMenuSelect()
    {
        audioSource.PlayOneShot(menuSelectSounds[Random.Range(0, menuSelectSounds.Count)]);
    }
    public void PlayMenuSubmit()
    {
        audioSource.PlayOneShot(menuSubmitSounds[Random.Range(0, menuSelectSounds.Count)]);
    }

}

using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource ballAudioSource;

    [SerializeField] List<AudioClip> menuSelectSounds = new List<AudioClip>();
    [SerializeField] List<AudioClip> menuSubmitSounds = new List<AudioClip>();
    [SerializeField] List<AudioClip> seagullKick = new List<AudioClip>();
    [SerializeField] List<AudioClip> pelicanKick = new List<AudioClip>();
    [SerializeField] AudioClip ball;

    public void PlayMenuSelect()
    {
        audioSource.PlayOneShot(menuSelectSounds[Random.Range(0, menuSelectSounds.Count)]);
    }

    public void PlayMenuSubmit()
    {
        audioSource.PlayOneShot(menuSubmitSounds[Random.Range(0, menuSubmitSounds.Count)]);
    }

    public void PlaySeagullKick()
    {
        audioSource.PlayOneShot(seagullKick[Random.Range(0, seagullKick.Count)]);
    }

    public void PlayPelicanKick()
    {
        audioSource.PlayOneShot(pelicanKick[Random.Range(0, pelicanKick.Count)]);
    }

    public void PlayBall()
    {
        ballAudioSource.pitch = Random.Range(0.75f, 1.25f);
        ballAudioSource.PlayOneShot(ball, 0.25f);
    }

}

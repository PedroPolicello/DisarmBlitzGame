using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip trap;
    public AudioClip timer;
    public AudioClip winGame;
    public AudioClip blind;
    public AudioClip dashCollision;
    public AudioClip dash;
    public AudioClip loseGame;
    public AudioClip completeDisarm;
    public AudioClip startGame;
    public AudioClip steps;
    public AudioClip slow;
    public AudioClip disarmBegin;
    public AudioClip button;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayButton()
    {
        PlaySFX(button);
    }
}

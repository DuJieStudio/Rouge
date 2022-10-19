using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;
    [SerializeField]
    private AudioClip attack1Audio,attack2Audio, shiftAudio, runAudio,jumpAudio;


    private void Awake()
    {
        instance = this;
    }

    public void ShiftAudio()
    {
        audioSource.clip = shiftAudio;
        audioSource.Play();
    }

    public void Attack1Audio()
    {
        audioSource.clip = attack1Audio;
        audioSource.Play();
    }

    public void Attack2Audio()
    {
        audioSource.clip = attack2Audio;
        audioSource.Play();
    }

    public void RunAudio()
    {
        audioSource.clip = runAudio;
        audioSource.Play();
    }

    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
   // public static SoundManager instance;

    public AudioSource audioSource;
    [SerializeField]
    private AudioClip attack1Audio,attack2Audio, shiftAudio, runAudio,jumpAudio,skillAudio,skill_Long;


    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(this);
    }
    //private void Awake()
    //{
    //    instance = this;
    //}

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

    public void SkillAudio()
    {
        audioSource.clip = skillAudio;
        audioSource.Play();
    }

    public void Skill_Long()
    {
        audioSource.clip = skill_Long;
        audioSource.Play();
    }
}

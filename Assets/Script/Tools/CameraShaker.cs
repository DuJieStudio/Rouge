using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance { get; private set; }

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;///
    public  float shakeTimer;
    public  float shakeTimerTotal;
    public float startingIntensity;


    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
           cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
    }


    public void ShakeCamera(float intensity,float frequency, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
        //shakeTimer = 0;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        // while(shakeTimer>0)
        {
            shakeTimer -= Time.deltaTime;
            //      if (shakeTimer <= 0f)
            //      {

            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakeTimerTotal);
            //     }
        }
        else
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
               cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;

        }
       
    }


    //public void HitPause(float duration)
    //{
    //    StartCoroutine(Pause(duration));
    //}

    //IEnumerator Pause(float duration)//อฃถู
    //{
    //    float pauseTime = duration / 60f;
    //    Time.timeScale = 0;
    //    yield return new WaitForSecondsRealtime(pauseTime);
    //    Time.timeScale = 1;     
    //}
}

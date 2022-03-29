using Cinemachine;
using UnityEngine;

//организует тряску камеры

public class CameraShaker : MonoBehaviour
{
    //камера
    CinemachineVirtualCamera camera;
    //зернистость
    CinemachineBasicMultiChannelPerlin perlin;
    //интенсивность тряски
    float startingIntencity;
    //продолжительность тряски
    float delay;
    //оставшееся время тряски
    float lostTime;

    private void OnEnable()
    {
        //подписываемся
        EventManager.OnShakeCameraEvent += ShakeCamera;
    }

    private void Awake()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        perlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    //трясем камеру
    private void ShakeCamera(float intensity, float delay)
    {
        perlin.m_AmplitudeGain = intensity;
        this.delay = delay;
        lostTime = delay;
        startingIntencity = intensity;
    }

    private void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            perlin.m_AmplitudeGain = Mathf.Lerp(startingIntencity, 0f, 1 - (delay / lostTime));
        }
    }

    private void OnDisable()
    {
        //отписываемся
        EventManager.OnShakeCameraEvent -= ShakeCamera;
    }

}

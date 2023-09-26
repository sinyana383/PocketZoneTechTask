using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float shakeAmplitude, float shakeDuration)
    {
        noise.m_AmplitudeGain = shakeAmplitude;
        StartCoroutine(StopShaking(shakeDuration));
    }

    private IEnumerator StopShaking(float shakeDuration)
    {
        yield return new WaitForSeconds(shakeDuration);
        noise.m_AmplitudeGain = 0f; // Stop shaking
    }
}
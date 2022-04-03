using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMShake : MonoBehaviour
{
	private CinemachineVirtualCamera _cmVirtualCamera;
	float shakeTimer;
	CinemachineBasicMultiChannelPerlin perlin;
	// Start is called before the first frame update
	void Start()
    {
		_cmVirtualCamera = GetComponent<CinemachineVirtualCamera>();
		perlin = _cmVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	}

    // Update is called once per frame
    void Update()
    {
		if(shakeTimer > 0){
			shakeTimer -= Time.deltaTime;
			if(shakeTimer <=0){
				perlin.m_AmplitudeGain = 0f;
			}
		}
	}

	public void ShakeCamera(float intensity, float frequency,  float time){
	
		perlin.m_AmplitudeGain = intensity;
		perlin.m_FrequencyGain = frequency;
		shakeTimer = time;
	}
}

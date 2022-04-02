using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	public AudioFX[] audioFXs;

	private void Awake() {
		foreach(AudioFX audioFX in audioFXs) {
			audioFX.source = gameObject.AddComponent<AudioSource>();
			audioFX.source.clip = audioFX.clip;
			audioFX.source.volume = audioFX.volume;
			audioFX.source.pitch = audioFX.pitch;
			audioFX.source.loop = audioFX.looping;
		}
	}

	public void Play(string name){
		AudioFX sfx = Array.Find(audioFXs, audioFX => audioFX.name == name);
		sfx.source.Play();
	}

	public void Stop(string name){
		AudioFX sfx = Array.Find(audioFXs, audioFX => audioFX.name == name);
		sfx.source.Stop();
	}

	public void StopAll(){
		foreach(AudioFX sfx in audioFXs){
			sfx.source.Stop();
		}
	}

	public void FadeIn(string name, float fadeTime, float volume){
		AudioFX sfx = Array.Find(audioFXs, audioFX => audioFX.name == name);
		sfx.volume = 0f;
		sfx.source.Play();
		StartCoroutine(DoFadeIn(sfx.source, fadeTime, volume));
	}

	public void FadeOut(string name, float fadeTime, float volume){
		AudioFX sfx = Array.Find(audioFXs, audioFX => audioFX.name == name);
		sfx.volume = 0f;
		sfx.source.Play();
		StartCoroutine(DoFadeOut(sfx.source, fadeTime, volume));
	}

	public static IEnumerator DoFadeIn(AudioSource source, float duration, float volume){
		source.Play();
		float curTime = 0;
		float start = source.volume;

		while(curTime < duration){
			curTime += Time.deltaTime;
			source.volume = Mathf.Lerp(start, volume, curTime / duration);
			yield return null;
		}

		yield break;
	}

	public static IEnumerator DoFadeOut(AudioSource source, float duration, float volume){
		float curTime = 0;
		float start = source.volume;

		while(curTime < duration){
			curTime += Time.deltaTime;
			source.volume = Mathf.Lerp(start, volume, curTime / duration);
			yield return null;
		}
		source.Stop();

		yield break;
	}
}

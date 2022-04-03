using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
	public MusicFX[] musicFXs;

	private void Awake() {
		foreach(MusicFX musicFX in musicFXs) {
			musicFX.source = gameObject.AddComponent<AudioSource>();
			musicFX.source.clip = musicFX.clip;
			musicFX.source.volume = musicFX.volume;
			musicFX.source.pitch = musicFX.pitch;
			musicFX.source.loop = musicFX.looping;
		}
	}

	public void Play(string name){
		MusicFX sfx = Array.Find(musicFXs, musicFX => musicFX.name == name);
		sfx.source.Play();
	}

	public void Stop(string name){
		MusicFX sfx = Array.Find(musicFXs, musicFX => musicFX.name == name);
		sfx.source.Stop();
	}

	public void StopAll(){
		foreach(MusicFX sfx in musicFXs){
			sfx.source.Stop();
		}
	}

	public void FadeIn(string name, float fadeTime, float volume){
		MusicFX sfx = Array.Find(musicFXs, musicFX => musicFX.name == name);
		sfx.volume = 0f;
		sfx.source.Play();
		StartCoroutine(DoFadeIn(sfx.source, fadeTime, volume));
	}

	public void FadeOut(string name, float fadeTime, float volume){
		MusicFX sfx = Array.Find(musicFXs, musicFX => musicFX.name == name);
		sfx.volume = 0f;
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

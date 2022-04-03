using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Fader : MonoBehaviour
{
    [SerializeField] Image image;
    
    public IEnumerator FadeIn(float time){
        yield return image.DOFade(1f, time).WaitForCompletion();
    }

    public IEnumerator FadeOut(float time){
        yield return image.DOFade(0f, time).WaitForCompletion();
    }
}

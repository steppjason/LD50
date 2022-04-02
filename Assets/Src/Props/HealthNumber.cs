using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthNumber : MonoBehaviour
{
	[SerializeField] float MAX_TIME = 2f;
	[SerializeField] float _decayTime;
	[SerializeField] float _decayRate = 0.1f;
	[SerializeField] float _floatRate;
	[SerializeField] TMP_Text _text;

	// Update is called once per frame
	void Update()
    {
		_text.rectTransform.anchoredPosition = new Vector2(_text.rectTransform.anchoredPosition.x,
						_text.rectTransform.anchoredPosition.y + _floatRate * Time.deltaTime);

		DecayText();
	}

	private void DecayText(){
		_decayTime -= _decayRate * Time.deltaTime;
		_text.color = new Color(1, 0.4235294f, 0, _decayTime / MAX_TIME);

		if(_decayTime <= 0){
			_decayTime = 0;
			gameObject.SetActive(false);
		}
	}

	public void Spawn(){
		gameObject.SetActive(true);
		_text.color = new Color(1, 0.4235294f, 0, 1);
		_decayTime = MAX_TIME;
	}
}


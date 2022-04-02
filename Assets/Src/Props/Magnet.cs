using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
	[SerializeField] GameObject _chip;
	bool _isMagnet;

	private void Update() {
		_chip.GetComponent<Chip>().ActivateMagnet(_isMagnet);
	}

	private void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.name == "PlayerController"){
			_isMagnet = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.name == "PlayerController"){
			_isMagnet = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	[SerializeField] PlayerController _playerController;

    void Update()
    {
		transform.position = new Vector3(_playerController.transform.position.x, _playerController.transform.position.y, transform.position.z);
	}

	public IEnumerator Shake(float duration, float magnitude){
		Vector3 originalPos = transform.localPosition;

		float elapsed = 0.0f;

		while(elapsed < duration){
			float x = Random.Range(-1, 1f) * magnitude;
			float y = Random.Range(-1, 1f) * magnitude;

			transform.localPosition = new Vector3(x, y, originalPos.z);
			elapsed += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = originalPos;
	}
}

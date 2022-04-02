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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[Header("Controllers")]
	[SerializeField] private PlayerController _playerController;
	[SerializeField] private AudioController _audioController;
	[SerializeField] private MusicController _musicController;

	private GameObject _player;
	[Header("Camera")]
	[SerializeField] private Camera _mainCamera;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

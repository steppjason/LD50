using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { TITLE, GAME, DEAD, PAUSE, CREDITS }
public class GameController : MonoBehaviour
{
	[Header("Controllers")]
	[SerializeField] private PlayerController _playerController;
	[SerializeField] private AudioController _audioController;
	[SerializeField] private MusicController _musicController;

	private GameObject _player;
	[Header("Camera")]
	[SerializeField] private Camera _mainCamera;

	public GameState State { get; set; }
	public int KillCount { get; set; }

	// Start is called before the first frame update
	void Start()
    {
		State = GameState.GAME;
	}

    // Update is called once per frame
    void Update()
    {
		if(State == GameState.TITLE)
			Debug.Log("Game is title");

		if(State == GameState.GAME)
			Debug.Log("Game is playing");
        
		if(State == GameState.DEAD)
			Debug.Log("Player is dead");
		
		if(State == GameState.PAUSE)
			Debug.Log("Game is paused");
		
		if(State == GameState.CREDITS)
			Debug.Log("Game is credits");

	}
}

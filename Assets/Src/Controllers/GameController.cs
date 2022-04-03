using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public enum GameState { TITLE, GAME, DEAD, PAUSE, CREDITS }
public class GameController : MonoBehaviour
{
	[Header("Controllers")]
	[SerializeField] private PlayerController _playerController;
	[SerializeField] private AudioController _audioController;
	[SerializeField] private MusicController _musicController;
	[SerializeField] Fader _fader;
	[SerializeField] GameObject _titleScreen;
	[SerializeField] TMP_Text _gameOver;

	private GameObject _player;
	[Header("Camera")]
	[SerializeField] private Camera _mainCamera;

	public GameState State { get; set; }
	public int KillCount { get; set; }

	float alpha;

	// Start is called before the first frame update
	void Start()
    {
		State = GameState.TITLE;
		StartCoroutine(_fader.FadeOut(4f));
	}

    // Update is called once per frame
    void Update()
    {
		if(State == GameState.TITLE){
			if(Input.GetButtonDown("Jump")){
				StartCoroutine(_fader.FadeIn(1f));
				_titleScreen.SetActive(false);
				SceneManager.LoadSceneAsync("Level");
				_musicController.FadeIn("Music", 2f, 0.4f);
				State = GameState.GAME;
			}
		}
		 	
		if(State == GameState.GAME){
			StartCoroutine(_fader.FadeOut(4f));
		}
		
        
		if(State == GameState.DEAD){
			//StartCoroutine(_fader.FadeIn(5f));
			_musicController.FadeOut("Music", 5f, 0);
			alpha += Time.deltaTime * 0.5f;
			_gameOver.color = new Color(1, 0.4817529f, 0, alpha);
		}
		
		
		// if(State == GameState.PAUSE)
		// 	Debug.Log("Game is paused");
		
		// if(State == GameState.CREDITS)
		// 	Debug.Log("Game is credits");

	}

	public void FadeMusic(){
		_musicController.FadeIn("Music", 5f, 0.5f);
	}
}

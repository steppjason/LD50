using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
	[SerializeField] Fader _fader;
	GameController _gameController;
	// Start is called before the first frame update
	void Start()
    {
		_gameController = FindObjectOfType<GameController>();
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")){
			StartCoroutine(_fader.FadeIn(2f));
			_gameController.State = GameState.GAME;
			_gameController.FadeMusic();
			
			SceneManager.UnloadScene("Init");
		}
    }
}

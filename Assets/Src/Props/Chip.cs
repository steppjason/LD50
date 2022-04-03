using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
	GameController _gameController;
	PlayerController _playerController;
	AudioController _audioController;
	[SerializeField] float HEALTH_VALUE = 1f;
	[SerializeField] Rigidbody2D _rb;
	[SerializeField] float _moveSpeed = 1f;
	[SerializeField] float _acceleration = 0.1f;
	[SerializeField] float _deceleration = 1f;
	[SerializeField] Collider2D _collider;

	Vector2 direction;
	bool _isMagnet;

	private void Start() {
		_gameController = FindObjectOfType<GameController>();
		_playerController = FindObjectOfType<PlayerController>();
		_audioController = FindObjectOfType<AudioController>();
	}

    void Update()
    {
		if(_gameController.State == GameState.GAME){
			Decelerate();

			if(_isMagnet)
				MagnetPull();
		}
	}

	public void MagnetPull(){
		_moveSpeed += _acceleration;
		direction = (_playerController.transform.position - transform.position).normalized;
		_rb.velocity = direction * _moveSpeed * Time.deltaTime;
	}

	public void ActivateMagnet(bool activated){
		_isMagnet = activated;
	}

	public void Decelerate(){
		_moveSpeed -= _deceleration;
		if(_moveSpeed <= 0)
			_moveSpeed = 0;

		_rb.velocity = direction * _moveSpeed * Time.deltaTime;
	}

	public void Spawn(){
		_moveSpeed = Random.Range(1000, 2000);
		direction = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.name == "PlayerController"){
			if(_collider.IsTouching(other)){
				_audioController.Play("Pickup");
				gameObject.SetActive(false);
				other.gameObject.GetComponent<PlayerController>().GainHealth(HEALTH_VALUE);
			}
			
		}
	}
}

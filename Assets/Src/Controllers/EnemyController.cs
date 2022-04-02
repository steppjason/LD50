using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	GameController _gameController;
	ChipController _chipController;
	[SerializeField] Rigidbody2D _rb;
	[SerializeField] SpriteRenderer _sprite;
	[SerializeField] float _health = 10f;

	[SerializeField] Vector2 numberOfChips;
	

	[Header("Animation Variables")]
	[SerializeField] public float _runSpeed = 1;
	[SerializeField] public float _idleTime;
	[SerializeField] public float _frameRate;
	[SerializeField] int _direction;

	[Header("Sprite Lists")]
	[SerializeField] List<Sprite> _nSprites;
	[SerializeField] List<Sprite> _neSprites;
	[SerializeField] List<Sprite> _eSprites;
	[SerializeField] List<Sprite> _seSprites;
	[SerializeField] List<Sprite> _sSprites;

	[SerializeField] List<Sprite> _nIdleSprites;
	[SerializeField] List<Sprite> _neIdleSprites;
	[SerializeField] List<Sprite> _eIdleSprites;
	[SerializeField] List<Sprite> _seIdleSprites;
	[SerializeField] List<Sprite> _sIdleSprites;

	public Vector2 velocity;
	public bool isChasingPlayer;

	private void Start() {
		_gameController = FindObjectOfType<GameController>();
		_chipController = FindObjectOfType<ChipController>();
	}

    // Update is called once per frame
    void Update()
    {
		if(_gameController.State == GameState.DEAD){
			_rb.velocity = new Vector2(0, 0);
		}
		
		SetDirection();
		HandleSpriteFlip();
		SetSprite();
		SetAnimation();
	}

	//============================================
	// Actions
	//============================================
	public void TakeDamage(float damage){
		_health -= damage;
		if(_health <= 0)
			Die();
	}

	public void Die(){
		int chipCount = UnityEngine.Random.Range((int)numberOfChips.x, (int)numberOfChips.y);
		for (int i = 0; i < chipCount; i++){
			var chip = _chipController.GetAvailable();
			chip.gameObject.SetActive(true);
			chip.transform.position = new Vector2(gameObject.transform.position.x + UnityEngine.Random.Range(0f, 1f), 
														gameObject.transform.position.y + UnityEngine.Random.Range(0f, 1f));
			chip.Spawn();
		}

		_gameController.KillCount++;

		gameObject.SetActive(false);

	}

	//============================================
	// Handle movement
	//============================================
	private void SetAnimation(){
		if(isChasingPlayer){
			_rb.velocity = velocity * _runSpeed;
		} else {
			_rb.velocity = new Vector2(0, 0);
		}
	}

	private void SetDirection(){

		if(velocity.y > 0.1f){
			if(Mathf.Abs(velocity.x) > 0.1f){
				_direction = 2;
			} else {
				_direction = 1;
			}
		} else if(velocity.y < -0.1f){
			if(Mathf.Abs(velocity.x) > 0.1f){
				_direction = 4;
			} else {
				_direction = 5;
			}
		} else if(Mathf.Abs(velocity.x) > 0.1f){ 
			_direction = 3;
		}

	}

	//============================================
	// Handle sprite 
	//============================================
	private void SetSprite(){
		List<Sprite> runSprite = GetSpriteDirection();
		List<Sprite> idleSprite = GetIdleSpriteDirection();

		if(runSprite != null){
			float time = Time.time - _idleTime;
			int totalFrames = (int)(time * _frameRate);
			int frame = totalFrames % runSprite.Count;
			_sprite.sprite = runSprite[frame];
		} else {
			_idleTime = Time.time;
		}
	}

	List<Sprite> GetSpriteDirection(){
		List<Sprite> sprites = null;

		switch (_direction){
			case 1:
				sprites = _nSprites;
				break;
			case 2:
				sprites = _neSprites;
				break;
			case 3:
				sprites = _eSprites;
				break;
			case 4:
				sprites = _seSprites;
				break;
			case 5:
				sprites = _sSprites;
				break;
		}

		return sprites;
	}

	List<Sprite> GetIdleSpriteDirection(){
		List<Sprite> sprites = null;

		switch (_direction){
			case 1:
				sprites = _nIdleSprites;
				break;
			case 2:
				sprites = _neIdleSprites;
				break;
			case 3:
				sprites = _eIdleSprites;
				break;
			case 4:
				sprites = _seIdleSprites;
				break;
			case 5:
				sprites = _sIdleSprites;
				break;
		}

		return sprites;
	}

	private void HandleSpriteFlip(){
		if(!_sprite.flipX && velocity.x < 0){
			_sprite.flipX = true;
		} else if(_sprite.flipX && velocity.x > 0){
			_sprite.flipX = false;
		}
	}

}

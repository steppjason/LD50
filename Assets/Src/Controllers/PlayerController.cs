using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float MAX_DASH_TIME = 2;
	[SerializeField] float START_DASH_TIME = 0.1f;
	[SerializeField] float _runSpeed = 5;

	[SerializeField] Rigidbody2D _rb;
	[SerializeField] SpriteRenderer _sprite;
	[SerializeField] BoxCollider2D _hitBox;
	[SerializeField] Camera _camera;

	[Header("Animation Variables")]
	[SerializeField] float _frameRate = 12;
	[SerializeField] int _direction = 1;
	[SerializeField] float _dashSpeed = 100;


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
	

	float _idleTime;
	Vector2 _input;
	Vector3 _vector;
	float _decayTime;
	float _playerHealth;
	float _dashTimer;
	float _dashTime;
	Vector3 _dashDirection;
	bool _isDashing;



	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(!_isDashing)
			PlayerInput();

		Dashing();
		SetDirection();
		HandleSpriteFlip();
		
	}

	//============================================
	// Handle player input
	//============================================
	private void PlayerInput(){
		_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
		_vector = _input;

		if(_vector.x != 0 || _vector.y != 0){
			_rb.velocity = _vector * _runSpeed;
			SetSprite(GetSpriteDirection());
		} else {
			_rb.velocity = new Vector2(0, 0);
			SetSprite(GetIdleSpriteDirection());
		}

		if(Input.GetMouseButtonDown(0)){
			Attack();
		}

		if(Input.GetMouseButtonDown(1)){
			Dash();
		}
		
	}


	//============================================
	// Player Actions
	//============================================

	private void Dash(){
		
		if(_dashTimer <= 0){
			_isDashing = true;
			Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
			_dashDirection = (mouseWorldPos - transform.position).normalized;
			_vector = _dashDirection;
			_dashTimer = MAX_DASH_TIME;
			_dashTime = START_DASH_TIME;
		}

	}

	private void Dashing(){
		_dashTimer -= Time.deltaTime;
		_dashTime -= Time.deltaTime;

		if(_dashTime <= 0)
			_isDashing = false;

		if(_isDashing)
			_rb.velocity = _dashDirection * _dashSpeed;
	}

	private void Attack(){
		Debug.Log("Left click pressed");
	}


	//============================================
	// Handle player sprite 
	//============================================
	private void SetDirection(){

		if(_vector.y > 0.1f){
			if(Mathf.Abs(_vector.x) > 0.1f){
				_direction = 2;
			} else {
				_direction = 1;
			}
		} else if(_vector.y < -0.1f){
			if(Mathf.Abs(_vector.x) > 0.1f){
				_direction = 4;
			} else {
				_direction = 5;
			}
		} else if(Mathf.Abs(_vector.x) > 0.1f){ 
			_direction = 3;
		}

	}

	private void SetSprite(List<Sprite> frames){
		
		if(frames != null){
			float time = Time.time - _idleTime;
			int totalFrames = (int)(time * _frameRate);
			int frame = totalFrames % frames.Count;
			_sprite.sprite = frames[frame];
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
		if(!_sprite.flipX && _vector.x < 0){
			_sprite.flipX = true;
		} else if(_sprite.flipX && _vector.x > 0){
			_sprite.flipX = false;
		}
	}

}

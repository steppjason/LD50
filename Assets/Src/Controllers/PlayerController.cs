using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	[SerializeField] GameController _gameController;
	[SerializeField] AudioController _audioController;

	[SerializeField] float MAX_DASH_TIME = 2;
	[SerializeField] float START_DASH_TIME = 0.1f;
	[SerializeField] float MAX_HEALTH = 100f;
	[SerializeField] float MAX_IFRAME = 2f;

	[SerializeField] float _runSpeed = 5;
	[SerializeField] float _health = 50f;
	[SerializeField] float _decayTime = 1f;
	[SerializeField] float _iFrameDecay = 1f;

	[SerializeField] Rigidbody2D _rb;
	[SerializeField] SpriteRenderer _sprite;
	[SerializeField] Camera _camera;
	[SerializeField] GameObject _weapon;
	[SerializeField] PlayerUI _playerUI;

	[SerializeField] LayerMask _layerMask;


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


	SpriteRenderer _spriteRenderer;
	float _idleTime;
	Vector2 _input;
	Vector3 _vector;
	float _playerHealth;

	Vector2 _dashDirection;
	float _dashTimer;
	float _dashTime;
	bool _isDashing;
	bool _canDash = true;
	bool _isIFrame = false;
	float _iFrameTime;
	public float maxHealth;

	CMShake cm_camera;


	void Start()
    {
		_playerUI.health = _health;
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		maxHealth = MAX_HEALTH;

		cm_camera = FindObjectOfType<CMShake>();
	}

    void Update()
    {
			
		DashDecay();

		if(_gameController.State == GameState.GAME){

			if(!_isDashing)
				PlayerInput();

			if(_isIFrame)
				IFrames();

			Aim();
			DrainHealth();
			SetDirection();
			HandleSpriteFlip();

		} else if(_gameController.State == GameState.DEAD){
			_rb.velocity = new Vector2(0, 0);
		}

		UpdateUI();
	}

	private void FixedUpdate() {
		Dashing();
	}


	public void UpdateUI(){
		_playerUI.health = _health;
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
			Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
			_dashDirection = mouseWorldPos - new Vector2(transform.position.x, transform.position.y);
			_dashDirection = _dashDirection.normalized;
			_vector = _dashDirection;
			_dashTimer = MAX_DASH_TIME;
			_dashTime = START_DASH_TIME;
		}

	}

	private void DashDecay(){
		_dashTimer -= Time.deltaTime;
		_dashTime -= Time.deltaTime;
	}

	private void Dashing(){

		Vector2 dashPosition = new Vector2(transform.position.x, transform.position.y) + _dashDirection * _dashSpeed;
		RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, _dashDirection, _dashSpeed, _layerMask);

		if(raycastHit2D.collider != null){
			dashPosition = raycastHit2D.point;
		}

		if(_dashTime <= 0)
			_isDashing = false;

		if(_isDashing && _canDash)
			_rb.MovePosition(dashPosition);


	}

	private void Aim(){
		Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 difference = (mouseWorldPos - new Vector2(_weapon.transform.position.x, _weapon.transform.position.y)).normalized;
		float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		_weapon.transform.rotation = Quaternion.Euler(0f, 0f, angle);
	}

	private void IFrames(){
		_spriteRenderer.color = new Color(1, 1, 1, 0.1f);
		_iFrameTime -= _iFrameDecay * Time.deltaTime;
		if(_iFrameTime <= 0){
			_spriteRenderer.color = new Color(1, 1, 1, 1);
			_isIFrame = false;
		}
	}

	//============================================
	// Health mechanics 
	//============================================

	public void DrainHealth(){
		_health -= Time.deltaTime * (_decayTime + (_gameController.KillCount / 20)) ;
		CheckDeath();
	}

	public void GainHealth(float hp){
		_health += hp;
		_playerUI.SpawnHealthNumber("+" + hp);
		if(_health >= MAX_HEALTH)
			_health = MAX_HEALTH;
	}

	public void TakeDamage(float damage){
		if(!_isIFrame){
			_isIFrame = true;
			_iFrameTime = MAX_IFRAME;
			_health -= damage;
			_audioController.Play("PlayerHit");
			cm_camera.ShakeCamera(5f, 200f, 0.5f);
		}
		
		CheckDeath();
	}

	public void CheckDeath(){
		if(_health <= 0){
			_gameController.State = GameState.DEAD;
		}
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

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	[SerializeField] Rigidbody2D _rb;
	[SerializeField] SpriteRenderer _sprite;

	[Header("Animation Variables")]
	[SerializeField] public float _runSpeed;
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

	public Vector2 _velocity;
	public bool _isMoving;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		SetAnimation();
	}

	//============================================
	// Handle movement
	//============================================
	private void SetAnimation(){
		if(_isMoving){
			_rb.velocity = _velocity * _runSpeed;
		} else {
			_rb.velocity = new Vector2(0, 0);
		}

		SetDirection();
		HandleSpriteFlip();
		SetSprite();
	}

	private void SetDirection(){

		if(_velocity.y > 0){
			if(Mathf.Abs(_velocity.x) > 0){
				_direction = 2;
			} else {
				_direction = 1;
			}
		} else if(_velocity.y < 0){
			if(Mathf.Abs(_velocity.x) > 0){
				_direction = 4;
			} else {
				_direction = 5;
			}
		} else if(Mathf.Abs(_velocity.x) > 0){ 
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
			float time = Time.time - _idleTime;
			int totalFrames = (int)(time * _frameRate);
			int frame = totalFrames % idleSprite.Count;
			_sprite.sprite = idleSprite[frame];
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
		if(!_sprite.flipX && _velocity.x < 0){
			_sprite.flipX = true;
		} else if(_sprite.flipX && _velocity.x > 0){
			_sprite.flipX = false;
		}
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
	[SerializeField] GameController _gameController;
	[SerializeField] AudioController _audioController;
	[SerializeField] float DAMAGE = 5;
	[SerializeField] Collider2D _collider;
	[SerializeField] float attackRate = 4f;
	[SerializeField] SpriteRenderer _sprite;
	[SerializeField] float _decayRate = 1f;

	[SerializeField] ContactFilter2D contactFilter;
	List<Collider2D> hitEnemies = new List<Collider2D>();

	float nextAttackTime = 0f;
	float alpha = 0f;

	void Update()
    {
		if(_gameController.State == GameState.GAME){
			if(Time.time >= nextAttackTime){
				if(Input.GetMouseButtonDown(0)){
					_audioController.Play("Attack2");
					alpha = 1;
					Attack();
					nextAttackTime = Time.time + 1f / attackRate;
				}
			}
		}

		HideSprite();
	}

	private void Attack(){
		Physics2D.OverlapCollider(_collider, contactFilter, hitEnemies);
		foreach(Collider2D enemy in hitEnemies){
			enemy.gameObject.GetComponentInParent<EnemyController>().TakeDamage(DAMAGE);
		}

	}

	private void HideSprite(){ 
		alpha -= Time.deltaTime * _decayRate;
		_sprite.color = new Color(1, 1, 1, alpha);
	}


}

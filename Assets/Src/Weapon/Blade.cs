using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
	[SerializeField] GameController _gameController;
	[SerializeField] float DAMAGE = 5;
	[SerializeField] Collider2D _collider;
	[SerializeField] float attackRate = 3f;
	
	[SerializeField] ContactFilter2D contactFilter;
	List<Collider2D> hitEnemies = new List<Collider2D>();

	float nextAttackTime = 0f;

    void Update()
    {
		if(_gameController.State == GameState.GAME){
			if(Time.time >= nextAttackTime){
				if(Input.GetMouseButtonDown(0)){
					Attack();
					nextAttackTime = Time.time + 1f / attackRate;
				}
			}
		}
    }

	private void Attack(){
		Physics2D.OverlapCollider(_collider, contactFilter, hitEnemies);
		foreach(Collider2D enemy in hitEnemies){
			Debug.Log(enemy);
			enemy.gameObject.GetComponentInParent<EnemyController>().TakeDamage(DAMAGE);
		}

	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
	[SerializeField] float MAX_ATTACK_DELAY = 3;
	[SerializeField] float ATTACK_DAMAGE = 5;

	[SerializeField] EnemyController _enemyController;
	[SerializeField] CircleCollider2D _visionCollider;
	[SerializeField] CircleCollider2D _hitBoxCollider;

	bool _isAttacking;
	float _attackDelay;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Attack();
	}

	private void Attack(){

		if(_isAttacking){
			if(_attackDelay <= 0){
				//Debug.Log("ATTACK THE PLAYER");
				_attackDelay = MAX_ATTACK_DELAY;
			}
		}
		_attackDelay -= Time.deltaTime;
	}

	private void OnTriggerStay2D(Collider2D other) {
		if(_visionCollider.IsTouching(other)){
			_enemyController.isChasingPlayer = true;
			_enemyController.velocity = (other.transform.position - transform.position).normalized;
		}

		if(_hitBoxCollider.IsTouching(other)){
			_enemyController.isChasingPlayer = false;
			_isAttacking = true;
		} else {
			_isAttacking = false;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(!_visionCollider.IsTouching(other)){
			_enemyController.isChasingPlayer = false;
		}
	}
}

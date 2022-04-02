using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
	[SerializeField] float MAX_SPAWN_TIMER = 10f;
	[SerializeField] float _spawnDecay = 1f;
	[SerializeField] int _enemyCount = 200;
	[SerializeField] GameObject _pool;
	[SerializeField] EnemyController _enemy;
	[SerializeField] float _spawnRate;

	GameController _gameController;

	Vector2 defaultPos = new Vector2(-1000, -1000);
	EnemyController[] _enemies;

	bool _spawnEnemy;
	float _spawnTimer;


	void Start()
    {
		_gameController = FindObjectOfType<GameController>();
		CreatePool();
	}

	private void Update() {
		if(_gameController.State == GameState.GAME){
			SpawnEnemy();
		}
	}

	private void SpawnEnemy(){
		if(_spawnEnemy){
			_spawnEnemy = false;
			_spawnTimer = MAX_SPAWN_TIMER;
			var enemy = GetAvailable();
			enemy.gameObject.SetActive(true);
			enemy.transform.position = new Vector2(Random.Range(-10,10), Random.Range(-10,10));
		}

		_spawnTimer -= _spawnDecay * Time.deltaTime;

		if(_spawnTimer <= 0)
			_spawnEnemy = true;
	}

	private void CreatePool(){
		_enemies = new EnemyController[_enemyCount];
		for (int i = 0; i < _enemies.Length; i++){
			_enemies[i] = Instantiate(_enemy, defaultPos, Quaternion.identity);
			_enemies[i].transform.parent = _pool.transform;
			_enemies[i].gameObject.SetActive(false);
		}
	}

	public EnemyController GetAvailable(){
		for (int i = 0; i < _enemies.Length; i++){
			if(!_enemies[i].gameObject.activeInHierarchy){
				return _enemies[i];
			}
		}
		return null;
	}

}

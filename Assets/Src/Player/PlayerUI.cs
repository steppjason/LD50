using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
	[SerializeField] GameController _gameController;
	[SerializeField] PlayerController _playerController;
	[SerializeField] TMP_Text _health;
	[SerializeField] Image _healthBar;

	[SerializeField] TMP_Text _killCount;

	[SerializeField] int _healthUICount = 50;
	[SerializeField] GameObject _pool;
	[SerializeField] HealthNumber _healthUI;

	Vector2 defaultPos = new Vector2(-1000, -1000);
	HealthNumber[] _healthUIs;

	public float health;
	

	private void Start() {
		CreatePool();
		
	}

    void Update()
    {
	
		if(_gameController.State == GameState.DEAD){
			_health.text = "0.000000";
			_healthBar.fillAmount = 0;
			_killCount.text = _gameController.KillCount.ToString();
		}
		else{
			_health.text = health.ToString();
			_killCount.text = _gameController.KillCount.ToString();
			_healthBar.fillAmount = (float)(health / _playerController.maxHealth);
		}

	}

	public void SpawnHealthNumber(string health){
		var ui = GetAvailable();
		ui.GetComponent<TMP_Text>().text = health;
		ui.GetComponent<TMP_Text>().rectTransform.anchoredPosition = new Vector2(Random.Range(0,1f), 0);
		ui.Spawn();
	}


	private void CreatePool(){
		_healthUIs = new HealthNumber[_healthUICount];
		for (int i = 0; i < _healthUIs.Length; i++){
			_healthUIs[i] = Instantiate(_healthUI, defaultPos, Quaternion.identity);
			_healthUIs[i].transform.SetParent(_pool.transform);
			_healthUIs[i].gameObject.SetActive(false);
		}
	}

	public HealthNumber GetAvailable(){
		for (int i = 0; i < _healthUIs.Length; i++){
			if(!_healthUIs[i].gameObject.activeInHierarchy){
				return _healthUIs[i];
			}
		}
		return null;
	}
}

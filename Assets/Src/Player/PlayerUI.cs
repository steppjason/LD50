using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
	[SerializeField] GameController _gameController;
	[SerializeField] TMP_Text _health;

	public float health;

    void Update()
    {
		if(_gameController.State == GameState.DEAD)
			_health.text = "0.000000";
		else 
			_health.text = health.ToString();

	}
}

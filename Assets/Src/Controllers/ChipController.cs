using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipController : MonoBehaviour
{
	[SerializeField] int _chipCount = 200;
	[SerializeField] GameObject _pool;
	[SerializeField] Chip _chip;

	Vector2 defaultPos = new Vector2(-1000, -1000);
	Chip[] _chips;
	
	void Start()
    {
		CreatePool();
	}

	private void CreatePool(){
		_chips = new Chip[_chipCount];
		for (int i = 0; i < _chips.Length; i++){
			_chips[i] = Instantiate(_chip, defaultPos, Quaternion.identity);
			_chips[i].transform.parent = _pool.transform;
			_chips[i].gameObject.SetActive(false);
		}
	}

	public Chip GetAvailable(){
		for (int i = 0; i < _chips.Length; i++){
			if(!_chips[i].gameObject.activeInHierarchy){
				return _chips[i];
			}
		}
		return null;
	}

	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
	[SerializeField] float HEALTH_VALUE = 1f;

	[SerializeField] Collider2D _magnet;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.name == "PlayerController"){
			this.gameObject.SetActive(false);
			other.gameObject.GetComponent<PlayerController>().GainHealth(HEALTH_VALUE);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour {
	void Start() {

	}

	void Update() {

	}

	private void OnTriggerEnter2D(Collider2D collision) {
		//So, here, it should check if they are a enemyGhost then, and if so, notify them about stuff,

		if(collision.TryGetComponent<EnemyGhost>(out EnemyGhost enemyGhost)) {
			
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		//If you are able to escape from the ghost, this should signal to them that they need to look for a new destination.
	}
}

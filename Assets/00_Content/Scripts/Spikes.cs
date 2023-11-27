using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	[Range(0, 10)][SerializeField] private float damage;
	public float Damage { get { return damage; } }

	private void OnTriggerEnter2D(Collider2D collision) {

		if (collision.gameObject.TryGetComponent(out PlayerController playerController)) {
			Debug.Log($"Something just entered {name}s trigger.");

			playerController.TakeDamage(damage);
		}
	}
}

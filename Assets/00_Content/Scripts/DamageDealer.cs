using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {

	[SerializeField] private bool isInstantDeath;

	[Tooltip("If true, this object will return maximum amount of damage on collision")]
	public bool IsInstantDeath { get { return isInstantDeath; } }

	[Range(0, 10)][SerializeField] private float damage;
	public float Damage { get { return damage; } }

	private void OnTriggerEnter2D(Collider2D collision) {

		if (collision.gameObject.TryGetComponent(out Health health)) {
			Debug.Log($"Something just entered {name}s trigger.");

			if(!IsInstantDeath) {
				health.TakeDamage(damage);
			}
			else {
				health.TakeDamage(float.MaxValue);
			}
		}
	}
}
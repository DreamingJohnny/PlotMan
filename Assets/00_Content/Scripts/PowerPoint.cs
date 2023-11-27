using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPoint : MonoBehaviour {

	[Range(0,10)]	[SerializeField] private float value;
	public float Value { get { return value; } }

	private void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log($"Something just entered {name}s trigger.");

		if (collision.gameObject.TryGetComponent(out PlayerController playerController)) {

			playerController.AddPower(value);
			gameObject.SetActive(false);
		}
	}
}
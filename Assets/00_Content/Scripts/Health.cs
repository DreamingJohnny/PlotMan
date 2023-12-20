using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public event EventHandler OnHealthChanged;
	//So, I could either, have another event here, that is just "death", and trigger that when/if it dies.
	//Or I can just have the GameHandler look at the same event, and have that one do it, right?
	//It could even look at the bool whenever... yeah, makes sense.

	[SerializeField] private float currentHealth;
	public float CurrentHealth { get { return currentHealth; } }

	[SerializeField] private float maxHealth;
	public float MaxHealth { get { return maxHealth; } }

	public bool IsDead {
		get {
			if (CurrentHealth <= 0f) return true;
			else return false;
		}
	}

	//TODO: Look at if you want to rename this to something like change health.
	public void TakeDamage(float value) {
		currentHealth -= value;
		OnHealthChanged?.Invoke(this, EventArgs.Empty);
		Debug.Log(currentHealth);
	}
}

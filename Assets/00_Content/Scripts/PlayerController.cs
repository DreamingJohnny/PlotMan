using System;
using System.Collections.Generic;
using UnityEngine;
using static MoveButton;

public class PlayerController : MonoBehaviour {

	public event EventHandler OnHealthChanged;
	public event EventHandler OnPowerChanged;

	[SerializeField] private float health;
	[SerializeField] private float maxHealth;

	public float Health { get { return health; } }

	public float MaxHealth { get { return maxHealth; } }

	[SerializeField] private float currentPower;
	[SerializeField] private float maxPower;
	[SerializeField] private float powerDrain;

	public float CurrentPower { get { return currentPower; } }

	public float MaxPower { get { return maxPower; } }

	[SerializeField] private Sprite turnedOn;
	[SerializeField] private Sprite turnedOff;

	[SerializeField] private bool isTurnedOn;

	public bool IsTurnedOn { get { return isTurnedOn; } }

	private Rigidbody2D rigidBody2D;

	private Queue<Vector2> moves;

	[SerializeField] private float waitTime;
	private float timeWaited;

	private void Start() {
		rigidBody2D = GetComponent<Rigidbody2D>();

		moves = new Queue<Vector2>();
	}

	//So, a function for signing up for the event, and for unsubscribing, and yeah, the move, right, so it receives it, and then unsubscribes.

	private void Update() {
		if (waitTime >= timeWaited) { timeWaited += Time.deltaTime; }
		else {
			timeWaited = 0f;
			DoAMove();
		}

		if(IsTurnedOn) {
			if (currentPower <= 0f) ChangePlayerLight(false);
			else {
				currentPower -= powerDrain * Time.deltaTime;
			}
		}
	}

	private void DoAMove() {
		if(moves.Count >= 1) {
			//Will need to look later so that all the moves are in the correct directions and length as well.
			Vector3 moveDirection = moves.Dequeue();
			rigidBody2D.MovePosition(transform.position + moveDirection);
		}
	}

	public void HandleOnMoveReceived(object sender, OnMoveButtonPressedEventArgs e) {
		Debug.Log(e.Move);
		Vector2 newMove = e.Move;

		moves.Enqueue(newMove);

		//Here, I'll probably want it to unsubscribe from the button as well.

	}

	public void HandleOnPowerButtonClicked(object sender, EventArgs e) {

		ChangePlayerLight();
	}

	private void ChangePlayerLight() {
		
		if(IsTurnedOn) { isTurnedOn = false; }
		else {
			isTurnedOn = true;
		}

		SetPlayerSprite();
	}

	private void ChangePlayerLight(bool value) {
		isTurnedOn = value;

		SetPlayerSprite();
	}

	private void SetPlayerSprite() {
		if(IsTurnedOn) {
			GetComponent<SpriteRenderer>().sprite = turnedOn;
		}
		else {
			GetComponent<SpriteRenderer>().sprite = turnedOff;
		}
	}

	public void AddPower(float value) {
		currentPower += value;
		OnPowerChanged?.Invoke(this, EventArgs.Empty);
		Debug.Log(currentPower);
	}

	public void TakeDamage(float value) {
		health -= value;
		OnHealthChanged?.Invoke(this, EventArgs.Empty);
		Debug.Log(health);
	}

}

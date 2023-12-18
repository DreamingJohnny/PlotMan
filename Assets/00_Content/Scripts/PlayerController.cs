using System;
using System.Collections.Generic;
using UnityEngine;
using static MoveButton;

public class PlayerController : MonoBehaviour {

	public event EventHandler OnHealthChanged;
	public event EventHandler OnPowerChanged;

	[SerializeField] private float health;
	public float Health { get { return health; } }

	[SerializeField] private float maxHealth;
	public float MaxHealth { get { return maxHealth; } }

	[SerializeField] private float currentPower;
	public float CurrentPower { get { return currentPower; } }

	[SerializeField] private float maxPower;
	public float MaxPower { get { return maxPower; } }

	[Tooltip("Power amount drained per s while light is active")]
	[SerializeField] private float powerDrain;

	[SerializeField] private Sprite turnedOn;
	[SerializeField] private Sprite turnedOff;

	[SerializeField] private bool isTurnedOn;

	public bool IsTurnedOn { get { return isTurnedOn; } }

	private Rigidbody2D rigidBody2D;

	private Queue<Move_Enum> moves;

	[SerializeField] private float waitTime;
	private float timeWaited;

	private Cell currentCell;

	//TODO: Fix so that this is supplied from somewhere instead of being set like this, in a menu.
	//TODO: Now THIS is where I'm at, currently I dislike how this needs to know about cells and stuff, it should be possible to remake it as a list of vectors, the midpoints,
	//And then have it move from one of those to the next, I'll fix that tomorrow.
	//Although, it still needs to know about the grid, because it needs to be able to talk to it's own cell...
	//No, I'm wrong, it needs the grid thingy to be set, so it needs a collection to it here.

	public Grid Grid { get; set; }

	private void Start() {
		rigidBody2D = GetComponent<Rigidbody2D>();

		moves = new Queue<Move_Enum>();
	}

	public void TeleportToCell(Cell cell) {
		currentCell = cell;
		transform.position = cell.MidPoint;
	}

	private void Update() {
		if (waitTime >= timeWaited) { timeWaited += Time.deltaTime; }
		else {
			timeWaited = 0f;
			DoAMove();
		}

		if (IsTurnedOn) {
			if (currentPower <= 0f) ChangePlayerLight(false);
			else {
				currentPower -= powerDrain * Time.deltaTime;
			}
		}
	}

	private void DoAMove() {
		if (moves.Count >= 1) {
			Move_Enum moveDirection = moves.Dequeue();

			if (Grid.IsDirectionClear(moveDirection, currentCell)) {
				
				//So, here I want to, in the end, do a list of cells, based on the list of moves, so it should return a cell, and then I can begin to move towards that cell.
				//To return a cell it needs an index, the index is the index of my current cell, but also, I need to add for the move, but I don't know which is the x and which is the y...
				//So, do I need to set up variables for these things? send in the move Direction and get back... two values? Hm. I suppose I could return a vector2, although it bothers me,
				//It's what I've done so far, so it should be alright.
				//Would it then be better to clamp it before sending it in?

				Cell destination = Grid.GetCell(currentCell, moveDirection); //So, take the index it has, adds the index based on move,
				rigidBody2D.MovePosition(destination.MidPoint);
				//Later on, this shouldn't be set here, but should instead be set once the player object has reached the cell in question.
				//Which should probably be part of the update loop? Or should there be something in the update loop that looks at if the move is finished, while the move is being carried out by a coroutine?
				currentCell = destination;
			}

		}
	}

	public void HandleOnMoveReceived(object sender, OnMoveButtonPressedEventArgs e) {

		Move_Enum newMove = e.Move_Enum;
		moves.Enqueue(newMove);

		//Here, I'll probably want it to unsubscribe from the button as well.

	}

	public void HandleOnPowerButtonClicked(object sender, EventArgs e) {

		ChangePlayerLight();
	}

	private void ChangePlayerLight() {

		if (IsTurnedOn) { isTurnedOn = false; }
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
		if (IsTurnedOn) {
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
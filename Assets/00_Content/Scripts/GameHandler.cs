using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	[SerializeField] private PlayerController player;

	[SerializeField] private LevelHandler levelHandler;

	[SerializeField] private UIHandler uIHandler;

	void Start() {
		SetUpGame();

	}

	void Update() {

	}

	private void HandleOnHealthChanged(object sender, EventArgs e) {
		//TODO: Check if this works, and if it works, check how I should better protect against it.
		Health health = (Health)sender;
		
		if (health.IsDead) DoGameOver();
		
	}

	private void SetUpGame() {

		levelHandler.SetUpLevel();

		player.gameObject.GetComponent<Health>().OnHealthChanged += HandleOnHealthChanged;
		player.TeleportToCell(levelHandler.GetPlayerSpawnCell());
		player.Grid = levelHandler.Grid;

		uIHandler.SetUpAllButtons(player);
	}

	private void DoGameOver() {

		Debug.Log($"{name} just recieved an event indicating that the player is dead, and has gone into the game over function.");
	}
}

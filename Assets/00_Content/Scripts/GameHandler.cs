using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	[SerializeField] private PlayerController player;

	[SerializeField] private LevelHandler levelHandler;

	void Start() {

		levelHandler.SetUpLevel();

		player.TeleportToCell(levelHandler.GetPlayerSpawnCell());

		player.Grid = levelHandler.Grid;

	}

	void Update() {

	}
}

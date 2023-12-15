using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour {

	[Header("Grid Settings")]
	[SerializeField] private int gridWidth;
	[SerializeField] private int gridHeight;
	[SerializeField] private float cellSize;
	[SerializeField] private Vector2 startingOffset;

	//Here I'll make a series of values for things like,
	//where enemies should spawn,
	//where the player should spawn,
	//and where the powers should spawn.
	//Later on, it seems logical that these will also become part of the "levelData".
	//TODO: Also, these should only be able to be ints, never floats, might be worth looking into how to set that up as well.
	[SerializeField] private Vector2 playerSpawnIndex;
	[SerializeField] private List<Vector2> enemySpawnIndexes;
	[SerializeField] private List<Vector2> powerUpSpawnIndexes;

	//Later on, this will contain all of the information needed for setting up the maze, including the values currently stored under "Grid Settings".
	//On top of that, this will later also hold the sprites for the maze.
	[SerializeField] SO_LevelData levelData;

	[SerializeField] private PowerPoint powerPoint;
	[SerializeField] private EnemyGhost enemyGhost;

	private Grid grid;

	//Actually doubtful if this header is needed.
	[Header("Pathfinding")]
	private Pathfinding pathfinder;

	void Start() {
		
	}

	void Update() {

	}

	public void SetUpLevel() {
		grid = new Grid(gridWidth, gridHeight, cellSize, startingOffset);

		pathfinder = new Pathfinding(grid);

		SpawnPowerPoints();

		SpawnEnemies();
	}

	private void SpawnEnemies() {
		if (enemyGhost == null) return;

		foreach (Vector2 vector2 in enemySpawnIndexes) {
			Instantiate(enemyGhost, grid.GetCellMidPoint(Mathf.CeilToInt(vector2.x), Mathf.CeilToInt(vector2.y)), UnityEngine.Quaternion.identity);
		}
	}

	private void SpawnPowerPoints() {
		if (powerPoint == null) return;

		foreach (Vector2 vector2 in powerUpSpawnIndexes) {
			Instantiate(powerPoint, grid.GetCellMidPoint(Mathf.CeilToInt(vector2.x), Mathf.CeilToInt(vector2.y)), UnityEngine.Quaternion.identity);
		}
	}

	/// <summary>
	/// Returns the real-world coordinates for where the player should spawn.
	/// </summary>	
	public Vector2 GetPlayerSpawnPoint() {
		return grid.GetCellMidPoint(Mathf.CeilToInt(playerSpawnIndex.x), Mathf.CeilToInt(playerSpawnIndex.y));
	}
}

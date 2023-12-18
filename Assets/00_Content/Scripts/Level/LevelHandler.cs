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

	public Grid Grid { get; set; }
	
	private Pathfinding pathfinder;

	void Start() {
		
	}

	void Update() {

	}

	public void SetUpLevel() {
		Grid = new Grid(gridWidth, gridHeight, cellSize, startingOffset);

		pathfinder = new Pathfinding(Grid);

		SpawnPowerPoints();

		SpawnEnemies();
	}

	private void SpawnEnemies() {
		if (enemyGhost == null) return;

		foreach (Vector2 vector2 in enemySpawnIndexes) {
			Instantiate(enemyGhost, Grid.GetCellMidPoint(Mathf.CeilToInt(vector2.x), Mathf.CeilToInt(vector2.y)), UnityEngine.Quaternion.identity);
		}
	}

	private void SpawnPowerPoints() {
		if (powerPoint == null) return;

		foreach (Vector2 vector2 in powerUpSpawnIndexes) {
			Instantiate(powerPoint, Grid.GetCellMidPoint(Mathf.CeilToInt(vector2.x), Mathf.CeilToInt(vector2.y)), UnityEngine.Quaternion.identity);
		}
	}

	/// <summary>
	/// Returns the real-world coordinates for where the player should spawn.
	/// </summary>	
	public Cell GetPlayerSpawnCell() {
		return Grid.GetCell(Mathf.CeilToInt(playerSpawnIndex.x), Mathf.CeilToInt(playerSpawnIndex.y));
	}
}

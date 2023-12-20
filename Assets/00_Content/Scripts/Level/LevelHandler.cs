using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

	//Later on, this will contain all of the information needed for setting up the maze, including the values currently stored under "Grid Settings".
	//On top of that, this will later also hold the sprites for the maze.
	[SerializeField] SO_LevelData levelData;

	[SerializeField] private PowerPoint powerPoint;
	[SerializeField] private EnemyGhost enemyGhost;
	[SerializeField] private DamageDealer spikes;
	[SerializeField] private Pusher pusher;

	public Grid Grid { get; set; }
	
	private Pathfinding pathfinder;

	void Start() {
		
	}

	void Update() {

	}

	public void SetUpLevel() {
		Grid = new Grid(levelData.GridWidth, levelData.GridHeight, levelData.CellSize, levelData.StartingOffset, levelData.SO_CellDatas);

		pathfinder = new Pathfinding(Grid);

		SpawnPowerPoints();

		SpawnHazards();

		SpawnEnemies();
	}

	private void SpawnHazards() {
		foreach (SO_HazardData spikesHazard in levelData.SO_SpikesSpawnDatas) {
			Instantiate(spikes,
				Grid.GetCellMidPoint(spikesHazard.IndexX, spikesHazard.IndexY),
				Quaternion.Euler(GetHazardRotation(spikesHazard)));
		}

		foreach (SO_HazardData pusherHazard in levelData.SO_PushersSpawnDatas) {
			Instantiate(pusher,
					Grid.GetCellMidPoint(pusherHazard.IndexX, pusherHazard.IndexY),
					Quaternion.Euler(GetHazardRotation(pusherHazard)));
		}
	}

	private Vector3 GetHazardRotation(SO_HazardData hazardData) {
		switch (hazardData.Facing) {
			case Facing_Enum.NORTH:
				return new Vector3(0, 0, 0);
			case Facing_Enum.EAST:
				return new Vector3(0, 0, -90);
			case Facing_Enum.SOUTH:
				return new Vector3(0, 0, 180);
			case Facing_Enum.WEST:
				return new Vector3(0, 0, 90);
			default:
				Debug.Log($"The {name} was unable to set the correct rotation of the hazard based on the hazardData.");
				return new Vector3(0, 0, 0);
		}
	}

	private void SpawnEnemies() {
		if (enemyGhost == null) return;

		foreach (Vector2 vector2 in levelData.EnemySpawnIndexes) {
			Instantiate(enemyGhost, Grid.GetCellMidPoint(Mathf.CeilToInt(vector2.x), Mathf.CeilToInt(vector2.y)), UnityEngine.Quaternion.identity);
		}
	}

	private void SpawnPowerPoints() {
		if (powerPoint == null) return;

		foreach (Vector2 vector2 in levelData.PowerPointSpawnIndexes) {
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

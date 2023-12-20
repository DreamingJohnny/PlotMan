using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelHandler : MonoBehaviour {

	//This will later also hold the sprites for the maze.
	[SerializeField] SO_LevelData levelData;

	[SerializeField] private EnemyGhost enemyGhost;

	[SerializeField] private PowerPoint powerPoint;
	private GameObject powerPointsParentObject;

	[SerializeField] private DamageDealer spike;
	private GameObject spikesParentObject;

	[SerializeField] private Pusher pusher;
	private GameObject pushersParentObject;

	public Grid Grid { get; set; }

	private Pathfinding pathfinder;

	public void SetUpLevel() {
		Grid = new Grid(levelData.GridWidth, levelData.GridHeight, levelData.CellSize, levelData.StartingOffset, levelData.SO_CellDatas);

		pathfinder = new Pathfinding(Grid);

		SpawnPowerPoints();

		SpawnHazards();

		SpawnEnemies();
	}

	private void SpawnPowerPoints() {
		if (powerPoint == null) return;

		powerPointsParentObject = new GameObject();

		foreach (Vector2 vector2 in levelData.PowerPointSpawnIndexes) {
			Instantiate(powerPoint,
				Grid.GetCellMidPoint(Mathf.CeilToInt(vector2.x), Mathf.CeilToInt(vector2.y)),
				Quaternion.identity,
				powerPointsParentObject.transform);
		}
	}

	private void SpawnHazards() {
		if (spike == null) return;
		else {
			spikesParentObject = new GameObject();

			foreach (SO_HazardData spikesHazard in levelData.SO_SpikesSpawnDatas) {
				Instantiate(spike,
					Grid.GetCellMidPoint(spikesHazard.IndexX, spikesHazard.IndexY),
					Quaternion.Euler(GetHazardRotation(spikesHazard)),
				spikesParentObject.transform);
			}
		}

		if (pusher == null) return;
		else {
			pushersParentObject = new GameObject();

			foreach (SO_HazardData pusherHazard in levelData.SO_PushersSpawnDatas) {
				Instantiate(pusher,
						Grid.GetCellMidPoint(pusherHazard.IndexX, pusherHazard.IndexY),
						Quaternion.Euler(GetHazardRotation(pusherHazard)),
						pushersParentObject.transform);
			}
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

	/// <summary>
	/// Returns the cell where the player should spawn on the grid.
	/// </summary>	
	public Cell GetPlayerSpawnCell() {
		return Grid.GetCell(Mathf.CeilToInt(levelData.PlayerSpawnIndex.x), Mathf.CeilToInt(levelData.PlayerSpawnIndex.y));
	}
}

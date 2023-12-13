using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour {

	[Header("Grid Settings")]
	[SerializeField] private int gridWidth;
	[SerializeField] private int gridHeight;
	[SerializeField] private float cellSize;
	[SerializeField] private Vector2 startingOffset;

	//Later on, this will contain all of the information needed for setting up the maze, including the values currently stored under "Grid Settings".
	//On top of that, this will later also hold the sprites for the maze.
	[SerializeField] SO_LevelData levelData;

	private Grid grid;

	//Actually doubtful if this header is needed.
	[Header("Pathfinding")]
	private Pathfinding pathfinder;



	void Start() {

	}

	void Update() {

	}
}

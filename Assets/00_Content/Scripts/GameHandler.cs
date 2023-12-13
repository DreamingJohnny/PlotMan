using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	[SerializeField] private PlayerController player;

	[SerializeField] private LevelHandler levelHandler;

	[Header("Grid Settings")]
	[SerializeField] private int gridWidth;
	[SerializeField] private int gridHeight;
	[SerializeField] private float cellSize;
	[SerializeField] private Vector2 startingOffset;

	private Grid grid;

	[Header("Pathfinding")]
	//TODO: Later on, it's possible we'll want the pathfinder to handle the grid, and hold that, rather than, as now, having the gameHandler hold it.
	//Although, I suppse it would make more sense if both pathfinding and the grid, and the grid setup, lay on a separate "Level" object. Then, gameHandler could just communicate with it.
	private Pathfinding pathfinder;

	[SerializeField] private PathTester pathTester;


	void Start() {
		grid = new Grid(gridWidth, gridHeight, cellSize, startingOffset);
		grid.DebugShowTextOnCells();

		pathfinder = new Pathfinding(grid);

		player.SetPosition(GetCellMidPoint(0, 0));

		pathTester.TestPath(pathfinder, grid);
	}

	void Update() {

	}

	public Vector2 GetCellMidPoint(int x, int y) {
		return grid.GetCellMidPoint(x, y);
	}
}

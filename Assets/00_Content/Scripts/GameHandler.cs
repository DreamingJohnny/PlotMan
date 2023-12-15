using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	[SerializeField] private PlayerController player;

	[SerializeField] private LevelHandler levelHandler;

	//[Header("Grid Settings")]
	//[SerializeField] private int gridWidth;
	//[SerializeField] private int gridHeight;
	//[SerializeField] private float cellSize;
	//[SerializeField] private Vector2 startingOffset;

	//private Grid grid;

	[Header("Pathfinding")]
	//TODO: Later on, it's possible we'll want the pathfinder to handle the grid, and hold that, rather than, as now, having the gameHandler hold it.
	//Although, I suppse it would make more sense if both pathfinding and the grid, and the grid setup, lay on a separate "Level" object. Then, gameHandler could just communicate with it.

	//private Pathfinding pathfinder;

	//[SerializeField] private PathTester pathTester;
	//This is also completely for testing, and should soon be removed.
	[SerializeField] private EnemyGhost enemyGhost;

	[SerializeField] private List<Vector2> testRoute;

	void Start() {
		//grid = new Grid(gridWidth, gridHeight, cellSize, startingOffset);

		//pathfinder = new Pathfinding(grid);

		//Actually, later on, gameHandler should be sending in the SO_LevelData here, to tell the level to set it all up.
		levelHandler.SetUpLevel();

		player.SetPosition(levelHandler.GetPlayerSpawnPoint());

		//pathTester.TestPath(pathfinder, grid);

		//enemyGhost.TeleportToCell(grid.GetCell(10, 10));

		//enemyGhost.SetRoute(pathfinder.GetPath(enemyGhost.CurrentCell, grid.GetCell(12, 8)));
	}

	void Update() {

	}

	//public Vector2 GetCellMidPoint(int x, int y) {
	//	return grid.GetCellMidPoint(x, y);
	//}
}

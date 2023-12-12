using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	[SerializeField] private PlayerController player;

	[Header("Grid Settings")]
	[SerializeField] private int gridWidth;
	[SerializeField] private int gridHeight;
	[SerializeField] private float cellSize;
	[SerializeField] private Vector2 startingOffset;

	private Grid grid;
	
	
	void Start() {
		grid = new Grid(gridWidth, gridHeight, cellSize, startingOffset);
		grid.DebugShowTextOnCells();

		player.SetPosition(GetCellMidPoint(0, 0));
	}

	void Update() {

	}

	public Vector2 GetCellMidPoint(int x, int y) {
		Vector2 v = grid.GetCellMidPoint(x, y);
		return v;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	[SerializeField] private int gridWidth;
	[SerializeField] private int gridHeight;
	[SerializeField] private float cellSize;
	[SerializeField] private Vector2 startingOffset;

	private Grid grid;
	
	
	void Start() {
		grid = new Grid(gridWidth, gridHeight, cellSize, startingOffset);
		grid.DebugShowTextOnCells();
	}

	void Update() {

	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Grid {
	//TODO: Add contidions to setters here so that they must remain positive
	private int width;
	public int Width { get { return width; } set { width = value; } }

	//TODO: Add contidions to setters here so that they must remain positive
	private int height;
	public int Height { get { return height; } set { height = value; } }

	//TODO: Add contidions to setters here so that they must remain positive
	private float cellSize;
	public float CellSize { get { return cellSize; } set { cellSize = value; } }

	//Since the pathfinding calculator is currently using the actual dimensions of the grid,
	//This value might be completely unneccessary.
	//TODO: look into if it should look at this value instead, if it should be multiplied by this, or what?
	private int traversalCost = 10;
	public int TraversalCost { get { return traversalCost; } }

	private Cell[,] cells;

	private Vector2 startPoint;

	public Grid(int width, int height, float cellSize, Vector2 startPoint) {
		this.width = width;
		this.height = height;
		this.cellSize = cellSize;

		this.startPoint = startPoint;

		SetUpCells();
	}

	/// <summary>
	/// Creates a new grid with new cells with new values.
	/// </summary>
	private void SetUpCells() {

		//TODO: This function should be able to set up the cells based on what it reads from a data containter, most likely a scriptable object.

		cells = new Cell[width, height];

		for (int x = 0; x < cells.GetLength(0); x++) {
			for (int y = 0; y < cells.GetLength(1); y++) {
				cells[x, y] = new Cell(this, x, y, SetCellMidPoint(x, y), 0, true, true, true, true);
			}
		}
	}

	private Vector2 SetCellMidPoint(int width, int height) {

		Vector2 temp = new((width * cellSize) + (cellSize * .5f), (height * cellSize) + (cellSize * .5f));

		return startPoint + temp;
	}

	/// <summary>
	/// Returns a Vector2 for the midpoint of the cell at the given index of the array.
	/// </summary>
	/// <param name="width">index along the first dimension</param>
	/// <param name="height">index along the second dimension</param>
	/// <returns>Vector2 for the middle of the cell</returns>
	/// <exception cref="NotImplementedException"></exception>
	public Vector2 GetCellMidPoint(int width, int height) {

		if (width >= cells.GetLength(0) || height >= cells.GetLength(1)) throw new NotImplementedException();

		if (cells[width, height] == null) throw new NotImplementedException();

		return cells[width, height].MidPoint;

	}

	public void ResetCells() {
		for (int x = 0; x < cells.GetLength(0); x++) {
			for (int y = 0; y < cells.GetLength(1); y++) {
				cells[x, y].PreviousCell = null;
				cells[x, y].WalkingFromStartCost = int.MaxValue;
				cells[x, y].HeuristicCost = 0;
				cells[x, y].CalculateTotalCost();
			}
		}
	}

	public Cell GetCell(int x, int y) {
		return cells[x, y];
	}
	//A function that tells you what cell you're in
}

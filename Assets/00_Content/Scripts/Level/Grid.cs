using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using UnityEngine.EventSystems;

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

	/// <summary>
	/// The coordinates the grid should begin from creating the cells from.
	/// </summary>
	private Vector2 startPoint;

	public Grid(int width, int height, float cellSize, Vector2 startPoint, SO_CellData[] sO_CellDatas) {
		this.width = width;
		this.height = height;
		this.cellSize = cellSize;

		this.startPoint = startPoint;

		SetUpCells(sO_CellDatas);
	}

	/// <summary>
	/// Creates a new grid with new cells with new values.
	/// </summary>
	private void SetUpCells(SO_CellData[] sO_CellDatas) {

		//TODO: This function should be able to set up the cells based on what it reads from a data containter, most likely a scriptable object.

		cells = new Cell[width, height];

		int i = 0;

		while (i < sO_CellDatas.Length) {

			for (int x = 0; x < cells.GetLength(0); x++) {
				for (int y = 0; y < cells.GetLength(1); y++) {
					cells[x, y] = new Cell(this, x, y, SetCellMidPoint(x, y), 0, sO_CellDatas[i].IsOpenNorth, sO_CellDatas[i].IsOpenEast, sO_CellDatas[i].IsOpenSouth, sO_CellDatas[i].IsOpenWest);
					i++;
				}
			}
		}
	}

	/// <summary>
	/// Checks the Move_Enum it recieves against the Move_Enum of the cell, to see if the cell is clear in all four othogonal directions. Returns true or false
	/// </summary>
	/// <param name="moveDirection"></param>
	/// <param name="currentCell"></param>
	/// <returns></returns>
	public bool IsDirectionClear(Move_Enum moveDirection, Cell currentCell) {

		switch (moveDirection) {
			case Move_Enum.NORTH:
				if (currentCell.IsOpenNorth) return true;
				else return false;
			case Move_Enum.EAST:
				if (currentCell.IsOpenEast) return true;
				else return false;
			case Move_Enum.SOUTH:
				if (currentCell.IsOpenSouth) return true;
				else return false;
			case Move_Enum.WEST:
				if (currentCell.IsOpenWest) return true;
				else return false;
			case Move_Enum.WAIT:
				Debug.Log("The PlayerController just tried to see if the direction was clear on a move with the enum WAIT. This doesn't make sense, and shouldn't happen.");
				return true;
			default:
				return false;
		}
	}

	/// <summary>
	/// Calculate a vector for the midpoint of a cell in the grid.
	/// </summary>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <returns></returns>
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

	/// <summary>
	/// Returns the cell at a given index of the grid.
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns></returns>
	public Cell GetCell(int x, int y) {
		if (cells[x, y] != null) { return cells[x, y]; }
		else {
			if (x > Width) {
				Debug.Log("Tried to get a cell at an index greater than the width of the grid.");
				return null;
			}
			else if (x < 0) {
				Debug.Log("Tried to get a cell with a negative width index.");
				return null;
			}
			else if (y > Height) {
				Debug.Log("Tried to get a cell at an index greater than the height of the grid.");
				return null;
			}
			else if (y < 0) {
				Debug.Log("Tried to get a cell with a negative height index.");
				return null;
			}
		}

		Debug.Log("The grid was unable to return a cell for the given index.");
		return null;
	}

	/// <summary>
	/// Returns the neighbouring cell in the direction given by the Move_Enum as long as it is within the grid.
	/// </summary>
	/// <param name="currentCell"></param>
	/// <param name="move_Enum"></param>
	/// <returns></returns>
	public Cell GetCell(Cell currentCell, Move_Enum move_Enum) {
		switch (move_Enum) {
			case Move_Enum.NORTH:
				if (currentCell.IndexY + 1 >= Height) {
					Debug.Log("The grid has been asked to return a cell that is outside of the grid.");
					return currentCell;
				}
				else {
					return cells[currentCell.IndexX, currentCell.IndexY + 1];
				}

			case Move_Enum.EAST:
				if (currentCell.IndexX + 1 >= Width) {
					Debug.Log("The grid has been asked to return a cell that is outside of the grid.");
					return currentCell;
				}
				else {
					return cells[currentCell.IndexX + 1, currentCell.IndexY];
				}

			case Move_Enum.SOUTH:
				if (currentCell.IndexY - 1 < 0) {
					Debug.Log("The grid has been asked to return a cell that is outside of the grid.");
					return currentCell;
				}
				else {
					return cells[currentCell.IndexX, currentCell.IndexY - 1];
				}

			case Move_Enum.WEST:
				if (currentCell.IndexX - 1 < 0) {
					Debug.Log("The grid has been asked to return a cell that is outside of the grid.");
					return currentCell;
				}
				else {
					return cells[currentCell.IndexX - 1, currentCell.IndexY];
				}

			case Move_Enum.WAIT:
				Debug.Log("The grid is asked to return a cell, but has been given the Wait command, it will return the cell it was given for now.");
				return currentCell;

			default:
				Debug.Log("The grid is asked to return a cell but cannot read the move enum that should govern it");
				return null;
		}
	}

}

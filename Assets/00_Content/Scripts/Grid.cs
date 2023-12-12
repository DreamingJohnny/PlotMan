using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Grid 
{
	private int width;
	private int height;
	private float cellSize;

	private Cell[,] cells;

	private Vector2 startPoint;

	public Grid(int width, int height, float cellSize, Vector2 startPoint) {
		this.width = width;
		this.height = height;
		this.cellSize = cellSize;

		cells = new Cell[this.width, this.height];

		this.startPoint = startPoint;

		SetUpCells();
	}

	private void SetUpCells() {
		//Double loop here, going through all of the cells, give each cell it's midpoint, and other values, one by one.
		//Later on we will want this one to read off a data, probably a scriptable object, as it sets the values of the grid.
		//So, this one, maybe it should reset the whole array of cells as it begins? So that we can be sure that they are correct etc.

		for (int x = 0; x < cells.GetLength(0); x++) {
			for (int y = 0; y < cells.GetLength(1); y++) {
				//So here we'll want to take each cell, and give it all of the values that it wants.
				cells[x, y] = new Cell(GetCellMidPoint(x, y), 0, true, true, true, true);
			}
		}
	}

	public void DebugShowTextOnCells() {
		for (int x = 0; x < cells.GetLength(0); x++) {
			for (int y = 0; y < cells.GetLength(1); y++) {
				UtilsClass.CreateWorldText(cells[x, y].ToString(), null, GetCellMidPoint(x, y), 20, Color.white, TextAnchor.MiddleCenter);
			}
		}
	}

	//TODO: Check on this later...
	//So, this one can be calculated here, but it should be given to the cell, as its midpoint, as the cell is created.
	public Vector2 GetCellMidPoint(int width, int height) {
		//This needs to check if width and height is within the bounds of the grid before it does anything else!

		Vector2 temp = new Vector2((width * cellSize) + (cellSize * .5f), (height * cellSize) + (cellSize * .5f));

		return startPoint + temp;
	}


	//A function that tells you what cell you're in
}

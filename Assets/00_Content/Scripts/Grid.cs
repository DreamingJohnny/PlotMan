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

	private int[,] cells;

	private Vector2 startPoint;

	public Grid(int width, int height, float cellSize, Vector2 startPoint) {
		this.width = width;
		this.height = height;
		this.cellSize = cellSize;

		cells = new int[this.width, this.height];

		this.startPoint = startPoint;
	}

	public void DebugShowTextOnCells() {
		for (int x = 0; x < cells.GetLength(0); x++) {
			for (int y = 0; y < cells.GetLength(1); y++) {
				UtilsClass.CreateWorldText(cells[x, y].ToString(), null, GetCellMidPoint(x, y), 20, Color.white, TextAnchor.MiddleCenter);
			}
		}
	}

	//TODO: Check on this later...
	public Vector2 GetCellMidPoint(int width, int height) {
		//This needs to check if width and height is within the bounds of the grid before it does anything else!

		Vector2 temp = new Vector2((width * cellSize) + (cellSize * .5f), (height * cellSize) + (cellSize * .5f));

		return (startPoint + temp);
	}


	//A function that tells you what cell you're in
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell {

	private Vector2 midPoint;

	private int walkingCost;
	private int heuristicCost;
	private int totalCost;

	private Cell previousCell;

	private bool isOpenNorth;
	private bool isOpenEast;
	private bool isOpenSouth;
	private bool isOpenWest;

	public Cell(Vector2 midPoint, int walkingCost, bool isOpenNorth, bool isOpenEast, bool isOpenSouth, bool isOpenWest) {

		this.midPoint = midPoint;

		this.walkingCost = walkingCost;
		heuristicCost = 0;
		totalCost = 0;
		previousCell = null;

		this.isOpenNorth = isOpenNorth;
		this.isOpenEast = isOpenEast;
		this.isOpenSouth = isOpenSouth;
		this.isOpenWest = isOpenWest;
	}

	public Vector2 MidPoint { get { return midPoint; } }

	public int WalkingCost { get { return walkingCost; } }

	public bool IsOpenNorth { get { return isOpenNorth; } }

	public bool IsOpenEast { get { return isOpenEast; } }

	public bool IsOpenSouth { get { return isOpenSouth; } }

	public bool IsOpenWest { get { return isOpenWest; } }

	public Cell PreviousCell { get { return previousCell; } }
}

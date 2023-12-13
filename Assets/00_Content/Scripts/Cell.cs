using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell {

	private Grid parentGrid;

	//TODO: These should get setters ensuring that they stay positive.
	public int IndexX { get; set; }
	public int IndexY { get; set; }

	private Vector2 midPoint;
	public Vector2 MidPoint { get { return midPoint; } }

	//Since the pathfinding calculator is currently using the actual dimensions of the grid,
	//This value might be completely unneccessary.
	//TODO: look into if it should look at this value instead, if it should be multiplied by this, or what?
	private int traversalCost;
	public int TraversalCost { get { return traversalCost; } }

	private int walkingFromStartCost;

	public int WalkingFromStartCost { get { return walkingFromStartCost; } set { walkingFromStartCost = value; } }

	private int heuristicCost;
	public int HeuristicCost { get { return heuristicCost; } set { heuristicCost = value; } }

	private int totalCost;
	public int TotalCost { get { return totalCost; } set { totalCost = value; } }

	//Would there ever actually be a point of doing a getter and setter like this?
	//Since it doesn't contain any rules for what you may set the value to.
	private Cell previousCell;
	public Cell PreviousCell { get { return previousCell; } set { previousCell = value; } }

	private bool isOpenNorth;
	public bool IsOpenNorth { get { return isOpenNorth; } }

	private bool isOpenEast;
	public bool IsOpenEast { get { return isOpenEast; } }

	private bool isOpenSouth;
	public bool IsOpenSouth { get { return isOpenSouth; } }

	private bool isOpenWest;
	public bool IsOpenWest { get { return isOpenWest; } }


	public Cell(Grid parentGrid, int indexX, int indexY, Vector2 midPoint, int traversalCost, bool isOpenNorth, bool isOpenEast, bool isOpenSouth, bool isOpenWest) {

		this.parentGrid = parentGrid;
		IndexX = indexX;
		IndexY = indexY;

		this.midPoint = midPoint;

		this.traversalCost = traversalCost;
		heuristicCost = 0;
		totalCost = 0;
		previousCell = null;

		this.isOpenNorth = isOpenNorth;
		this.isOpenEast = isOpenEast;
		this.isOpenSouth = isOpenSouth;
		this.isOpenWest = isOpenWest;
	}

	public void CalculateTotalCost() {
		TotalCost = WalkingFromStartCost + HeuristicCost;
	}
}
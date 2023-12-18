using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding {

	private Grid grid;

	public Grid Grid { get { return grid; } set { grid = value; } }

	private List<Cell> openCells;

	private List<Cell> closedCells;

	public Pathfinding(Grid grid) {
		this.grid = grid;

	}

	public List<Cell> GetPath(Cell start, Cell goal) {
		openCells = new List<Cell>() { start };
		closedCells = new List<Cell>();

		grid.ResetCells();

		start.TotalCost = 0;
		start.HeuristicCost = GetOrthogonalDistanceBetween(start.MidPoint, goal.MidPoint);
		start.CalculateTotalCost();

		while (openCells.Count > 0) {
			Cell currentCell = GetCurrentCell();
			if (currentCell == goal) {
				Debug.Log("You've reached the end goal!");
				return CalculatePath(goal);
			}

			openCells.Remove(currentCell);
			closedCells.Add(currentCell);

			foreach (Cell neighbour in GetNeighbors(currentCell)) {
				if (closedCells.Contains(neighbour)) continue;

				int checkWalkFromStarCost = currentCell.WalkingFromStartCost + GetOrthogonalDistanceBetween(currentCell.MidPoint, neighbour.MidPoint);
				if(checkWalkFromStarCost < neighbour.WalkingFromStartCost) {
					neighbour.PreviousCell = currentCell;
					neighbour.WalkingFromStartCost = checkWalkFromStarCost;
					neighbour.HeuristicCost = GetOrthogonalDistanceBetween(neighbour.MidPoint, goal.MidPoint);
					neighbour.CalculateTotalCost();

					if (!openCells.Contains(neighbour)) {
						openCells.Add(neighbour);
					}
				}
			}
		}


		Debug.Log($"The pathfinder was unable to find a path that reached the goal.");
		return null;
	}

	private List<Cell> CalculatePath(Cell goalCell) {

		List<Cell> path = new List<Cell>();
		path.Add(goalCell);
		Cell currentCell = goalCell;


		while (currentCell.PreviousCell != null) {
			path.Add(currentCell);
			currentCell = currentCell.PreviousCell;
		}

		path.Reverse();

		return path;
	}

	/// <summary>
	/// Returns a list of orthogonally neighbouring cells.
	/// </summary>
	/// <param name="cell"></param>
	/// <returns></returns>
	private List<Cell> GetNeighbors(Cell cell) {
		List<Cell> neighbors = new List<Cell>();

		if (cell.IsOpenNorth && cell.IndexY + 1 <= grid.Height) {
			neighbors.Add(grid.GetCell(cell.IndexX, cell.IndexY + 1));
		}

		if (cell.IsOpenEast && cell.IndexX + 1 <= grid.Width) {
			neighbors.Add(grid.GetCell(cell.IndexX + 1, cell.IndexY));
		}

		if (cell.IsOpenSouth && cell.IndexY - 1 > 0) {
			neighbors.Add(grid.GetCell(cell.IndexX, cell.IndexY - 1));
		}

		if (cell.IsOpenSouth && cell.IndexX - 1 > 0) {
			neighbors.Add(grid.GetCell(cell.IndexX - 1, cell.IndexY));
		}

		return neighbors;
	}

	/// <summary>
	/// returns an int for the sum of the differences in x and y, as an absolute value. Assumes there can be no diagonal movement.
	/// </summary>
	/// <param name="start"></param>
	/// <param name="goal"></param>
	/// <returns></returns>
	private int GetOrthogonalDistanceBetween(Vector2 start, Vector2 goal) {
		float distance = Mathf.Abs(start.x - goal.x) + Mathf.Abs(start.y + goal.y);

		return Mathf.CeilToInt(distance);
	}
	//TODO: Look at if I actually don't want this one, and instead should just "set" the starting cell, seeing as I already know that I mean.
	//TODO: This one also has a strange function name, that should be fixed to something more appropriate
	private Cell GetCurrentCell() {
		Cell lowestTotalCostCell = openCells[0];

		for (int i = 0; i < openCells.Count; i++) {
			if (openCells[i].TotalCost < lowestTotalCostCell.TotalCost) {
				lowestTotalCostCell = openCells[i];
			}
		}

		return lowestTotalCostCell;
	}
}

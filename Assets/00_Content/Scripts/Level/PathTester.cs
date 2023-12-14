using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTester : MonoBehaviour {

	//So, thinking on this, this one should send in two cells from the grid, and then it should recieve a list of cells,
	//and then print the values of those cells one by one? Sure


	void Start() {

	}

	void Update() {

	}

	public void TestPath(Pathfinding pathfinder, Grid grid) {
		ShowPath(pathfinder.GetPath(grid.GetCell(0, 0), grid.GetCell(5, 5)));
	}

	public void ShowPath(List<Cell> cells) {

		foreach (Cell cell in cells) {
			Debug.Log($"{cell.MidPoint}");
		}



	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelDataScriptableObject", order = 2)]
public class SO_LevelData : ScriptableObject {

	[Header("Grid Settings")]
	[SerializeField] [Min(1)] private int gridWidth;
	public int GridWidth { get { return gridWidth; } }

	[SerializeField] [Min(1)] private int gridHeight;
	public int GridHeight { get { return gridHeight; } }

	[SerializeField] [Min(0)] private float cellSize;
	public float CellSize { get { return cellSize; } }

	[SerializeField] private Vector2 startingOffset;
	public Vector2 StartingOffset { get { return startingOffset; } }

	//Later on, it seems logical that these will also become part of the "levelData".
	//TODO: Also, these should only be able to be ints, never floats, might be worth looking into how to set that up as well.
	[SerializeField] private SO_CellData[] sO_CellDatas;
	public SO_CellData[] SO_CellDatas { get { return sO_CellDatas; } }

	[SerializeField] private Vector2 playerSpawnIndex;
	public Vector2 PlayerSpawnIndex { get { return playerSpawnIndex; } }

	[SerializeField] private List<Vector2> enemySpawnIndexes;
	public List<Vector2> EnemySpawnIndexes { get { return enemySpawnIndexes; } }

	[SerializeField] private List<Vector2> powerUpSpawnIndexes;
	public List<Vector2> PowerPointSpawnIndexes { get { return powerUpSpawnIndexes; } }

}

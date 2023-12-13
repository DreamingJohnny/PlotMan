using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CellDataScriptableObject", order = 3)]
public class SO_CellData : ScriptableObject {

	[SerializeField] private bool isOpenNorth;
	public bool IsOpenNorth { get { return isOpenNorth; } }

	[SerializeField] private bool isOpenEast;
	public bool IsOpenEast { get { return isOpenEast; } }

	[SerializeField] private bool isOpenSouth;
	public bool IsOpenSouth { get { return isOpenSouth; } }

	[SerializeField] private bool isOpenWest;
	public bool IsOpenWest { get { return isOpenWest; } }
}

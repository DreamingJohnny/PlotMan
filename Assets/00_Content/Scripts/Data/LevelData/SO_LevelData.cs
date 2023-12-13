using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelDataScriptableObject", order = 2)]
public class SO_LevelData : ScriptableObject {

	[SerializeField] private SO_CellData[] sO_CellDatas;
}

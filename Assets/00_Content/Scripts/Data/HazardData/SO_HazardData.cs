using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HazardDataScriptableObject", order = 4)]
public class SO_HazardData : ScriptableObject {

	[SerializeField][Min(0)] private int indexX;
	public int IndexX { get { return indexX; } }

	[SerializeField][Min(0)] private int indexY;
	public int IndexY { get { return indexY; } }

	[SerializeField] private Facing_Enum facing;
	public Facing_Enum Facing { get { return facing; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MoveActionScriptableObject", order = 1)]
public class SO_MoveAction : ScriptableObject {

	[SerializeField] private Sprite buttonSprite;

	[SerializeField] private Vector2 move;

	[SerializeField] private Move_Enum move_Direction;

	public Move_Enum Move_Direction { get { return move_Direction; } }

	public Vector2 Move { get { return move; } }	

	public Sprite ButtonSprite { get { return buttonSprite; } }

}

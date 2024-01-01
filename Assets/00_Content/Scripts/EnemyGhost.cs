using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyGhost : MonoBehaviour {

	public event EventHandler OnNeedsDestination;

	[SerializeField] private float speed;
	public float Speed { get { return speed; } }

	Rigidbody2D rigidBody2D;

	public List<Cell> Route;

	public Cell CurrentCell { get; set; }

	public bool HasDestination {
		get {
			if (Route != null && Route.Count > 0) {
				return true;
			}
			else {
				return false;
			}
		}
	}
	public Vector2 GetDestination {
		get {
			if (HasDestination) {
				return Route[Route.Count].MidPoint;
			}
			else {
				Debug.Log($"{name} does not have a destination");
				return Vector2.zero;
			}
		}
	}

	void Start() {
		rigidBody2D = GetComponent<Rigidbody2D>();
		Debug.Assert(rigidBody2D);
	}

	private void FixedUpdate() {
		DoMove();
	}

	public void TeleportToCell(Cell cell) {

		CurrentCell = cell;
		transform.position = cell.MidPoint;
	}

	public void SetRoute(List<Cell> cells) {
		Route = cells;
	}

	private void DoMove() {
		if (!HasDestination) {
			Debug.Log($"{name} has no destination and will therefore now trigger the event asking for a new one.");
			
			OnNeedsDestination?.Invoke(this, EventArgs.Empty);

			return;
		}

		//How does this work? Is in a lossless implicit conversion to convert to Vector3 from Vector2 in this way?
		if (transform.position == (Vector3)Route[0].MidPoint) {
			CurrentCell = Route[0];
			Route.RemoveAt(0);

		}
		else {
			rigidBody2D.MovePosition(Vector2.MoveTowards(transform.position, Route[0].MidPoint, (speed * Time.deltaTime)));
			//transform.position = Vector2.MoveTowards(transform.position, Route[0].MidPoint, (speed * Time.deltaTime));
		}
	}
}

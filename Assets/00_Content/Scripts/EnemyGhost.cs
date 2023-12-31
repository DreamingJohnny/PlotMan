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


	//However, there is a problem with when this value should be set, and how, so this cell should be set to null when? As soon as the enemy has received a route?
	//When the light goes out?
	private Cell quarryCell;
	public Cell QuarryCell { get { return quarryCell; } }

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

	private void OnEnable() {
		rigidBody2D = GetComponent<Rigidbody2D>();
		Debug.Assert(rigidBody2D);

		//TODO: This might be very unneccessary, right? Should I remove it completely maybe?
		quarryCell = null;
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

	public void SetQuarry(Cell cell) {
		//So this enemy should now send up an event with a cell, and ask if it could get a route?
		
		//So, this one should then also be used to set the cell to null? Yeah, so it is set to null when the light is turned off then?

		//So, this one gets a quarry, sets it, and then asks for a 

		quarryCell = cell;

		//If the quarryCell isn't null, then that means that it has been updated, and so a new route is needed,
		//If QuarryCell is null, then the ghost should continue towards it's latest destination
		if(QuarryCell != null) OnNeedsDestination?.Invoke(this, EventArgs.Empty);
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

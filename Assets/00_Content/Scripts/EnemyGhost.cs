using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyGhost : MonoBehaviour {

	[SerializeField] private float speed;
	public float Speed { get { return speed; } }

	Rigidbody2D rigidBody2D;

	public List<Cell> Route;

	public Cell CurrentCell { get; set; }

	public bool HasDestination {
		get {
			if(Route != null && Route.Count > 0) {
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

	void Update() {

	}

	//TODO: Since the enemy is kinematic, this should probably be moved to ordinary Update, right?
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
		if (!HasDestination) return;
		
		//How does this work? Is in a lossless implicit conversion?
		if(transform.position == (Vector3)Route[0].MidPoint) {
			CurrentCell = Route[0];
			Route.RemoveAt(0);
		}
		else {
			//So, here it should move towards the midpoint of the next closest cell.
			transform.position = Vector2.MoveTowards(transform.position, Route[0].MidPoint, (speed * Time.fixedDeltaTime));
			
		}
	}

	//So, this is the ghost, it needs to have a cell that it's currently occupying, or position,
	//and then it needs to be able to ask for a new one. And it needs to be able to say: "Hello! I need a new position!"
	//And it needs to be able to recieve a new one, and then take that and do stuff with it.
	//So a public function for recieving a new list of Cells called route,

	//So, it needs to be able to go through the route, and then, hm,
	//So, it should compare it's currentPosition, towards the next position, which is, hm, the next cell along the route. And when they are correct, then they move things around.

	//actually, it needs a path, where it takes the midPoints one by one, moves towards that one, and then looks for the next.



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.Rendering.DebugUI;

public class PlayerLight : MonoBehaviour {

	private List<EnemyGhost> pursuingEnemies;

	private Cell currentCell;

	private void Start() {
	}

	private void OnEnable() {
		pursuingEnemies = new List<EnemyGhost>();
		currentCell = null;

		GetComponent<Collider2D>().enabled = false;
		GetComponent<Light2DBase>().enabled = false;
	}

	private void OnDisable() {
		//So, here we'll need to clear the list of pursuingEnemies then?
		//This will be in a pretty weird case, since this shouldn't happen that often, the whole script isn't disabled after all.
	}

	public void SetCurrentCell(Cell cell) {
		currentCell = cell;
		Debug.Log($"New cell has index: {currentCell.IndexX}, {currentCell.IndexY}.");

		foreach (EnemyGhost enemy in pursuingEnemies) {
			enemy.SetQuarry(currentCell);

		}
	}

	public void TurnOnLight(bool value) {

		GetComponent<Collider2D>().enabled = value;
		GetComponent<Light2DBase>().enabled = value;

		if (!value && pursuingEnemies.Count > 0) {

			foreach (EnemyGhost enemy in pursuingEnemies) {
				enemy.SetQuarry(null);
			}

			pursuingEnemies.Clear();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {

		if (collision.TryGetComponent(out EnemyGhost enemyGhost)) {
			if (!pursuingEnemies.Contains(enemyGhost)) {
				enemyGhost.SetQuarry(currentCell);
				pursuingEnemies.Add(enemyGhost);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.TryGetComponent(out EnemyGhost enemyGhost)) {
			enemyGhost.SetQuarry(null);
			pursuingEnemies.Remove(enemyGhost);
		}
	}
}
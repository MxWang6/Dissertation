using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	private enum Mode {
		UPDATE_PATH_AFTER_EACH_STEP, 
		UPDATE_ONLY_WHEN_MONSTER_CAN_ATTACH_PATH,
		UPDATE_ONLY_WHEN_MONSTER_MOVES_POSITION,
		UPDATE_ONLY_WHEN_MONSTER_MOVES_POSITION_WITHIN_ELLIPSE
	}
		
	private static Mode ALGORITHM_MODE = Mode.UPDATE_ONLY_WHEN_MONSTER_MOVES_POSITION_WITHIN_ELLIPSE;

	private BoardManager boardManager;

	public Position currentPosition;
	public Position targetPosition;
	public Position nextStepPosition;

	public Vector3 mousePoint;

	public Animator animator;

	private List<Node> path = new List<Node> ();

	public Vector3 p;

	private float startTime;
	private float speed = 3.0F;
	private Tile nextStepTile;

	private List<MonsterMoveEvent> monsterMoveEvents;

	// Use this for initialization
	void Start () {

		//Get a component reference to the player's animator component
		animator = GetComponent<Animator>();
		//Get a component reference to the gameobject's gameManger component
		boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
		currentPosition = targetPosition = nextStepPosition = new Position (transform.position);
		// Substribe to notifications from the monster.
		NotificationSystem.subscribe (this);
		startTime = Time.time;
		monsterMoveEvents = new List<MonsterMoveEvent> ();

		Debug.Log("Player start position: " + currentPosition);
	}

	void Update() {
		// Check mouse event
		checkMouseEvent ();

		// When player moves to the next step.
		if (transform.position == nextStepPosition.toVector3()) {
			if (nextStepTile != null) {
				nextStepTile.highlighted = false;
			}
			currentPosition = nextStepPosition;

			switch (ALGORITHM_MODE) {
			case Mode.UPDATE_PATH_AFTER_EACH_STEP:
				recalculateThePath ();
				break;
			case Mode.UPDATE_ONLY_WHEN_MONSTER_CAN_ATTACH_PATH:
				if (canMonsterAttackPath()) {
					recalculateThePath ();
				}
				break;
			case Mode.UPDATE_ONLY_WHEN_MONSTER_MOVES_POSITION:
				if (hasSomeMonsterMoved()) {
					recalculateThePath ();
					monsterMoveEvents.Clear ();
				}
				break;
			case Mode.UPDATE_ONLY_WHEN_MONSTER_MOVES_POSITION_WITHIN_ELLIPSE:
				if (hasSomeMonsterMovedWithinEllipseArea ()) {
					recalculateThePath ();
					monsterMoveEvents.Clear ();
				}
				break;
			default:
				break;
			}


			if (path.Count > 0) {
				nextStepTile = path [0].tile;
				path.RemoveAt (0);
				nextStepPosition = nextStepTile.getPosition ();

				// animation of player
				if (nextStepPosition.x > currentPosition.x) {
					animator.SetTrigger ("PlayerRight");
				} else if (nextStepPosition.x < currentPosition.x) {
					animator.SetTrigger ("PlayerLeft");
				} else if (nextStepPosition.x == currentPosition.x && nextStepPosition.y < currentPosition.y) {
					animator.SetTrigger ("PlayerIdle");
				} else if (nextStepPosition.x == currentPosition.x && nextStepPosition.y > currentPosition.y) {
					animator.SetTrigger ("PlayerBack");
				}
			}
		}
			
		float distance = Vector2.Distance (transform.position, nextStepPosition.toVector2());
		float duration = Time.time - startTime;
		float distCovered = duration * speed;
		transform.position = Vector2.Lerp (transform.position, nextStepPosition.toVector2(), distCovered / distance);
		startTime = startTime + duration;
	}

	private void checkMouseEvent(){
		
		if (Input.GetMouseButtonDown (0)) {

			Debug.Log ("Pressed left click.");
			mousePoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			Debug.Log (Input.mousePosition);
			Debug.Log ("target position" + mousePoint);
			targetPosition = new Position (mousePoint.x, mousePoint.y);

			lock (path) {
				//start to find path
				path.ForEach ((step) => step.tile.highlighted = false);
				path.Clear ();
				path.AddRange (boardManager.getGridWorld ().findPath (currentPosition, targetPosition));
			}
		}
	}

	private void recalculateThePath() {
		// recalculate the path every step it goes until it reaches the target positin.
		if (targetPosition != currentPosition) {
			path.ForEach ((step) => step.tile.highlighted = false);
			path.Clear ();
			path.AddRange (boardManager.getGridWorld ().findPath (currentPosition, targetPosition));
		}
	}

	public void monsterMoved(MonsterMoveEvent moveEvent) {
		monsterMoveEvents.Add (moveEvent);
	}

	private bool canMonsterAttackPath() {
		foreach (Node step in path) {
			if (step.tile.monsterCost != 0) {
				return true;
			}
		}
		return false;
	}

	private bool hasSomeMonsterMoved() {
		if (monsterMoveEvents.Count > 0) {
			return true;
		}
		return false;
	}
	private bool hasSomeMonsterMovedWithinEllipseArea() {
		if (!hasSomeMonsterMoved()) {
			return false;
		}

		// ellipse between current position and target position
		int centerX = targetPosition.x > currentPosition.x ? (targetPosition.x - currentPosition.x)/2 : (currentPosition.x - targetPosition.x)/2;
		int centerY = targetPosition.y > currentPosition.y ? (targetPosition.y - currentPosition.y)/2 : (currentPosition.y - targetPosition.y)/2;

		double a = Math.Sqrt (Math.Pow (currentPosition.x - centerX, 2) + Math.Pow (currentPosition.y - centerY, 2));
		int pathLength = path.Count; // because grid size is 1.
		double angle1 = Math.Acos(a*2/pathLength);
		double angle2 = Math.Acos (Math.Abs (currentPosition.x - centerX) / a);
		double angle = angle1 + angle2;

		double diffX = targetPosition.x > currentPosition.x ? pathLength / 2 * Math.Cos (angle) : -(pathLength / 2 * Math.Cos (angle));
		double diffY = targetPosition.y > currentPosition.y ? pathLength / 2 * Math.Sin (angle) : -(pathLength / 2 * Math.Sin (angle));
		double bX = currentPosition.x + diffX;
		double bY = currentPosition.y + diffY;
		double b = Math.Sqrt(Math.Pow(bX - centerX, 2) + Math.Pow(bY - centerY, 2));

		foreach (MonsterMoveEvent moveEvent in monsterMoveEvents) {
			Position newPos = moveEvent.newPos;
			if (Math.Pow (newPos.x - centerX, 2) / Math.Pow (a, 2) + Math.Pow (newPos.y - centerY, 2) / Math.Pow (b, 2) <= 1) {
				return true;
			}
		}

		return false;
	}
}

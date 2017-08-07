using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {


	private BoardManager boardManager;

	public Position currentPosition;
	public Position targetPosition;
	public Position nextStepPosition;

	public Vector3 mousePoint;

	public Animator animator;

	private List<Node> path = new List<Node> ();

	public Vector3 p;

	private float startTime;
	private float speed = 2.0F;
	private Tile nextStepTile;

	// Use this for initialization
	void Start () {

		//Get a component reference to the player's animator component
		animator = GetComponent<Animator>();
		//Get a component reference to the gameobject's gameManger component
		boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
		currentPosition = nextStepPosition = new Position (transform.position);
		// Substribe to notifications from the monster.
		NotificationSystem.subscribe (this);
		startTime = Time.time;
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

			// recalculate the path every step it goes until it reaches the target positin.
			if (targetPosition != currentPosition) {
				path.ForEach ((step) => step.tile.highlighted = false);
				path.Clear ();
				path.AddRange (boardManager.getGridWorld ().findPath (currentPosition, targetPosition));
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

	public void monsterMoved(MonsterMoveEvent moveEvent) {
	}
}

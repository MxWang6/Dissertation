using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {


	private BoardManager boardManager;

	public Position currentPosition;
	public Position targetPosition;

	public Vector3 mousePoint;

	public Animator animator;


	private List<Node> path = new List<Node> ();


	public Vector3 p;


	// Use this for initialization
	public void Start () {

		//Get a component reference to the player's animator component
		animator = GetComponent<Animator>();

		//Get a component reference to the gameobject's gameManger component
		boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();

		currentPosition = new Position (transform.position.x, transform.position.y);
		targetPosition = new Position (0,0);

		Debug.Log(currentPosition);
		Time.fixedDeltaTime = 0.5f;
		NotificationSystem.subscribe (this);
	}

	public void FixedUpdate() {
		if (targetPosition != currentPosition) {
			//start to find path
			path.ForEach ((step) => step.tile.highlighted = false);
			path.Clear ();
			path.AddRange (boardManager.getGridWorld ().findPath (currentPosition, targetPosition));
		}

		if (path.Count > 0) {
			Tile nextTile = path [0].tile;
			path.RemoveAt (0);
			currentPosition = nextTile.getPosition ();
			transform.position = currentPosition.toVector3 ();
			nextTile.highlighted = false;

			// animation of player
			if (targetPosition.x > currentPosition.x) {
				animator.SetTrigger ("PlayerRight");
			} else if (targetPosition.x < currentPosition.x) {
				animator.SetTrigger ("PlayerLeft");
			} else if (targetPosition.x == currentPosition.x && targetPosition.y < currentPosition.y) {
				animator.SetTrigger ("PlayerIdle");
			} else if (targetPosition.x == currentPosition.x && targetPosition.y > currentPosition.y) {
				animator.SetTrigger ("PlayerBack");
			}
		}
	}


	// Update is called once per frame
	void Update () {
		checkMouseEvent ();
	}

	public void checkMouseEvent(){
		
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

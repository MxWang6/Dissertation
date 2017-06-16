using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {


	private BoardManager boardManager;

	public Position currentPosition;
	public Position targetPosition;


	private List<Node> path = new List<Node> ();


	public Vector3 p;


	// Use this for initialization
	public void Start () {

		boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();

		currentPosition = new Position (transform.position.x, transform.position.y);

		Debug.Log(currentPosition);
		Time.fixedDeltaTime = 0.5f;
	}

	public void FixedUpdate() {
		if (path.Count > 0) {
			Tile nextTile = path [0].tile;
			path.RemoveAt (0);
			transform.position = nextTile.getPosition ().toVector3 ();
			nextTile.highlighted = false;
		}
	}


	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Pressed left click.");
//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit;
//			if (Physics.Raycast (ray, out hit))
//			{
//				pointsq = hit.point;
//
//				Debug.Log (pointsq);
//			}

//			float x = p.x;
//			int y = (int)p.y;
//			Debug.Log (y);

			Debug.Log (Input.mousePosition);
			targetPosition = new Position(Input.mousePosition.x/100,Input.mousePosition.y/100);
//			

			path.ForEach ((step) => step.tile.highlighted = false);
			path.Clear();
			path.AddRange(boardManager.getGridWorld().findPath(currentPosition, targetPosition));
			currentPosition = targetPosition;
		//	location = newLocation;


		}
	}
}

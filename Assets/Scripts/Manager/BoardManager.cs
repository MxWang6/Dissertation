using System.Collections;
using System.Collections.Generic; // using list
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count
	{
		public int minimum;             //Minimum value for our Count class.
		public int maximum;             //Maximum value for our Count class.


		//Assignment constructor.
		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}
		
	private const int columns = 27;                                         //Number of columns in our game board.
	private const int rows = 12;                                            //Number of rows in our game board.
	public Count wallCount = new Count (5, 9);                      //Lower and upper limit for our random number of walls per level.
	public Count foodCount = new Count (1, 5);                      //Lower and upper limit for our random number of food items per level.

	public GameObject goldMine;
	public GameObject shack;
	public GameObject bank;
	public GameObject saloon;
	public GameObject[] floorTiles;
	public GameObject[] outerWallTiles; 

	private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.

	private Tile[,] gridWorld = new Tile[columns, rows];

	//Sets up the outer walls and floor (background) of the game board.
	private void BoardSetup ()
	{
		
		boardHolder = new GameObject ("Board").transform;

		for(int x = -1; x < columns + 1; x++) {
			for(int y = -1; y < rows + 1; y++) {
				Position position = new Position (x, y);
				GameObject instance;
				if (x == -1 || x == columns || y == -1 || y == rows) {
					GameObject toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
					instance = Instantiate (toInstantiate, position.toVector3(), Quaternion.identity) as GameObject;
				} else {
					GameObject toInstantiate = floorTiles[Random.Range (0, floorTiles.Length)];
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					instance = Instantiate (toInstantiate, position.toVector3(), Quaternion.identity) as GameObject;
					Tile tileSprite = instance.GetComponent<Tile> ();
					tileSprite.setPosition (position);
					gridWorld [x, y] = tileSprite;
				}
				//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
				instance.transform.SetParent (boardHolder);
			}
		}
	}
		
	//RandomPosition returns a random position from our list gridPositions.
	private Vector3 RandomPosition ()
	{
		
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}


	//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
	void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
	{
		
		int objectCount = Random.Range (minimum, maximum+1);

		for(int i = 0; i < objectCount; i++)
		{
			Vector3 randomPosition = RandomPosition();
			GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
			Instantiate(tileChoice, randomPosition, Quaternion.identity);
		}
	}


	//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void SetupScene (int level)
	{
		BoardSetup();
//		InitialiseList();

	//	LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
	//	LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
		//int enemyCount = (int)Mathf.Log(level, 2f);
	//	LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
		Instantiate (bank, Locations.BANK.toVector3(), Quaternion.identity);
		Instantiate (shack, Locations.SHACK.toVector3(), Quaternion.identity);
		Instantiate (goldMine, Locations.GOLDMINE.toVector3(), Quaternion.identity);
		Instantiate (saloon, Locations.SALOON.toVector3(), Quaternion.identity);
	}

	public List<Node> findPath(Position currentPosition, Position targetPosition) {
		Tile currentTile = gridWorld [currentPosition.x, currentPosition.y];
		Tile targetTile = gridWorld [targetPosition.x, targetPosition.y];

		List<Node> open = new List<Node> ();
		List<Node> close = new List<Node> ();

		Node startNode = new Node (currentTile);
		Node targetNode = new Node (targetTile);
		open.Add (startNode);
		Node currentNode = startNode;
		while (open.Count > 0) {
			currentNode = open [0];
			open.ForEach( (node) => {
				if (node.GetFcost() < currentNode.GetFcost() || node.GetFcost() == currentNode.GetFcost() && node.hcost < currentNode.hcost) {
					currentNode = node;
				}
			});
			open.Remove (currentNode);
			close.Add (currentNode);

			if (currentNode.Equals(targetNode)) {
				return GetPath (startNode, currentNode);
			}

			findNearNodes(currentNode).ForEach((nearNode) => {
				if (!nearNode.tile.blocked && !close.Contains(nearNode)) {
					int newGcostToNearNode = currentNode.gcost + GetDistance(currentNode, nearNode);

					if (newGcostToNearNode < nearNode.gcost || !open.Contains(nearNode)) {
						
						if (!open.Contains(nearNode)) {
							open.Add(nearNode);
						} else {
							// we wanna retrieve the near node stored in the open list already to update.
							nearNode = open[open.IndexOf(nearNode)];
//							open.ForEach((node) => {
//								if (node.Equals(nearNode)) 
//							});
						}
						nearNode.gcost = newGcostToNearNode;
						nearNode.hcost = GetDistance(nearNode, targetNode);
						nearNode.parent = currentNode;
					}
				}
			});

		}

		// no path is found, e.g. no path can reach the distination.
		return GetPath (startNode, currentNode);
	}

	private List<Node> findNearNodes(Node currentNode) {
		Position currentPosition = currentNode.tile.getPosition ();
		List<Node> nearNodes = new List<Node> ();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0) {
					continue;
				}
				int nearPositionX = currentPosition.x + x;
				int nearPositionY = currentPosition.y + y;
				if (nearPositionX >= 0 && nearPositionX < columns && nearPositionY >= 0 && nearPositionY < rows) {
					nearNodes.Add (new Node (gridWorld [nearPositionX, nearPositionY]));
				}
			}
		}

		return nearNodes;
//
//		if (currentPosition.x - 1 >= 0) {
//			TileSprite tile = gridWorld [currentPosition.x - 1, currentPosition.y];
//			nearNodes.Add (new Node(tile));
//		}
//
//		if (currentPosition.x + 1 < columns) {
//			TileSprite tile = gridWorld [currentPosition.x + 1, currentPosition.y];
//			nearNodes.Add (new Node(tile));
//		}
//
//		if (currentPosition.y - 1 >= 0) {
//			TileSprite tile = gridWorld [currentPosition.x, currentPosition.y - 1];
//			nearNodes.Add (new Node(tile));
//		}
//
//		if (currentPosition.y + 1 < rows) {
//			TileSprite tile = gridWorld [currentPosition.x, currentPosition.y + 1];
//			nearNodes.Add (new Node(tile));
//		}
//
//		return nearNodes;
	}

	private int GetDistance(Node node1, Node node2) {
		int distanceX = Math.Abs (node1.tile.position.x - node2.tile.position.x);
		int distanceY = Math.Abs (node1.tile.position.y - node2.tile.position.y);

		if (distanceX > distanceY) {
			return 14 * distanceY + 10 * (distanceX - distanceY);
		}

		return 14 * distanceX + 10 * (distanceY - distanceX);
	}

	private List<Node> GetPath(Node startNode, Node endNode) {
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;
		while (!currentNode.Equals(startNode)) {
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse ();

		return path;
	}
}

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

	// add the location prefab here
	public GameObject goldMine;
	public GameObject shack;
	public GameObject bank;
	public GameObject saloon;
	public GameObject outlawCamp;
	public GameObject sheriffOffice;
	public GameObject cemetery;

	public GameObject[] floorTiles;
	public GameObject[] outerWallTiles; 

	private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.

	private GridWorld gridWorld = new GridWorld(columns, rows);

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
					gridWorld.addTile (x, y, tileSprite);
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
	private void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
	{
		
		int objectCount = Random.Range (minimum, maximum+1);

		for(int i = 0; i < objectCount; i++)
		{
			Vector3 randomPosition = RandomPosition();
			GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
			Instantiate(tileChoice, randomPosition, Quaternion.identity);
		}
	}

	private void LayoutObjectAtRandom (GameObject obj)
	{

		Vector3 randomPosition = RandomPosition();
		Instantiate(obj, randomPosition, Quaternion.identity);
	}

	//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void SetupScene (int level)
	{
		BoardSetup();


		Position position = new Position (Random.Range (0, columns), Random.Range (0, rows));
		Instantiate(bank, position.toVector3(), Quaternion.identity);
		Locations.BANK = position;
		// if the location is placed on a tile marked as blocked. we need to mark as unblocked.
		// otherwise our path finding can not reach it.
		gridWorld.getTile (position).blocked = false;

		position = new Position (Random.Range (0, columns), Random.Range (0, rows));
		Instantiate(shack, position.toVector3(), Quaternion.identity);
		Locations.SHACK = position;
		gridWorld.getTile (position).blocked = false;

		position = new Position (Random.Range (0, columns), Random.Range (0, rows));
		Instantiate(goldMine, position.toVector3(), Quaternion.identity);
		Locations.GOLDMINE = position;
		gridWorld.getTile (position).blocked = false;

		position = new Position (Random.Range (0, columns), Random.Range (0, rows));
		Instantiate(saloon, position.toVector3(), Quaternion.identity);
		Locations.SALOON = position;
		gridWorld.getTile (position).blocked = false;

		// add other location here - outlawcamp
		position = new Position (Random.Range (0, columns), Random.Range (0, rows));
		Instantiate(outlawCamp, position.toVector3(), Quaternion.identity);
		Locations.OUTLAWCAMP = position;
		gridWorld.getTile (position).blocked = false;

		// sheriff office
		position = new Position (Random.Range (0, columns), Random.Range (0, rows));
		Instantiate(sheriffOffice, position.toVector3(), Quaternion.identity);
		Locations.OFFICE = position;
		gridWorld.getTile (position).blocked = false;

		// CEMETERY
		position = new Position (Random.Range (0, columns), Random.Range (0, rows));
		Instantiate(cemetery, position.toVector3(), Quaternion.identity);
		Locations.CEMETERY = position;
		gridWorld.getTile (position).blocked = false;
	}
		
	public GridWorld getGridWorld() {
		return gridWorld;
	}
}

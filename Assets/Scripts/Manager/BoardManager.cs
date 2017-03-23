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
		
	public int columns = 27;                                         //Number of columns in our game board.
	public int rows = 8;                                            //Number of rows in our game board.
	public Count wallCount = new Count (5, 9);                      //Lower and upper limit for our random number of walls per level.
	public Count foodCount = new Count (1, 5);                      //Lower and upper limit for our random number of food items per level.

	public GameObject goldMine;
	public GameObject shack;
	public GameObject exit;                                         //Prefab to spawn for exit.
	public GameObject[] floorTiles;
	public GameObject[] outerWallTiles; 
//	public GameObject[] wallTiles;                                  //Array of wall prefabs.
//	public GameObject[] foodTiles;                                  //Array of food prefabs.
//	public GameObject[] enemyTiles;                                 //Array of enemy prefabs.

	private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.


	//Clears our list gridPositions and prepares it to generate a new board.
	private void InitialiseList ()
	{
		
		gridPositions.Clear ();
		for(int x = 1; x < columns-1; x++)
		{
			
			for(int y = 1; y < rows-1; y++)
			{
				gridPositions.Add (new Vector3(x, y, 0f));
			}
		}
	}


	//Sets up the outer walls and floor (background) of the game board.
	private void BoardSetup ()
	{
		
		boardHolder = new GameObject ("Board").transform;

		for(int x = -1; x < columns + 1; x++)
		{
			for(int y = -1; y < rows + 1; y++)
			{
				GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
				if (x == -1 || x == columns || y == -1 || y == rows) {
					toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
				}

				//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
				GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;

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
		InitialiseList();

	//	LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
	//	LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
		//int enemyCount = (int)Mathf.Log(level, 2f);
	//	LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
		Instantiate (exit, Locations.EXIT, Quaternion.identity);
		Instantiate (shack, Locations.SHACK, Quaternion.identity);
		Instantiate (goldMine, Locations.GOLDMINE, Quaternion.identity);
	}
}

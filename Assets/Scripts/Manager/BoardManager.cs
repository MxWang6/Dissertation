﻿using System.Collections;
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
		
	private const int columns = 80;                                         //Number of columns in our game board.
	private const int rows = 37;                                            //Number of rows in our game board.
//	public Count wallCount = new Count (5, 9);                      //Lower and upper limit for our random number of walls per level.
//	public Count foodCount = new Count (1, 5);                      //Lower and upper limit for our random number of food items per level.

	// add the location prefab here

	public GameObject[] floorTiles;
	public GameObject[] outerWallTiles; 

	// add the Monster prefab here
	public GameObject[] monsterTiles;
	public int monsterCount;

	private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.

	private GridWorld gridWorld = new GridWorld(columns, rows);

	public List <Vector3> monsterPositions = new List<Vector3> (); // A list of monster position



	void InitialiseList()
	{
		
		//Clear our list gridPositions.
		gridPositions.Clear ();

		//Loop through x axis (columns).
		for(int x = 1; x < columns-1; x++)
		{
			//Within each column, loop through y axis (rows).
			for(int y = 1; y < rows-1; y++)
			{
				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
				gridPositions.Add (new Vector3(x, y, 0f));
			}
		}

	}

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

		//limit the range of randomPosition
		while (randomPosition.x < 4 || randomPosition.x > (columns - 6) || randomPosition.y < 4 || randomPosition.y > (rows - 6)) {
			
			randomPosition = gridPositions[Random.Range (0, gridPositions.Count)];
		}

		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}


	//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
	private void LayoutMonsterAtRandom (GameObject[] tileArray, int minimum, int maximum)
	{
		
		int objectCount = Random.Range (minimum, maximum+1);

		for(int i = 0; i < objectCount; i++)
		{

			Vector3 randomPosition = RandomPosition();

			while (gridWorld.getTile (toPosition (randomPosition)).blocked) {

				 randomPosition = RandomPosition();
			}

			monsterPositions.Add (randomPosition);
			GameObject tileChoice = tileArray [Random.Range (0, tileArray.Length)];

			GameObject tileChoiceClone = Instantiate (tileChoice, randomPosition, Quaternion.identity);
			Monster monster = tileChoiceClone.GetComponent<Monster> ();
			monster.setPosition (gridWorld, toPosition (randomPosition));

			//app = monster.setMovingArea (MovingArea);
//			MovingArea movingArea;
//			movingArea.setGridWorld (gridWorld);
		}
	}
		

	//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void SetupScene (int level)
	{
		BoardSetup();

		InitialiseList ();

		LayoutMonsterAtRandom (monsterTiles,monsterCount,monsterCount);


	}
		
	public GridWorld getGridWorld() {
		return gridWorld;
	}

	public Position toPosition(Vector3 number){

		Position p = new Position (number.x, number.y);
		return p;
	}


}

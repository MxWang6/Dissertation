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
		
	private const int columns = 40;   //(80,37) (40,20)                                  //Number of columns in our game board.
	private const int rows = 20;                                            //Number of rows in our game board.
//	public Count wallCount = new Count (5, 9);                      //Lower and upper limit for our random number of walls per level.
//	public Count foodCount = new Count (1, 5);                      //Lower and upper limit for our random number of food items per level.

	// add the location prefab here

	public GameObject[] floorTiles;
	public GameObject[] outerWallTiles; 
	public GameObject[] obstacleTiles;
	public GameObject[] backgroundTiles;
	public GameObject[] itemTiles;

	// add the Monster prefab here
	public GameObject[] monsterTiles;
	public int monsterCount;

	private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.

	private GridWorld gridWorld = new GridWorld(columns, rows);

	public List <Vector3> monsterPositions = new List<Vector3> (); // A list of monster position

	public int t;

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
				} else if ( x == 30 && y == 15|| x == 31 && y == 15 || x == 30 && y == 14 || x == 31 && y == 14
					|| x == 11 && y == 12|| x == 12 && y == 12 || x == 11 && y == 11 || x == 12 && y == 11
					|| x == 15 && y == 9 || x == 16 && y == 9 || x == 15 && y == 8 || x == 16 && y == 8
					|| x == 14 && y == 8 || x == 16 && y == 10 
					|| x == 34 && y == 5 || x == 35 && y == 5 || x == 34 && y == 4 || x == 35 && y == 4
					|| x == 27 && y == 2 || x == 28 && y == 2 || x == 27 && y == 1 || x == 28 && y == 1){


					if (x == 30 && y == 15 || x == 11 && y == 12 || x == 27 && y == 2 ) {
						t = 0;
					} else if (x == 31 && y == 15 || x == 12 && y == 12 || x == 28 && y == 2) {
						t = 1;
					} else if (x == 30 && y == 14 || x == 11 && y == 11 || x == 27 && y == 1) {
						t = 2;
					} else if (x == 31 && y == 14 || x == 12 && y == 11 || x == 28 && y == 1) {
						t = 3;
					}else if ( x == 15 && y == 9){
						t = 8;
					}else if ( x == 16 && y == 9){
						t = 9;
					}else if ( x == 15 && y == 8){
						t = 10;
					}else if ( x == 16 && y == 8){
						t = 11;
					}else if ( x == 14 && y == 8){
						t = 12;
					}else if ( x == 16 && y == 10){
						t = 13;
					}else if ( x == 34 && y == 5){
						t = 4;
					}else if ( x == 35 && y == 5){
						t = 5;
					}else if ( x == 34 && y == 4){
						t = 6;
					}else if ( x == 35 && y == 4){
						t = 7;
					}
					
					GameObject toInstantiate = backgroundTiles[t];
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					instance = Instantiate (toInstantiate, position.toVector3(), Quaternion.identity) as GameObject;

					Tile tileSprite = instance.GetComponent<Tile> ();
					tileSprite.setPosition (position);
					gridWorld.addTile (x, y, tileSprite);

				}else if (assignObstacle(x,y)){

					// floor with obstacle
					GameObject toInstantiate = obstacleTiles[Random.Range (0, obstacleTiles.Length)];
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					instance = Instantiate (toInstantiate, position.toVector3(), Quaternion.identity) as GameObject;

					Tile tileSprite = instance.GetComponent<Tile> ();
					tileSprite.setPosition (position);
					gridWorld.addTile (x, y, tileSprite);

				}else if (assignItem(x,y)){

					int indexItem = 0;

					if (x == 3)
						indexItem = 0;
					else if (x == 20)
						indexItem = 1;
					else if (x == 26)
						indexItem = 4;
					else if (x == 33)
						indexItem = 3;
					else if (x == 6)
						indexItem = 4;
					
					//  floor with item
					GameObject toInstantiate = itemTiles[indexItem];
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					instance = Instantiate (toInstantiate, position.toVector3(), Quaternion.identity) as GameObject;

					Tile tileSprite = instance.GetComponent<Tile> ();
					tileSprite.setPosition (position);
					gridWorld.addTile (x, y, tileSprite);

				}
				else{

					// the rest of floor
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
		while (randomPosition.x < 5 || randomPosition.x > (columns - 6) || randomPosition.y < 5 || randomPosition.y > (rows - 6)) {
			
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


	public bool assignObstacle(int x, int y){

		if (x == 2 && y == 2 || x == 3 && y == 1 || x == 6 && y == 2 || x == 7 && y == 2 || x == 8 && y == 1 || x == 14 && y == 2 || x == 18 && y == 2|| x == 22 && y == 1 || x == 23 && y == 1 || x == 30 && y == 2 || x == 32 && y == 2 || x == 36 && y == 1 || x == 37 && y == 0
			|| x == 3 && y == 3 || x == 4 && y == 3 || x == 7 && y == 0 || x == 8 && y == 0 || x == 15 && y == 3 || x == 17 && y == 3|| x == 23 && y == 3 || x == 24 && y == 3 || x == 31 && y == 3 || x == 32 && y == 3 || x == 38 && y == 3 || x == 37 && y == 3
		    || x == 16 && y == 4 || x == 25 && y == 4 || x == 25 && y == 5 || x == 11 && y == 5 || x == 29 && y == 5 || x == 18 && y == 5||x == 10 && y == 6 || x == 11 && y == 6 || x == 20 && y == 6 || x == 21 && y == 6
			|| x == 0 && y == 7 || x == 10 && y == 7 || x == 12 && y == 7 || x == 16 && y == 7 || x == 17 && y == 7|| x == 21 && y == 7 || x == 25 && y == 7 || x == 26 && y == 7 || x == 27 && y == 7 || x == 33 && y == 7 || x == 34 && y == 7
			||x == 2 && y == 8 || x == 3 && y == 8 || x == 8 && y == 9 || x == 14 && y == 9 || x == 18 && y == 9|| x == 30 && y == 9 || x == 32 && y == 10 || x == 39 && y == 9 || x == 38 && y == 9
			||x == 1 && y == 10 || x == 3 && y == 10 || x == 6 && y == 10 || x == 7 && y == 10 || x == 8 && y == 10 || x == 15 && y == 10 || x == 16 && y == 11|| x == 20 && y == 11 || x == 21 && y == 11 || x == 30 && y == 12 || x == 32 && y == 11 || x == 37 && y == 11 || x == 37 && y == 12
			||x == 2 && y == 13 || x == 3 && y == 13 || x == 6 && y == 13 || x == 6 && y == 12 || x == 9 && y == 13 || x == 14 && y == 13 || x == 18 && y == 13|| x == 25 && y == 14 || x == 26 && y == 14 || x == 30 && y == 14 || x == 35 && y == 14
			||x== 2 && y == 14 || x == 4 && y == 15 || x == 4 && y == 16 || x == 11 && y == 15 || x == 13 && y == 15 || x == 17 && y == 16 || x == 19 && y == 16|| x == 20 && y == 16 || x == 23 && y == 17	
			||x == 2 && y == 18 || x == 3 && y == 18 || x == 10 && y == 18 || x == 14 && y == 18 || x == 5 && y == 19|| x == 10 && y == 19 || x == 15 && y == 19 || x == 36 && y == 19 || x == 36 && y == 18 || x == 23 && y == 18 || x == 24 && y == 18
		)
			return true;
		else
			return false;
	}

	public bool assignItem(int x, int y){

		if (x == 3 && y == 2 || x == 20 && y == 12 || x == 26 && y == 10 || x == 33 && y == 17 || x == 6 && y == 18) {
			return true;
		} else
			return false;
	}

}

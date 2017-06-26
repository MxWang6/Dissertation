using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	private BoardManager boardManager;
	public Position monsterPosition;

	public float attackPower;
	public float [][] attackArea;
	public int movingAreaX;
	public int movingAreaY;

	public float attackProbability = 0;
	public float monsterCost;

	public int movingX;
	public int movingY;

	public int ajustedFactor;

//	public Tile monsterTile;

	public GridWorld gridWorld;


	// Use this for initialization
	void Start () {

		boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
		Vector3 posi = transform.position;
		Debug.Log ("monster"+ posi);
		Debug.Log (boardManager.monsterPositions[0]); 
	}

//	public void FixedUpdate(){
//
//		Position p = new Position (movingX,movingY);
//		//float step = 0.5f * Time.deltaTime;
//		transform.position = p.toVector3();
//		//		transform.position = Vector3.MoveTowards(transform.position, p.toVector3(), step);
//
//	}

	// Update is called once per frame
	void Update () {

//		if (boardManager.app != null){
			Position p = boardManager.app;
		    float step = 0.5f * Time.deltaTime;
		    transform.position = Vector3.MoveTowards(transform.position, p.toVector3(), step);
     		setMovingArea(this.toPosition(transform.position));
//		}

	}

	public void setGridWorld(GridWorld gridWorld) {
		this.gridWorld = gridWorld;
	}

	public void setPosition(Position position) {
		this.monsterPosition = position;

		float a = 1 / 9.0f;

		// update its surrounding tiles in the grid world with monsterAtttackCost
		Position mPosition = new Position (monsterPosition.x, monsterPosition.y);
		gridWorld.getTile(mPosition).monsterCost = a * attackPower * ajustedFactor;

		mPosition = new Position (monsterPosition.x-1, monsterPosition.y-1);
		gridWorld.getTile(mPosition).monsterCost = a  * attackPower * ajustedFactor;

		mPosition = new Position (monsterPosition.x, monsterPosition.y-1);
		gridWorld.getTile(mPosition).monsterCost = a * attackPower * ajustedFactor;

		mPosition = new Position (monsterPosition.x+1, monsterPosition.y-1);
		gridWorld.getTile(mPosition).monsterCost = a * attackPower * ajustedFactor;

		mPosition = new Position (monsterPosition.x-1, monsterPosition.y);
		gridWorld.getTile(mPosition).monsterCost = a * attackPower * ajustedFactor;

		mPosition = new Position (monsterPosition.x+1, monsterPosition.y);
		gridWorld.getTile(mPosition).monsterCost = a * attackPower * ajustedFactor;

		mPosition = new Position (monsterPosition.x-1, monsterPosition.y+1);
		gridWorld.getTile(mPosition).monsterCost = a * attackPower * ajustedFactor;

		mPosition = new Position (monsterPosition.x, monsterPosition.y+1);
		gridWorld.getTile(mPosition).monsterCost = a * attackPower * ajustedFactor;

		mPosition = new Position (monsterPosition.x+1, monsterPosition.y+1);
		gridWorld.getTile(mPosition).monsterCost = a * attackPower * ajustedFactor;

	}

	public Position getPosition() {
		return monsterPosition;
	}

	public float getAttackPower(){

		return attackPower;
	}

	public Position setMovingArea(Position position){


		// left up point 
		int mapAreaLeftUpX = position.x - movingAreaX + 2;
		int mapAreaLeftUpY = position.y - movingAreaY + 2;

		// right up point
		int mapAreaRightUpX = position.x + movingAreaX - 2;
		int mapAreaRightUpY = position.y - movingAreaY + 2;

		// left down point
		int mapAreaLeftDownX = position.x - movingAreaX + 2;
		int mapAreaLeftDownY = position.y + movingAreaY - 2;

		// right down point 
		int mapAreaRightDownX = position.x + movingAreaX - 2;
		int mapAreaRightDownY = position.y + movingAreaY - 2;

//		int movingX;
//		int movingY;

		movingX = Random.Range (mapAreaLeftUpX, mapAreaRightUpX);
		movingY = Random.Range (mapAreaLeftUpY, mapAreaLeftDownY);

		 //Vector3.MoveTowards(transform.position, p.toVector3(), step);
//
		Position p = new Position (movingX,movingY);
//
		setHighted(mapAreaLeftUpX,mapAreaRightUpX,mapAreaLeftUpY,mapAreaLeftDownY);
		gridWorld.getTile(p).highlighted = true;
//		float step = 0.5f * Time.deltaTime;
	//	transform.position = p.toVector3();
//		transform.position = Vector3.MoveTowards(transform.position, p.toVector3(), step);

		return p;
	}
		

	public Position toPosition(Vector3 number){

		Position p = new Position (number.x, number.y);
		return p;
	}

	public void setHighted(int x1, int x2,int y1,int y2)
	{
		for (int x = x1; x < x2+1; x++) {
			for (int y = y1; y<y2+1; y++){

				Position tileM = new Position (x, y);
				gridWorld.getTile (tileM).highlightedM = true;
			}

		}
	}
}

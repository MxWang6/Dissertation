using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	private BoardManager boardManager;
	public Position monsterPosition;

	public float attackPower;
	public float [][] attackArea;

	public float attackProbability = 0;
	public float monsterCost;

	public int ajustedFactor;

	public GridWorld gridWorld;

	public int movingAreaWidth;
	public int movingAreaHeight;

	private MovingArea movingArea;

	// Use this for initialization
	void Start () {

		boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
		Vector3 posi = transform.position;
		Debug.Log ("monster"+ posi);
		Debug.Log (boardManager.monsterPositions[0]); 
		Time.fixedDeltaTime = 0.5f;
	}

	public void FixedUpdate(){

		toggleCostOfTile (toPosition2 (transform.position), false);
		Vector2 mA = movingArea.getNextRandomPosition(transform.position);
		transform.position = mA;
		toggleCostOfTile (toPosition2 (mA), true);
		//setPosition (toPosition2(mA));
        //transform.position = movingArea.getNextRandomPosition(transform.position);
	}

	public void setPosition(GridWorld gridWorld, Position position) {
		this.gridWorld = gridWorld;
		this.monsterPosition = position;
		this.movingArea = new MovingArea(gridWorld, position , movingAreaWidth, movingAreaHeight);

		// initial its surrounding tiles in the grid world with monsterAtttackCost
		this.toggleCostOfTile (monsterPosition, true);
	}

	// update its surrounding tiles in the grid world with monsterAtttackCost
	public void toggleCostOfTile(Position position, bool turnedOn){

		float a = 1 / 9.0f;

		Position mPosition = new Position (position.x, position.y);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x-1, position.y-1);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x, position.y-1);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x+1, position.y-1);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x-1, position.y);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x+1, position.y);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x-1, position.y+1);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x, position.y+1);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x+1, position.y+1);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

	}

	public Position getPosition() {
		return monsterPosition;
	}

	public float getAttackPower(){

		return attackPower;
	}

	public Position toPosition(Vector3 number){

		Position p = new Position (number.x, number.y);
		return p;
	}

	public Position toPosition2(Vector2 number){

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

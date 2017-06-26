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
		transform.position = movingArea.getNextRandomPosition(transform.position);
	}

	public void setGridWorld(GridWorld gridWorld) {
		this.gridWorld = gridWorld;
	}

	public void setPosition(Position position) {
		this.monsterPosition = position;
		this.movingArea = new MovingArea(position , movingAreaWidth, movingAreaHeight);
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

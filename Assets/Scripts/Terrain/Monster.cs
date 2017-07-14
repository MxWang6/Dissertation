using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	private BoardManager boardManager;
	public Position monsterPosition;

	public float attackPower;

	public float attackProbability = 0;
	public int attackProbabilityArea;
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
		if (transform.position.x != mA.x || transform.position.y != mA.y) {
			//notify the player through notification system.
			NotificationSystem.publish (new MonsterMoveEvent (this, toPosition2 (transform.position), toPosition2 (mA)));
		}
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

		//float attackArea = attackProbabilityArea * 2 + 1;

		float a = 1 / (attackProbabilityArea * 2.0f + 1.0f);

		for (int i = 1; i< attackProbabilityArea + 1; i++)
		{

			setProbabilityArea (position, turnedOn, i, a);

		}
			
	}

	public void setProbabilityArea(Position position, bool turnedOn, int circle, float a)
	{
		
		Position mPosition = new Position (position.x, position.y);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		// the first row
		mPosition = new Position (position.x - circle, position.y - circle);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x, position.y - circle);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x + circle, position.y - circle);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		// the second row
		mPosition = new Position (position.x - circle, position.y);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x + circle, position.y);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		// the third row
		mPosition = new Position (position.x - circle, position.y + circle);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x, position.y + circle);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		mPosition = new Position (position.x + circle, position.y + circle);
		gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
		gridWorld.getTile (mPosition).highlightedM = turnedOn;

		if (circle == 2) {
			
			mPosition = new Position (position.x - circle + 1, position.y - circle);
			gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
			gridWorld.getTile (mPosition).highlightedM = turnedOn;

			mPosition = new Position (position.x + circle -1, position.y - circle);
			gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
			gridWorld.getTile (mPosition).highlightedM = turnedOn;

			mPosition = new Position (position.x - circle, position.y - circle + 1);
			gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
			gridWorld.getTile (mPosition).highlightedM = turnedOn;

			mPosition = new Position (position.x + circle, position.y - circle + 1);
			gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
			gridWorld.getTile (mPosition).highlightedM = turnedOn;

			mPosition = new Position (position.x - circle, position.y + circle - 1);
			gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
			gridWorld.getTile (mPosition).highlightedM = turnedOn;

			mPosition = new Position (position.x + circle, position.y + circle - 1);
			gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
			gridWorld.getTile (mPosition).highlightedM = turnedOn;

			mPosition = new Position (position.x - circle + 1, position.y + circle);
			gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
			gridWorld.getTile (mPosition).highlightedM = turnedOn;

			mPosition = new Position (position.x + circle -1, position.y + circle);
			gridWorld.getTile(mPosition).monsterCost = turnedOn ? a * attackPower * ajustedFactor : 0;
			gridWorld.getTile (mPosition).highlightedM = turnedOn;

		}
			
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

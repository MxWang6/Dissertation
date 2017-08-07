using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

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

	private float startTime;
	private float speed = 1.0F;

	private Vector2 currentPosition;
	private Vector2 targetPosition;

	// Use this for initialization
	void Start () {
		currentPosition = targetPosition = transform.position;
		startTime = Time.time;

		Debug.Log ("Monster start position"+ currentPosition);
	}

	public void Update() {
		// when the monster moves to the target position, 
		// we need to start to move to the next position again and again.
		if (transform.position == new Vector3(targetPosition.x, targetPosition.y, 0)) {
			// Remove the cost of tile on the start position
			toggleCostOfTile (toPosition (currentPosition), false);
			// Add the cost of tile on the target position
			toggleCostOfTile (toPosition (targetPosition), true);
			// Notify the player through notification system that the monster now in a new position.
			NotificationSystem.publish (new MonsterMoveEvent (this, toPosition (currentPosition), toPosition (targetPosition)));

			// Now the arrived target position becomes the start position for the next move.
			currentPosition = targetPosition;
			// Retrive the target position for the next move.
			targetPosition = movingArea.getNextRandomPosition(transform.position);
		}

		float distance = Vector2.Distance (transform.position, targetPosition);
		float duration = Time.time - startTime;
		float distCovered = duration * speed;
		transform.position = Vector2.Lerp (transform.position, targetPosition, distCovered / distance);
		startTime = startTime + duration;
	}
		
	public void setPosition(GridWorld gridWorld, Position position) {
		this.gridWorld = gridWorld;
		this.monsterPosition = position;
		this.movingArea = new MovingArea(gridWorld, position , movingAreaWidth, movingAreaHeight);
	
		// initial its surrounding tiles in the grid world with monsterAtttackCost
		this.toggleCostOfTile (monsterPosition, true);
	}

	// update its surrounding tiles in the grid world with monsterAtttackCost
	public void toggleCostOfTile(Position position, bool turnedOn) {

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

	public Position toPosition(Vector2 vectorPosition){

		Position p = new Position (vectorPosition.x, vectorPosition.y);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	private BoardManager boardManager;
	public Position monsterPosition;

	public float attackPower;
	public float[,] attackArea;
	public float attackProbability = 0;
	public float monsterCost;


	public int ajustedFactor;

	public Tile monsterTile;

	public GridWorld gridWorld;


	// Use this for initialization
	void Start () {

		boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();

		Vector3 posi = transform.position;
		Debug.Log ("monster"+ posi);
		Debug.Log (boardManager.monsterPositions[0]); 
	}


	// Update is called once per frame
	void Update () {

		int x = Random.Range (-1, 1);
		int y = Random.Range (-1, 1);
		Position p = new Position (transform.position.x+x, transform.position.y + y);
		float step = 0.5f * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, p.toVector3(), step);
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
		
}

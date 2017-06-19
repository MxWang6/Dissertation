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



	}

	public void setGridWorld(GridWorld gridWorld) {
		this.gridWorld = gridWorld;
	}

	public void setPosition(Position position) {
		this.monsterPosition = position;
	}

	public Position getPosition() {
		return monsterPosition;
	}

	public float getAttackPower(){

		return attackPower;
	}


	public void assignProbability(List<Vector3> monsterPositions){

		int length = monsterPositions.Count;
		for (int i = 0; i < length; i++) {

			createProbability (1, monsterPositions [i]);

		}

	}

	public void createProbability(int attackArrange, Vector3 mP){

		if (attackArrange == 1) {

			Position mPosition = new Position (mP.x, mP.y);
			boardManager.getGridWorld().getTile(mPosition).attackedProbability = 0.9f * attackPower * ajustedFactor;
		
			mPosition = new Position (mP.x-1, mP.y-1);
			boardManager.getGridWorld().getTile(mPosition).attackedProbability = 0.9f * attackPower * ajustedFactor;

			mPosition = new Position (mP.x, mP.y-1);
			boardManager.getGridWorld().getTile(mPosition).attackedProbability = 0.9f * attackPower * ajustedFactor;

			mPosition = new Position (mP.x+1, mP.y-1);
			boardManager.getGridWorld().getTile(mPosition).attackedProbability = 0.9f * attackPower * ajustedFactor;

			mPosition = new Position (mP.x-1, mP.y);
			boardManager.getGridWorld().getTile(mPosition).attackedProbability = 0.9f * attackPower * ajustedFactor;

			mPosition = new Position (mP.x+1, mP.y);
			boardManager.getGridWorld().getTile(mPosition).attackedProbability = 0.9f * attackPower * ajustedFactor;

			mPosition = new Position (mP.x-1, mP.y+1);
			boardManager.getGridWorld().getTile(mPosition).attackedProbability = 0.9f * attackPower * ajustedFactor;

			mPosition = new Position (mP.x, mP.y+1);
			boardManager.getGridWorld().getTile(mPosition).attackedProbability = 0.9f * attackPower * ajustedFactor;

			mPosition = new Position (mP.x+1, mP.y+1);
			boardManager.getGridWorld().getTile(mPosition).attackedProbability = 0.9f * attackPower * ajustedFactor;

		}
	}

	public float CalculateCost(){

		this.monsterCost = attackPower * ajustedFactor;
		return monsterCost;
	}
}

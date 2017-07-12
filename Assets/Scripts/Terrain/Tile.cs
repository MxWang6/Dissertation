using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public bool blocked;
	public Position position;
	public bool highlighted;
	public bool highlightedM;
	public bool highlightedA;
	public bool isMonsterArea;

	public float attackedProbability;
	public float monsterCost;

	public float monsterAttackCost;

	private SpriteRenderer spriteRenderer;

	public void Start() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void Update() {
		if (highlighted) {
			spriteRenderer.color = new Color (27, 0, 198);
		} else if (highlightedM)
			spriteRenderer.color = new Color (0, 150, 130);
		else if (highlightedA)
			spriteRenderer.color = new Color (255, 0, 0);
		else{
			spriteRenderer.color = new Color (255, 255, 255);
		}
	}

	public void setPosition(Position position) {
		this.position = position;
	}

	public Position getPosition() {
		return position;
	}

	public float CalculateMonsterCost(){

		return monsterCost;
	}


}


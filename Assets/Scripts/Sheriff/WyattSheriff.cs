using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyattSheriff : Agent {

	public enum Location {
		Office,
		Bank,
		Saloon,
		Goldmine,
		Shack
	};

	private BoardManager boardManager;
	private StateMachine<WyattSheriff> stateMachine;
	private Position currentPosition;
	private Position targetPosition;
	private Location location;
	private int GoldCarried = 0;
	private int GoldInBank = 0;

	private List<Node> path = new List<Node>();

	public void Awake() {
		boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
		stateMachine = new StateMachine<WyattSheriff>();
		stateMachine.Init(this, RandomCheckState.Instance);

	}

	public void Start() {
		currentPosition = Locations.OFFICE;
		transform.position = currentPosition.toVector3 ();
		Time.fixedDeltaTime = 0.4f;
	} 

	public override void Update(){

		stateMachine.Update();
	}

	public void ChangeState(State<WyattSheriff> state){
		stateMachine.ChangeState(state);

	}

	public void RevertToPreviousState(){
		stateMachine.RevertToPreviousState();
	}

	public void ChangeLocation(Location newLocation) {
		if (newLocation == Location.Bank) {
			targetPosition = Locations.BANK;
		} else if (newLocation == Location.Goldmine) {
			targetPosition = Locations.GOLDMINE;
		} else if (newLocation == Location.Saloon) {
			targetPosition = Locations.SALOON;
		} else if (newLocation == Location.Shack) {
			targetPosition = Locations.SHACK;
		}
		else {
			// not happen.
		}

		path.Clear();
		path.AddRange(boardManager.getGridWorld().findPath(currentPosition, targetPosition));
		currentPosition = targetPosition;
		location = newLocation;
	}

	public Position GetPosition() {
		return currentPosition;
	}

	public Location GetLocation() {
		return location;
	}

	public void DepositGoldToBank() {
		GoldInBank += GoldCarried;
		GoldCarried = 0;
	}

	public int getGoldInBank() {
		return GoldInBank;
	}

	public bool isOutlawHere(){


	}
}

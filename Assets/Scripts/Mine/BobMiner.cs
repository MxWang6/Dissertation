using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobMiner : Agent {

	public enum Location {
		Goldmine,
		Saloon,
		Bank,
		Shack
	};
	protected BoardManager boardScript;

	private StateMachine<BobMiner> stateMachine;
	private Location location = Location.Goldmine;
	private int GoldCarried = 0;
	private int GoldInBank = 0;
	private int DailyDepositedGoldInBank = 0;
	// higher value means more thirsty.
	private int Thirst = 0;
	// higher value means more fatigue.
	private int Fatigue = 0;

	public void Awake() {
		
		boardScript = GameObject.Find("GameManager").GetComponent<BoardManager>();
		stateMachine = new StateMachine<BobMiner>();
		stateMachine.Init(this, GoHomeAndSleepTillRested.Instance);
	}

	public override void Update() {
		Thirst++;
		stateMachine.Update();
	}

	public void ChangeState(State<BobMiner> e) {
		stateMachine.ChangeState(e);
	}

	public void ChangeLocation(Location newLocation) {
		if (newLocation == Location.Bank) {
			this.transform.position = Locations.EXIT - new Vector3 (0, 1, 0);

		} else if (newLocation == Location.Goldmine) {
			this.transform.position = Locations.GOLDMINE - new Vector3 (0, 1, 0);

		} else if (newLocation == Location.Saloon) {
			this.transform.position = new Vector3 (0, 0, 0);

		} else if (newLocation == Location.Shack) {
			this.transform.position = Locations.SHACK - new Vector3 (0, 1, 0);

		} else {
			// not happen.
		}

		location = newLocation;
	}

	public void DigNugget() {
		GoldCarried ++;
		Fatigue ++;
	}

	public void SleepAndRest() {
		Fatigue -= 5;
		if (Fatigue < 0) {
			Fatigue = 0;
		}
	}

	public void DepositGoldToBank() {
		GoldInBank += GoldCarried;
		DailyDepositedGoldInBank += GoldCarried;

		GoldCarried = 0;
	}

	public void quenchThirst() {
		Thirst -= 5;
		// make sure thirst can be less than 0.
		if (Thirst < 0) {
			Thirst = 0;
		}
	}

	public void GoBackHome() {
		DailyDepositedGoldInBank = 0;
	}

	public bool IsPocketFull() {
		return GoldCarried == 5;
	}

	public bool IsThirsty() {
		return Thirst >= 10;
	}

	public bool IsThirstQuenched() {
		return Thirst == 0;
	}

	public bool IsFatigue() {
		return Fatigue >= 50;
	}

	public bool IsFullyRested() {
		return Fatigue == 0;
	}

	public bool IsGoldDeposited() {
		return GoldCarried == 0;
	}

	public bool IsWealthyEnough() {
		return DailyDepositedGoldInBank >= 20;
	}

	public Location GetLocation() {
		return location;
	}

	public int getCarriedGold() {
		return GoldCarried;
	}

	public int getGoldInBank() {
		return GoldInBank;
	}
}

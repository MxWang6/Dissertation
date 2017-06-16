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
	private BoardManager boardManager;


	private StateMachine<BobMiner> stateMachine;
	private Position currentPosition;
	private Position targetPosition;
	private Location location;
	private int GoldCarried = 0;
	private int GoldInBank = 0;
	private int DailyDepositedGoldInBank = 0;
	private int RobGold = 0;
	// higher value means more thirsty.
	private int Thirst = 0;
	// higher value means more fatigue.
	private int Fatigue = 0;
	// 10 means stew is full.
//	private int Stew = 0;

	private List<Node> path = new List<Node>();

//	public delegate void BobBackHome();
//	public static event BobBackHome OnBobBackHome;
//
//	public delegate void JesseRobBank();
//	public static event JesseRobBank OnJesseRobBank;

	public void Awake() {
		boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
		stateMachine = new StateMachine<BobMiner>();
		stateMachine.Init(this, GoHomeAndSleepTillRested.Instance);
	}

	public void Start() {
		currentPosition = Locations.SHACK;
		transform.position = currentPosition.toVector3 ();
		Time.fixedDeltaTime = 0.5f;

	}

	public void FixedUpdate() {
		if (path.Count > 0) {
			Tile nextTile = path [0].tile;
			path.RemoveAt (0);
			transform.position = nextTile.getPosition ().toVector3 ();
			nextTile.highlighted = false;
		}
	}
	public override void Update() {
		if (path.Count == 0) {
			Thirst++;
			stateMachine.Update ();
		}

	}

	public void ChangeState(State<BobMiner> e) {
		stateMachine.ChangeState(e);
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
//			//trigger the event 
//			if (OnBobBackHome != null) {
//					OnBobBackHome ();
//			}
		} else {
			// not happen.
		}

		path.ForEach ((step) => step.tile.highlighted = false);
		path.Clear();
		path.AddRange(boardManager.getGridWorld().findPath(currentPosition, targetPosition));
		currentPosition = targetPosition;
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

	public Position GetPosition() {
		return currentPosition;
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

	public int decreseGoldInBank(){

		RobGold	= Random.Range (1, 4);
		if (GoldInBank != 0) {
			GoldInBank -= RobGold;
			Debug.Log ("Bob's MONEY in bank is " + GoldInBank);
		} else {
			RobGold = 0;
		}
		return RobGold;
	}

//	public void EatStew() {
//		Stew -= 3;
//		if (Stew < 0) {
//			Stew = 0;
//
//		}
//	}
//
//	public bool StewFullyEaten() {
//		return Stew == 0;
//	}

	//  Bob receive message
//	public void OnEnable()
//	{
//		ElsaWife.OnCookIsReady += CookingIsReady;
//		JesseOutlaw.OnRobBank += InRobBank;
//	}
//
//	public void OnDisable()
//	{
//		ElsaWife.OnCookIsReady -= CookingIsReady;
//		JesseOutlaw.OnRobBank -= InRobBank;
//	}	

//	public void CookingIsReady(){
//		Debug.Log ("Elsa tells Bob: Dinner is fully cooked. Ready to eat...");
//		Stew = 10;
//		this.ChangeState (EatStewState.Instance);
//	}

//	public void InRobBank(){
//		Debug.Log ("Jesse send message: Going to rob bank......");
//		if (OnJesseRobBank != null) {
//			OnJesseRobBank ();
//		}
//	}
	// sensing
//	public override bool DetectsModality (Signal signal)
//	{
//		// only have sight.
//		// only worry about outlaw.
//		return signal.modality is SightModality && signal.sender is JesseOutlaw;
//	}
//
//	public override void Notify (Signal signal)
//	{
//		if (!targetPosition.Equals(Locations.BANK)) {
//			Debug.Log ("Bob: I just saw Outlaw. I am going to bank to protect my gold.");
//			ChangeLocation (Location.Bank);
//		}
//	}

//	public override Vector3 Position ()
//	{
//		return transform.position;
//	}
}

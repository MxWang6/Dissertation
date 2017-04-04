using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesseOutlaw : Agent {

	public enum Location {
		OutlawCamp,
		Cemetery,
		Bank
	};

	private BobMiner Bob;
	private BoardManager boardManager;
	private StateMachine<JesseOutlaw> stateMachine;
	private Position currentPosition;
	private Position targetPosition;
	private Location location;
	private int FindOpportunity = 0;
	private int Value = 0;
	private int TimeToRob = 0;
	private int GoldCarried = 0;

	private int waitedTime = 0;
	private int createdTime = 0;

	private List<Node> path = new List<Node>();

	//event
	public delegate void GoToRobBank();
	public static event GoToRobBank OnRobBank;

	public void Awake() {

		Bob = GameObject.Find("Miner").GetComponent<BobMiner>();
		boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
		stateMachine = new StateMachine<JesseOutlaw>();
		stateMachine.Init(this, LurkInOutlawCampState.Instance);
	}

	public void Start() {
		currentPosition = Locations.OUTLAWCAMP;
		transform.position = currentPosition.toVector3 ();
		Time.fixedDeltaTime = 0.5f;
	} 

	// add here...........
	public void FixedUpdate() {
		if (path.Count > 0) {
			Tile nextTile = path [0].tile;
			path.RemoveAt (0);
			transform.position = nextTile.getPosition ().toVector3 ();
			nextTile.highlighted = false;
		}
	}

	public override void Update(){

		if (path.Count == 0) {
			TimeToRob++;
			stateMachine.Update ();
		}
	}

	public void ChangeState(State<JesseOutlaw> state){
		stateMachine.ChangeState(state);

	}

	public void RevertToPreviousState(){
		stateMachine.RevertToPreviousState();
	}

	public void RandomValue(){

		if (Value == 0) {
			Value = Random.Range(2,7);
		}
			
	}

	public void LukIn(){

		FindOpportunity += 1;
	}

	public bool EndLukIn(){

		if (FindOpportunity > Value)
			return true;
		else
			return false;
	}
		
	public void InitialValue(){
		Value = 0;
		FindOpportunity = 0;
	}

	// send the Event message that tell bob 
	public bool RobBank(){

		if (TimeToRob > 12) {
			if (OnRobBank != null) {
				OnRobBank ();
			}
			return true;
		} else {

			return false;
		}
	}

	public void finishRob(){

		TimeToRob = 0;
	}

	public int RobGoldInBank(){

		GoldCarried = Bob.decreseGoldInBank ();
		//GoldCarried = Random.Range (5, 10);
		return GoldCarried;
	}

	public int getGoldCarried(){
		return GoldCarried;
	}

	// change location here
	public void ChangeLocation(Location newLocation) {
		if (newLocation == Location.OutlawCamp) {
			targetPosition = Locations.OUTLAWCAMP;
		} else if (newLocation == Location.Cemetery) {
			targetPosition = Locations.CEMETERY;
		} else if (newLocation == Location.Bank) {
			targetPosition = Locations.BANK;
		} else {
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

	public void rebornJesse(){

		this.transform.position = Locations.OUTLAWCAMP.toVector3();
		stateMachine.Init (this, LurkInOutlawCampState.Instance);
	}

	public void CreateTime () {
		createdTime++;
	}

	public bool EnoughTimeToWait(){

		return createdTime > 5;
	}
}

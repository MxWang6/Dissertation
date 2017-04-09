using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undertaker : Agent {

	public enum Location {
		Undertaker,
		Cemetery
	};

	private BoardManager boardManager;
	private RegionalSenseManager senseManager;
	private StateMachine<Undertaker> stateMachine;
	private Position currentPosition;
	private Position targetPosition;
	private Location location;
	private int randomValue = 0;

	private List<Node> path = new List<Node>();

	public void Awake() {
		boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
		stateMachine = new StateMachine<Undertaker>();
		stateMachine.Init(this, HoverInOfficeState.Instance);

		senseManager = GameObject.Find ("GameManager").GetComponent<RegionalSenseManager> ();
	}

	public void Start() {
		currentPosition = Locations.UNDERTAKER;
		transform.position = currentPosition.toVector3 ();
		Time.fixedDeltaTime = 0.5f;

		senseManager.Register (this);
	} 

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
			stateMachine.Update ();
		}
		randomValue++;

	}

	public int getRandomValue(){
		
		return randomValue;
	}

	public void  leavingOffice(){

		randomValue = 0;
	}

	public Position GetPosition() {
		return currentPosition;
	}

	public Location GetLocation() {
		return location;
	}

	public void ChangeLocation(Location newLocation) {
		if (newLocation == Location.Undertaker) {
			targetPosition = Locations.UNDERTAKER;
		}else if (newLocation == Location.Cemetery) {
			targetPosition = Locations.CEMETERY;
		} 
		else {
			// not happen.
		}

		path.Clear();
		path.AddRange(boardManager.getGridWorld().findPath(currentPosition, targetPosition));
		currentPosition = targetPosition;
		location = newLocation;
	}

	public void ChangeState(State<Undertaker> state){
		stateMachine.ChangeState(state);

	}

	public void RevertToPreviousState(){
		stateMachine.RevertToPreviousState();
	}

	// receive message

	public void OnEnable()
	{
		WyattSheriff.OnKillJesse += JessIsDied;
	}


	public void OnDisable()
	{
		WyattSheriff.OnKillJesse -= JessIsDied;
	}

	void JessIsDied(){

		Debug.Log ("Wyatt tells Undertaker: Jess Died...");
		this.ChangeState (LookForBodyState.Instance);
	}

	// sensing
	public override bool DetectsModality (Signal signal)
	{
		// not enabled
		return false;
	}

	public override void Notify (Signal signal)
	{
		// do nothing.
	}

	public override Vector3 Position ()
	{
		return transform.position;
	}
}

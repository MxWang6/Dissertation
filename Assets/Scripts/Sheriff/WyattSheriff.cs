using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyattSheriff : Agent {

	public enum Location {
		Office,
		Bank,
		Saloon,
		Goldmine,
		Shack,
		Cemetery
	};

	private JesseOutlaw Jesse;
	private ElsaWife Elsa;
	private BoardManager boardManager;
	private RegionalSenseManager senseManager;
	private StateMachine<WyattSheriff> stateMachine;
	private Position currentPosition;
	private Position targetPosition;
	private Location location;
	private int GoldCarried = 0;
	private int GoldInBank = 0;
	private int talkTime = 0;

	private List<Node> path = new List<Node>();

	// add message

	public delegate void KillJesse();
	public static event KillJesse OnKillJesse;

	public void Awake() {
		Jesse = GameObject.Find ("Outlaw").GetComponent<JesseOutlaw> ();
//		Elsa = GameObject.Find ("Wife").GetComponent<ElsaWife> ();
		boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
		stateMachine = new StateMachine<WyattSheriff>();
		stateMachine.Init(this, RandomCheckState.Instance);

		senseManager = GameObject.Find ("GameManager").GetComponent<RegionalSenseManager> ();
	}

	public void Start() {
		currentPosition = Locations.OFFICE;
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

		Signal signal = new Signal ();
		signal.sender = this;
		signal.strength = 6;
		senseManager.AddSignal (signal);
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
		}else if (newLocation == Location.Office) {
			targetPosition = Locations.OFFICE;
		}else if (newLocation == Location.Cemetery) {
			targetPosition = Locations.CEMETERY;
		}
		else {
			// not happen.
		}

		path.ForEach ((step) => step.tile.highlighted = false);
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
		return neiborhoodCompare();
	}

	public bool isOutlawDead(){
		Jesse.ChangeState(WaitRebornState.Instance);

		return true;
	}

	public void rebornOutlaw(){

		Jesse.rebornJesse();
	}

	public void getGoldFromOutlaw(){

		GoldCarried = Jesse.getGoldCarried ();
	}

	public void callMessage(){

		if (OnKillJesse != null) {
			OnKillJesse();
		}

	}

	public void talkTimeTo(){
		
		talkTime++;
	
	}

	public bool neiborhoodCompare(){

		float X = this.transform.position.x;
		float Y = this.transform.position.y;
		Vector3 JessPosition = Jesse.transform.position;

		if ( (JessPosition == new Vector3(X,Y,0f) )|| (JessPosition == new Vector3(X-1,Y,0f)) ||(JessPosition == new Vector3(X,Y+1,0f))||(JessPosition == new Vector3(X+1,Y,0f)) || (JessPosition == new Vector3(X,Y-11,0f))){

			return true;
		}

		return false;

	}

	public void callSensorEvent(){

	//	RaycastHit2D[] Hits = Physics2D.RaycastAll((), Vector2.zero);

	}
		
	// sheriff receive message
	public void OnEnable()
	{
		
		BobMiner.OnJesseRobBank += InJesseRobBank;
	}

	public void OnDisable()
	{
		BobMiner.OnJesseRobBank -= InJesseRobBank;
	}	

	public void InJesseRobBank(){

		Debug.Log ("Bob send message to Wyatt: Jesse is robbing bank");
	}

	// sensing
	public override bool DetectsModality (Signal signal)
	{
		// only have sight.
		// only worry about sensing the outlaw. And ignore all others.
		return signal.modality is SightModality && signal.sender is JesseOutlaw;
	}

	public override void Notify (Signal signal)
	{
		if (signal.sender.Position().Equals(Locations.BANK.toVector3 ())) {
			Debug.Log ("Wyatt: freeze... I am shooting outlaw dead..."); 
			isOutlawDead ();
		}
	}

	public override Vector3 Position ()
	{
		return transform.position;
	}
}

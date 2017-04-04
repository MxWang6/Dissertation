using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElsaWife : Agent {

	private StateMachine<ElsaWife> stateMachine;
	private Position currentPosition;

	public delegate void CookIsReady();
	public static event CookIsReady OnCookIsReady;

	// higher value represnts more cooked. 10 means fully cooked.
	private int cooking;
	private int tired;
	private int printValue = 0;

	public void OnEnable()
	{
		BobMiner.OnBobBackHome += BobIsBackHome;
	}


	public void OnDisable()
	{
		BobMiner.OnBobBackHome -= BobIsBackHome;
	}

	void BobIsBackHome()
	{
		Debug.Log ("Bob tells Elsa: I am going back home...");
		this.ChangeState (CookStew.Instance);
	}

	public void Cooking() {
		cooking += 2;
	}

	public void FinishCooking() {
		cooking = 0;
	}

	public bool isCooking() {
		return cooking > 0;
	}

	public void HouseWork(){

		tired += 1;
	}

	public bool GoToBathroom(){

		if (tired >= 10) {
			return true;
		} else {
			return false;
		}
	}

	public void AfterBathroom(){
		tired = 0;
	}

	public bool IsFullyCooked() {
		if (cooking >= 10) {
			if (OnCookIsReady != null) {
				OnCookIsReady ();
			}
			return true;
		}
		return false;
	}

	public void Awake() {
		stateMachine = new StateMachine<ElsaWife>();
		stateMachine.Init(this, DoHouseWork.Instance);
		Debug.Log("Elsa: Wouderful day!"); 
	}

	public void Start() {
		currentPosition = Locations.SHACK;
		transform.position = currentPosition.toVector3 ();
		Time.fixedDeltaTime = 0.5f;
	}

	public Position GetPosition() {
		return currentPosition;
	}

	public override void Update() {

		printValue++;
		stateMachine.Update ();
	}

	public int getPrintValue(){

		return printValue;
	}

	public void zeroPrintValue(){
		printValue = 0;
	}

	public void ChangeState(State<ElsaWife> state){
		this.stateMachine.ChangeState(state);

	}
		
	public void RevertToPreviousState(){
		stateMachine.RevertToPreviousState();
	}
}

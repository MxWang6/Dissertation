using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undertaker : Agent {

	public enum Location {
		SheriffOffice
	};

	private StateMachine<Undertaker> stateMachine;
	private Position currentPosition;

	public void Awake() {

		stateMachine = new StateMachine<Undertaker>();
		//stateMachine.Init(this, LurkInOutlawCampState.Instance);
		//this.RandomValue ();
	}

	public void Start() {
		currentPosition = Locations.CEMETERY;
		transform.position = currentPosition.toVector3 ();
		Time.fixedDeltaTime = 0.4f;
	} 

	public override void Update(){

		stateMachine.Update();
	}

	public void ChangeState(State<Undertaker> state){
		stateMachine.ChangeState(state);

	}

	public void RevertToPreviousState(){
		stateMachine.RevertToPreviousState();
	}
}

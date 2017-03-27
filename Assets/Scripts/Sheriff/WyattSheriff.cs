using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyattSheriff : Agent {

	public enum Location {
		SheriffOffice
	};

	private StateMachine<WyattSheriff> stateMachine;
	private Position currentPosition;

	public void Awake() {

		stateMachine = new StateMachine<WyattSheriff>();
		//stateMachine.Init(this, LurkInOutlawCampState.Instance);
		//this.RandomValue ();
		//	currentPosition = new Position(Locations.OutlawCamp.x, Locations.OutlawCamp.y);
		currentPosition = new Position(20,2);
		transform.position = currentPosition.toVector3();
		Time.fixedDeltaTime = 0.5f;
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
}

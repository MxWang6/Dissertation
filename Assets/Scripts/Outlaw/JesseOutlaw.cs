using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesseOutlaw : Agent {

	public enum Location {
		OutlawCamp,
		Cemetery
	};

	private StateMachine<JesseOutlaw> stateMachine;
	private Position currentPosition;
	private int FindOpportunity = 0;
	private int Value = 0;
	private int Thirst = 0;
	private int TimeToRob = 0;
	private int GoldCarried = 0;

	//event
	public delegate void GoToRobBank();
	public static event GoToRobBank OnRobBank;

	public void Awake() {

		stateMachine = new StateMachine<JesseOutlaw>();
		stateMachine.Init(this, LurkInOutlawCampState.Instance);
		//this.RandomValue ();
		//	currentPosition = new Position(Locations.OutlawCamp.x, Locations.OutlawCamp.y);
		currentPosition = new Position(17,10);
		transform.position = currentPosition.toVector3();
		Time.fixedDeltaTime = 0.5f;
	}

	public override void Update(){

		TimeToRob++;
		stateMachine.Update();
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

	public bool RobBank(){

		if (TimeToRob > 2) {
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
		GoldCarried = Random.Range (5, 10);
		return GoldCarried;
	}

}

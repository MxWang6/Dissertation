using UnityEngine;
using System.Collections;

public class RandomCheckState : State<WyattSheriff>
{

	private static readonly RandomCheckState instance = new RandomCheckState();

	private RandomCheckState() {
		// private constructor to prevent instantiation.
	}

	public static RandomCheckState Instance {
		get {
			return instance;
		}
	}

	public override void Enter (WyattSheriff wf) {
		if (wf.GetLocation () != WyattSheriff.Location.Office) {
			Debug.Log ("Wyatt: Arrived...");
			wf.ChangeLocation (WyattSheriff.Location.Office);
		} else {
			Debug.Log("Wyatt: Daily check");
		}
	}

	public override void Execute (WyattSheriff wf) {

		Debug.Log ("..........");
		
	}

	public override void Exit(WyattSheriff wf) {

		Debug.Log ("..........");
	}
}


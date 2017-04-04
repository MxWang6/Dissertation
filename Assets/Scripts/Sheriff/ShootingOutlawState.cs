using UnityEngine;
using System.Collections;

public class ShootingOutlawState : State<WyattSheriff>
{

	private static readonly ShootingOutlawState instance = new ShootingOutlawState();

	private ShootingOutlawState() {
		// private constructor to prevent instantiation.
	}

	public static ShootingOutlawState Instance {
		get {
			return instance;
		}
	}

	public override void Enter (WyattSheriff wf) {

		Debug.Log ("Wyatt: You are here! let me kill you");
	}

	public override void Execute (WyattSheriff wf) {

		if (wf.isOutlawDead()) {
			wf.getGoldFromOutlaw ();
			Debug.Log ("Wyatt: Get Gold from outlaw - " + wf.getGoldInBank ());
			wf.callMessage ();
			wf.ChangeState (SaveMoneyOnBankState.Instance);
		}
	}

	public override void Exit(WyattSheriff wf) {

		Debug.Log ("Wyatt: WELL DONE !");
	}
}


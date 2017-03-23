using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuenchThirstState : State<BobMiner>
{
	private static readonly QuenchThirstState instance = new QuenchThirstState();

	private QuenchThirstState() {
		// private constructor to prevent instantiation.
	}

	public static QuenchThirstState Instance {
		get {
			return instance;
		}
	}

	public override void Enter (BobMiner m) {
		Debug.Log("Arrive the Bistro.");
		m.ChangeLocation (BobMiner.Location.Saloon);
	}

	public override void Execute (BobMiner m) {
		if (m.IsThirstQuenched ()) {
			m.ChangeState (EnterMineAndDigForNuggets.Instance);

		} else {
			Debug.Log ("Drinking water... ");
			m.quenchThirst ();
			if (m.IsThirstQuenched ()) {
				m.ChangeState (EnterMineAndDigForNuggets.Instance);
			}
		}
	}

	public override void Exit(BobMiner m) {
		Debug.Log ("Leave the Bistro.");
	}
}

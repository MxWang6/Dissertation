using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHomeAndSleepTillRested : State<BobMiner> {

	private static readonly GoHomeAndSleepTillRested instance = new GoHomeAndSleepTillRested();

	private GoHomeAndSleepTillRested() {
		// private constructor to prevent instantiation.
	}

	public static GoHomeAndSleepTillRested Instance {
		get {
			return instance;
		}
	}
		
	public override void Enter (BobMiner m) {
		Debug.Log("Arrive Home.");
		m.GoBackHome ();
		m.ChangeLocation (BobMiner.Location.Shack);
	}

	public override void Execute (BobMiner m) {
		if (m.IsFullyRested ()) {
			if (m.IsThirsty ()) {
				m.ChangeState (QuenchThirstState.Instance);
			} else {
				m.ChangeState (EnterMineAndDigForNuggets.Instance);
			}
		} else {
			Debug.Log (" ZZZZZ....");
			m.SleepAndRest ();
		}
	}

	public override void Exit(BobMiner m) {
		Debug.Log ("Wake up and leave home.");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHomeAndSleepTillRested : State<Miner> {

	static readonly GoHomeAndSleepTillRested instance = new GoHomeAndSleepTillRested();
	public static GoHomeAndSleepTillRested Instance {
		get {
			return instance;
		}
	}
	static GoHomeAndSleepTillRested() { }
	private GoHomeAndSleepTillRested() { }

	public override void Enter (Miner m) {
		if (m.m_Location != M_Locations.shack) {
			Debug.Log("Arrived Home");
			m.ChangeLocation(M_Locations.shack);
		}
	}

	public override void Execute (Miner m) {

		Debug.Log (" ZZZZZ.... ");

	}

	public override void Exit(Miner m) {
		
	}
}

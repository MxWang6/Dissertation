using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMineAndDigForNuggets : State<BobMiner> {

	private static readonly EnterMineAndDigForNuggets instance = new EnterMineAndDigForNuggets();

	private EnterMineAndDigForNuggets() {
		// private constructor to prevent instantiation.
	}

	public static EnterMineAndDigForNuggets Instance {
		get {
			return instance;
		}
	}

	public override void Enter (BobMiner m) {
		Debug.Log("Entering the goldmine...");
		m.ChangeLocation (BobMiner.Location.Goldmine);
	}

	public override void Execute (BobMiner m) {
		if (m.IsPocketFull ()) {
			m.ChangeState (VisitBankAndDepositGold.Instance);
		} else if (m.IsFatigue ()) {
			m.ChangeState (GoHomeAndSleepTillRested.Instance);
		} else if (m.IsThirsty ()) {
			m.ChangeState (QuenchThirstState.Instance);
		} else {
			m.DigNugget();
			Debug.Log("Pickin' up a nugget and that's..." + m.getCarriedGold());
		}

		//    m.MoveToward (GameObject.Find("GameManager").GetComponent<BoardManager>().gridMap["bank"]);
		
	}

	public override void Exit(BobMiner m) {
		Debug.Log("Leaving the mine...");
	}
}

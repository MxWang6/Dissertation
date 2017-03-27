using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitBankAndDepositGold : State<BobMiner> {

	private static readonly VisitBankAndDepositGold instance = new VisitBankAndDepositGold();

	private VisitBankAndDepositGold() { 
		// private constructor to prevent instantiation.
	}

	public static VisitBankAndDepositGold Instance{
		get {
			return instance;
		}
	}

	public override void Enter (BobMiner m) {
		Debug.Log("Bob: Entering the bank...");
		m.ChangeLocation (BobMiner.Location.Bank);
	}

	public override void Execute (BobMiner m) {
		if (m.IsWealthyEnough ()) {
			m.ChangeState (GoHomeAndSleepTillRested.Instance);
		} else if (m.IsGoldDeposited ()) {
			m.ChangeState (EnterMineAndDigForNuggets.Instance);
		} else {
			m.DepositGoldToBank();
			Debug.Log("Bob: Depositing gold. Total savings now:" + m.getGoldInBank());
		}
	}

	public override void Exit(BobMiner m) {
		Debug.Log("Bob: Leaving the bank...");
	}


}

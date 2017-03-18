using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitBankAndDepositGold : State<Miner> {

	static readonly VisitBankAndDepositGold instance = new VisitBankAndDepositGold();

	public static VisitBankAndDepositGold Instance{
		get {
			return instance;
		}
	}
	static VisitBankAndDepositGold() { }
	private VisitBankAndDepositGold(){ }


	public override void Enter (Miner m) {
		
		if (m.m_Location != M_Locations.bank) {
			Debug.Log("Entering the bank...");
			m.ChangeLocation(M_Locations.bank);
		}

	}

	public override void Execute (Miner m) {

		m.AddToMoneyInBank(m.GoldCarried);
		Debug.Log("Depositing gold. Total savings now:" + m.MoneyInBank);
		if (m.RichEnough ()) {
			m.ChangeState (GoHomeAndSleepTillRested.Instance);
		}
		else{
		m.ChangeState(EnterMineAndDigForNuggets.Instance);
		}
	}

	public override void Exit(Miner m) {
		Debug.Log("Leaving the bank...");
	}

}

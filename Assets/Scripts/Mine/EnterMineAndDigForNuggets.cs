using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMineAndDigForNuggets : State<Miner> {

	static readonly EnterMineAndDigForNuggets instance = new EnterMineAndDigForNuggets();

	public static EnterMineAndDigForNuggets Instance {
		get {
			return instance;
		}
	}
	static EnterMineAndDigForNuggets() { }
	private EnterMineAndDigForNuggets() { }

	public override void Enter (Miner m) {
		if (m.m_Location != M_Locations.goldmine) {
			Debug.Log("Entering the goldmine...");
		//	m.MoveToward (GameObject.Find("GameManager").GetComponent<BoardManager>().gridMap["goldMine"]);
			m.ChangeLocation(M_Locations.goldmine);
		}
	}

	public override void Execute (Miner m) {
		m.AddToGoldCarried(1);
		Debug.Log("Pickin' up a nugget and that's..." + m.GoldCarried);
		m.IncreaseFatigue();
		if (m.PocketsFull())
			m.ChangeState(VisitBankAndDepositGold.Instance);
		//    m.MoveToward (GameObject.Find("GameManager").GetComponent<BoardManager>().gridMap["bank"]);
		
	}

	public override void Exit(Miner m) {
		Debug.Log("Leaving the mine with my pockets full...");
	}
}

using UnityEngine;
using System.Collections;

public class SaveMoneyOnBankState : State<WyattSheriff>
{

	private static readonly SaveMoneyOnBankState instance = new SaveMoneyOnBankState();

	private SaveMoneyOnBankState() {
		// private constructor to prevent instantiation.
	}

	public static SaveMoneyOnBankState Instance {
		get {
			return instance;
		}
	}

	public override void Enter (WyattSheriff wf) {

		if (wf.GetLocation()!= WyattSheriff.Location.Bank) {

			wf.ChangeLocation (WyattSheriff.Location.Bank);
		}
		Debug.Log ("Wyatt: Arrived in Bank");
	}

	public override void Execute (WyattSheriff wf) {

		wf.DepositGoldToBank();
		Debug.Log ("Wyatt: Save gold to Bank - " + wf.getGoldInBank());
		wf.ChangeState (CelebrateDayWorkState.Instance);
	}

	public override void Exit(WyattSheriff wf) {

		Debug.Log ("Wyatt: Leaving the Bank !");
	}
}


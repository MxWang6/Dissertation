using UnityEngine;
using System.Collections;

public class RobBankState : State<JesseOutlaw>
{

	private static readonly RobBankState instance = new RobBankState();
	private RobBankState() {
		// private constructor to prevent instantiation.
	}

	public static RobBankState Instance {
		get {
			return instance;
		}
	}

	public override void Enter (JesseOutlaw outlaw) {

		Debug.Log("Jesse: Arrived in bank, Let's EARN some money!"); 
		outlaw.ChangeLocation (JesseOutlaw.Location.Bank);
	}

	public override void Execute (JesseOutlaw outlaw) {


		Debug.Log ("Jesse: Total harvest now: " + outlaw.RobGoldInBank());
		outlaw.finishRob ();
		outlaw.RevertToPreviousState ();


	}

	public override void Exit(JesseOutlaw outlaw) {

		Debug.Log ("Jesse: Breaking away from the bank.");
	}



}


using UnityEngine;
using System.Collections;

public class LurkInCemeteryState : State<JesseOutlaw>
{

	private static readonly LurkInCemeteryState instance = new LurkInCemeteryState();
	private LurkInCemeteryState() {
		// private constructor to prevent instantiation.
	}

	public static LurkInCemeteryState Instance {
		get {
			return instance;
		}
	}

	public override void Enter (JesseOutlaw outlaw) {

		Debug.Log("Jesse: Arrived in the cemetery!"); 

	}

	public override void Execute (JesseOutlaw outlaw) {

		outlaw.RandomValue ();
		if (outlaw.EndLukIn ()) {
			outlaw.ChangeState (LurkInOutlawCampState.Instance);
			outlaw.InitialValue ();
		} else if(outlaw.RobBank()){
			outlaw.ChangeState (RobBankState.Instance);
		}else
		{
			outlaw.LukIn ();
			Debug.Log ("Jess: Lurking in the cemetery");
		}
		}

		
	public override void Exit(JesseOutlaw outlaw) {

		Debug.Log ("Jess: Leaving in the cemetery");
	}
}






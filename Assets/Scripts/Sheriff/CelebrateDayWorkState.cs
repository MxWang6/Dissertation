using UnityEngine;
using System.Collections;

public class CelebrateDayWorkState : State<WyattSheriff>
{

	private static readonly CelebrateDayWorkState instance = new CelebrateDayWorkState();

	private CelebrateDayWorkState() {
		// private constructor to prevent instantiation.
	}

	public static CelebrateDayWorkState Instance {
		get {
			return instance;
		}
	}

	public override void Enter (WyattSheriff wf) {

		Debug.Log ("Wyatt: Arrived in the saloon!");
		wf.ChangeLocation (WyattSheriff.Location.Saloon);
	}

	public override void Execute (WyattSheriff wf) {


		Debug.Log ("Wyatt: Celebrate! All drinks on me today!");
		wf.ChangeState (RandomCheckState.Instance);

	}

	public override void Exit(WyattSheriff wf) {

		Debug.Log ("Wyatt: Leaving the saloon");
	}

}


using UnityEngine;
using System.Collections;

public class RandomCheckState : State<WyattSheriff>
{

	private static readonly RandomCheckState instance = new RandomCheckState();

	private RandomCheckState() {
		// private constructor to prevent instantiation.
	}

	public static RandomCheckState Instance {
		get {
			return instance;
		}
	}

	public override void Enter (WyattSheriff wf) {
		if (wf.GetLocation () != WyattSheriff.Location.Office) {
			Debug.Log ("Wyatt: Arrived...");
			wf.ChangeLocation (WyattSheriff.Location.Office);
		}
	}

	public override void Execute (WyattSheriff wf) {

		int n = Random.Range (1, 5);
		Debug.Log ("Wyatt: Let us keep everyone safe ");
		switch (n) {
		case 1:
			{
				wf.ChangeLocation (WyattSheriff.Location.Shack);
				if (!wf.isOutlawHere ()) {
					Debug.Log ("Wyatt: Safe place - Going to another place - Shack");
				} else {
					wf.ChangeState (ShootingOutlawState.Instance);
				}
			}
			break;
		case 2:
			{
				wf.ChangeLocation (WyattSheriff.Location.Bank);
				if (!wf.isOutlawHere ()) {
					Debug.Log ("Wyatt: Safe place - Going to another place - Bank");

				}else {
					wf.ChangeState (ShootingOutlawState.Instance);
				}
			}
			break;

		case 3:
			{
				wf.ChangeLocation (WyattSheriff.Location.Saloon);
				if (!wf.isOutlawHere ()) {
					Debug.Log ("Wyatt: Safe place - Going to another place - Saloon");

				}else {
					wf.ChangeState (ShootingOutlawState.Instance);
				}
			}
			break;
		case 4:
			{
				wf.ChangeLocation (WyattSheriff.Location.Cemetery);
				if (!wf.isOutlawHere ()) {
					Debug.Log ("Wyatt: Safe place - Going to another place - Cemetery");

				}else {
					wf.ChangeState (ShootingOutlawState.Instance);
				}
			}
			break;
		default:
			break;
		}
		
	}

	public override void Exit(WyattSheriff wf) {


	}
}


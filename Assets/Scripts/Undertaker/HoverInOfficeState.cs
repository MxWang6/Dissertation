using UnityEngine;
using System.Collections;

public class HoverInOfficeState : State<Undertaker>
{

	private static readonly HoverInOfficeState instance = new HoverInOfficeState();

	private HoverInOfficeState() {
		// private constructor to prevent instantiation.
	}

	public static HoverInOfficeState Instance {
		get {
			return instance;
		}
	}

	public override void Enter (Undertaker u) {

		if (u.GetLocation() != Undertaker.Location.Undertaker) {
			
			Debug.Log ("Undertaker: Arrived in the office");
			u.ChangeLocation (Undertaker.Location.Undertaker);
		}
	}

	public override void Execute (Undertaker u) {

		if (u.getRandomValue() == 0) {
			Debug.Log ("Undertaker: Hovering in the office");
		}
	}

	public override void Exit(Undertaker u) {

		Debug.Log ("Undertaker: Leaving in the office");
		u.leavingOffice ();
	}


}


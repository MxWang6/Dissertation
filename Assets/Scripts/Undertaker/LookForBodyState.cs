using UnityEngine;
using System.Collections;

public class LookForBodyState : State<Undertaker>{

	private static readonly LookForBodyState instance = new LookForBodyState();

	private LookForBodyState() {
		// private constructor to prevent instantiation.
	}

	public static LookForBodyState Instance {
		get {
			return instance;
		}
	}

	public override void Enter (Undertaker u) {
		

		Debug.Log ("Undertaker: Going to find body");
		u.ChangeLocation (Undertaker.Location.Cemetery);

	}

	public override void Execute (Undertaker u) {

		Debug.Log ("Undertaker: Find body and drag it off to the Cemetery");
		u.ChangeState (HoverInOfficeState.Instance);

	}

	public override void Exit(Undertaker u) {

		Debug.Log ("Undertaker: Leaving........");
	}
}


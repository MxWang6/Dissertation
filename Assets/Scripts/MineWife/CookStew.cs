using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookStew : State<ElsaWife> {

	private static readonly CookStew instance = new CookStew();
	private CookStew() {
		// private constructor to prevent instantiation.
	}

	public static CookStew Instance {
		get {
			return instance;
		}
	}

	public override void Enter(ElsaWife mw)
	{
		if (!mw.isCooking ()) {
			// MinersWife sends a delayed message to herself to arrive when the food is ready
			Debug.Log( "Putting the stew in the oven");
		}
	}

	public override void Execute(ElsaWife mw)
	{
		if (mw.IsFullyCooked ()) {
			mw.RevertToPreviousState ();
		} else {
			Debug.Log("Elsa: Fussin' over food");
			mw.Cooking ();
		}
	}

	public override void Exit(ElsaWife mw)
	{
		mw.FinishCooking ();
		Debug.Log("Elsa: Puttin' the stew on the table");
	}
}

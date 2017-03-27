using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitBathroom : State<ElsaWife> {

	private static readonly VisitBathroom instance = new VisitBathroom();
	private VisitBathroom() {
		// private constructor to prevent instantiation.
	}

	public static VisitBathroom Instance {
		get {
			return instance;
		}
	}

	public override void Enter(ElsaWife mw)
	{
		Debug.Log("Elsa: Walkin' to the can. Need to powda mah pretty li'lle nose");
	}

	public override void Execute(ElsaWife mw)
	{
		Debug.Log("Elsa: Ahhhhhh! Sweet relief!");
		mw.AfterBathroom ();
		mw.RevertToPreviousState ();
	}

	public override void Exit(ElsaWife mw)
	{
		Debug.Log("Elsa: Leavin' the Jon");
	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoHouseWork : State<ElsaWife> {


	private static readonly DoHouseWork instance = new DoHouseWork();
	private DoHouseWork() {
		// private constructor to prevent instantiation.
	}

	public static DoHouseWork Instance {
		get {
			return instance;
		}
	}

	public override void Enter (ElsaWife mw) {
		
		Debug.Log("Elsa: It is time to do house works"); 
	}

	public override void Execute (ElsaWife mw) {
		
		if (!mw.GoToBathroom ()) {
			switch (Random.Range (1, 3)) {
			case 1:
				Debug.Log ("Elsa: Moppin' the floor"); 
				mw.HouseWork ();
				break;
			case 2:
				Debug.Log ("Elsa: Washin' the dishes"); 
				mw.HouseWork ();
				break;
			case 3:
				Debug.Log ("Elsa: Makin' the bed"); 
				mw.HouseWork ();
				break;
			default:
				break;

			}
		} else {

			mw.ChangeState (VisitBathroom.Instance);
		}
	}

	public override void Exit(ElsaWife mw) {

	}

}

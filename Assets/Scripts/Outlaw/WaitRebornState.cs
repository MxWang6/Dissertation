using UnityEngine;
using System.Collections;

public class WaitRebornState : State<JesseOutlaw>
{

	private static readonly WaitRebornState instance = new WaitRebornState();
	private WaitRebornState() {
		// private constructor to prevent instantiation.
	}

	public static WaitRebornState Instance {
		get {
			return instance;
		}
	}

	public override void Enter (JesseOutlaw outlaw) {

		Debug.Log ("Jesse: Next time, I 'd like be a good man. Goodbye");

	}

	public override void Execute (JesseOutlaw outlaw) {

		if (outlaw.EnoughTimeToWait ()) {
			outlaw.rebornJesse ();
		} else {
			Debug.Log ("...Wait...Wait...I will come back !!");
			outlaw.CreateTime ();
		}
	}

	public override void Exit(JesseOutlaw outlaw) {

		Debug.Log ("Jesse: Time to live");
	}
}


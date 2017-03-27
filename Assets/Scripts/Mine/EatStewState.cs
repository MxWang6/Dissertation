using UnityEngine;
using System.Collections;

public class EatStewState : State<BobMiner>
{
	private static readonly EatStewState instance = new EatStewState();

	private EatStewState() {
		// private constructor to prevent instantiation.
	}

	public static EatStewState Instance {
		get {
			return instance;
		}
	}
	public override void Enter(BobMiner m)
	{
		Debug.Log("Bob: Smells Reaaal goood Elsa!");
	}

	public override void Execute(BobMiner m)
	{
		if (m.StewFullyEaten ()) {
			m.RevertToPreviousState ();
		} else {
			Debug.Log("Bob: Tastes real good too! Thankya li'lle lady. Ah better get back to whatever ah wuz doin'");
			m.EatStew ();
		}
	}

	public override void Exit(BobMiner m)
	{
		Debug.Log("Bob: Going to sleep,zzzz....'");
	
	}



}


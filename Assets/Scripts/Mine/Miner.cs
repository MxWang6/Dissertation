using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum M_Locations { goldmine, saloon, bank, shack };


public class Miner : Agent {

	private StateMachine<Miner> M_stateMachine;

	public GameObject test;
	public M_Locations m_Location = M_Locations.goldmine;

	protected BoardManager boardScript;

	public int GoldCarried = 0;
	public int MoneyInBank  = 0;
	public int Thirst = 0;
	public int Fatigue = 0;
	public int ComfortLevel = 3;        

	public void Awake() {
		
		boardScript = GameObject.Find("GameManager").GetComponent<BoardManager>();

		Debug.Log("Miner awakes...");
		M_stateMachine = new StateMachine<Miner>();
		M_stateMachine.Init(this, EnterMineAndDigForNuggets.Instance);


	}

	public void ChangeState(State<Miner> e) {
		M_stateMachine.ChangeState(e);
	}

	public override void Update() {
		Thirst++;
		M_stateMachine.Update();
	}

	public void ChangeLocation(M_Locations l) {
		m_Location = l;
	}

	public void AddToGoldCarried(int amount) {
		GoldCarried += amount;
	}

	public void AddToMoneyInBank(int amount ) {
		MoneyInBank += amount;
		GoldCarried = 0;
	}

//	public bool RichEnough() {
//		return false;
//	}

	public bool PocketsFull() {
		bool full = GoldCarried ==  3 ? true : false;
		return full;
	}

	public bool Thirsty() {
		bool thirsty = Thirst == 5 ? true : false;
		return thirsty;
	}

	public void IncreaseFatigue() {
		Fatigue++;
	}

	public bool RichEnough(){
		if (MoneyInBank >= ComfortLevel)
			return true;
		else
			return false;
       
	}
}

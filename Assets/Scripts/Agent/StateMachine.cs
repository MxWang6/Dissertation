using UnityEngine;

public class StateMachine<T> where T: Agent {
	
	private T agent;

	private State<T> currentState;
	private State<T> previouState;
	private State<T> globalState;

	public void Awake () {
		this.currentState = null;
		this.previouState = null;
		this.globalState = null;
	}

	public void Init (T agent, State<T> startState) {
		this.agent = agent;
		this.currentState = startState;
	}

	public void Update () {
		if (this.globalState != null) {
			this.globalState.Execute (this.agent);
		}
		if (this.currentState != null) {
			this.currentState.Execute (this.agent);
		}
	}
	
	public void ChangeState (State<T> newState) {
		
		if (this.currentState != null) {
			this.currentState.Exit (this.agent);
		}
		this.previouState = this.currentState;

		if (newState != null) {
			newState.Enter (this.agent);
		}
		this.currentState = newState;
	}

	public void RevertToPreviousState(){
		if (previouState != null) {
			ChangeState (previouState);
		}
	}

}
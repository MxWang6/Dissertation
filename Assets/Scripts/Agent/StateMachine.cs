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
//		BoardManager boardScript = GameObject.Fi nd("GameManager").GetComponent<BoardManager>();
//		Vector3 position = boardScript.worldGrid[agent.gridPosition.x, agent.gridPosition.y].transform.position;
//		agent.transform.position = new Vector3(position.x, position.y, agent.transform.position.z);
		if (this.globalState != null)
			this.globalState.Execute (this.agent);
		if (this.currentState != null) 
			this.currentState.Execute(this.agent);
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
		if (previouState != null)
			ChangeState(previouState);
	}
}
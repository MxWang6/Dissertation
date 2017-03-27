public abstract class State <T> {

	public abstract void Enter (T agent);
	public abstract void Execute (T agent);
	public abstract void Exit (T agent);
	//public abstract bool OnMessage(T agent, Telegram telegram);
}
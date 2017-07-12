using System.Collections;

public class MonsterMoveEvent
{
	public Monster monster;
	public Position currentPos;
	public Position newPos;

	public MonsterMoveEvent(Monster monster, Position currentPos, Position newPos) {
		this.monster = monster;
		this.currentPos = currentPos;
		this.newPos = newPos;
	}
}


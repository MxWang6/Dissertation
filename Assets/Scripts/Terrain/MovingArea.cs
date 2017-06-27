using UnityEngine;
using Random = UnityEngine.Random;

public class MovingArea
{
	private GridWorld gridWorld;
	private int x;
	private int y;
	private int width;
	private int height;

	public MovingArea (GridWorld gridWorld, Position origin, int width, int height)
	{
		this.gridWorld = gridWorld;
		this.x = origin.x - width/2;
		this.y = origin.y - height/2;
		this.width = width;
		this.height = height;
	}

	public Vector2 getNextRandomPosition(Vector2 currentPos) {
		Vector2 nextPos = new Vector2 ();

		int nextStep = Random.Range (0, 4);
		int offsetX = 0;
		int offsetY = 0;
		if (nextStep == 0) {
			// move up
			offsetY++;
		} else if (nextStep == 1) {
			// move right
			offsetX++;
		} else if (nextStep == 2) {
			// move down
			offsetY--;
		} else if (nextStep == 3) {
			// move left
			offsetX--;
		}

		nextPos.x = currentPos.x + offsetX;
		nextPos.y = currentPos.y + offsetY;

		// We only return the new position if it is in the range of the moving area and its not blocked.
		return isWithinMovingArea (nextPos) && !isBlocked (nextPos) ? nextPos : currentPos;
	}

	private bool isWithinMovingArea(Vector2 position) {
		return this.x <= position.x && position.x <= this.x + width && this.y <= position.y && position.y <= this.y + height;
	}

	private bool isBlocked(Vector2 position) {
		return gridWorld.getTile (new Position(position.x, position.y)).blocked;
	}


}

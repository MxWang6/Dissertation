using UnityEngine;
using Random = UnityEngine.Random;

public class MovingArea
{
	private int x;
	private int y;
	private int width;
	private int height;

	public MovingArea (Position origin, int width, int height)
	{
		this.x = origin.x - width/2;
		this.y = origin.y - height/2;
		this.width = width;
		this.height = height;
	}

	public Vector2 getNextRandomPosition(Vector2 currentPos) {
		Vector2 nextPosition = new Vector2 ();

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

		nextPosition.x = currentPos.x + offsetX;
		nextPosition.y = currentPos.y + offsetY;

		// We only return the new position if it is in the range of the moving area.
		if (this.x <= nextPosition.x &&  nextPosition.x <= this.x + width && this.y <= nextPosition.y && nextPosition.y <= this.y + height) {
			return nextPosition;
		}

		return currentPos;
	}
}

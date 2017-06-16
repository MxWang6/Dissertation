using System;
using UnityEngine;

public class Position {
	public int x;
	public int y;
	public Position (int x, int y) {
		this.x = x;
		this.y = y;
	}

	public Position (float x, float y){

		this.x = (int)x;
		this.y = (int)y;
	}

	public Vector3 toVector3() {
		return new Vector3 (x, y, 0f);
	}

	public override bool Equals (object obj)
	{
		if (!(obj is Position)) {
			return false;
		}

		Position thePostion = (Position) obj;
		if (thePostion.x == x && thePostion.y == y) {
			return true;
		}

		return false;
	}
}

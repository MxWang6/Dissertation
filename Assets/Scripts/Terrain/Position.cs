using System;
using UnityEngine;

public class Position {
	public int x;
	public int y;
	public Position (int x, int y) {
		this.x = x;
		this.y = y;
	}

	public Vector3 toVector3() {
		return new Vector3 (x, y, 0f);
	}
}

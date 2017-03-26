using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public bool blocked;
	public Position position;

	public void setPosition(Position position) {
		this.position = position;
	}

	public Position getPosition() {
		return position;
	}
}

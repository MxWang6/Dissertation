using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal {
	public int strength;
	public Modality modality;
	public Sensor sender;

	public Signal() {
		strength = 10;
		modality = new SightModality ();
	}
}

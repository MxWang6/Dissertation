using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensor : MonoBehaviour {
	// all sensors can see the signal's stregth greater than 4.
	public int threshold = 4;
	// the current position of the sensor
	public abstract Vector3 Position();
	// check if this sensor is able to detect such modality.
	public abstract bool DetectsModality (Signal signal);
	// notify the signal to this sensor.
	public abstract void Notify (Signal signal);
}

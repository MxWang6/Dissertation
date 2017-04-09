using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightModality : Modality {
	private float attenuation;
	private int maximumRange;
	private int transmissionSpeed;

	public SightModality() {
		this.maximumRange = 20;
		this.attenuation = 0.8f;
		this.transmissionSpeed = 300000000; // light transmission is instant
	}
		
	public float Attenuation () {
		return attenuation;
	}

	public int MaximumRange () {
		return maximumRange;
	}

	public int TransmissionSpeed() {
		return transmissionSpeed;
	}

	public bool ExtraChecks (Signal signal, Sensor sensor) {
		// only tile that marked as blocked can block the sight, e.g. tree.
		Vector3 sightDirection = signal.sender.Position() - sensor.Position();
		RaycastHit2D[] hits = Physics2D.RaycastAll (new Vector2(sensor.Position().x, sensor.Position().y), new Vector2(sightDirection.x, sightDirection.y));

		// no block detected in the sight.
		if (hits.Length <= 2) {
			return true;
		}

		GameObject gameObject = hits [1].collider.gameObject;
		if (signal.sender == gameObject.GetComponent<JesseOutlaw>() || signal.sender == gameObject.GetComponent<BobMiner>() || signal.sender == gameObject.GetComponent<WyattSheriff>()) {
			return true;
		}

		return false;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RegionalSenseManager : MonoBehaviour {

	private List<Sensor> sensors = new List<Sensor> ();
	private Queue<Notification> notificationQueue = new Queue<Notification> ();

	// Update is called once per frame
	void Update () {
		SendSignal ();
	}

	public void Register(Sensor sensor) {
		sensors.Add (sensor);
	}

	public void AddSignal(Signal signal) {
		foreach (Sensor sensor in sensors) {
			if (sensor.Equals (signal.sender)) {
				continue;
			}

			if (!sensor.DetectsModality (signal)) {
				continue;
			}

			double distance = Distance (signal.sender.Position(), sensor.Position());
			if (signal.modality.MaximumRange () < distance) {
				continue;
			}

			double intensity = signal.strength * Math.Pow (signal.modality.Attenuation (), distance);
			if (intensity < sensor.threshold) {
				continue;
			}

			if (!signal.modality.ExtraChecks (signal, sensor)) {
				continue;
			}

			// going to notify the sensor
			DateTime time = DateTime.Now.AddSeconds(distance / signal.modality.TransmissionSpeed());
			Notification notification = new Notification ();
			notification.time = time;
			notification.signal = signal;
			notification.sensor = sensor;

			notificationQueue.Enqueue (notification);
		}
	}

	public void SendSignal() {
		DateTime currentTime = DateTime.Now;

		while (notificationQueue.Count > 0) {
			Notification notification = notificationQueue.Peek ();

			if (notification.time.CompareTo (currentTime) <= 0) {
				notification.sensor.Notify (notification.signal);
				notificationQueue.Dequeue ();
			} else {
				break;
			}
		}
	}

	private double Distance(Vector3 p1, Vector3 p2) {
		return Math.Sqrt(Math.Pow (p1.x - p2.x, 2) + Math.Pow (p1.y - p2.y, 2));
	}
}

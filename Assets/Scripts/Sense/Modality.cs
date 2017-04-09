using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Modality {

	float Attenuation ();

	int MaximumRange ();

	int TransmissionSpeed();

	bool ExtraChecks (Signal signal, Sensor sensor);
}

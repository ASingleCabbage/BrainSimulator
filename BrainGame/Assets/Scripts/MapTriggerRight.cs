using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTriggerRight : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Destroy (other.gameObject);
	}
}

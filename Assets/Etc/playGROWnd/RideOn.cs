using UnityEngine;
using System.Collections;

public class RideOn : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player") == false) {
			return;
		}

		other.transform.parent = transform;
	}

	void OnTriggerExit(Collider other) {

		if (other.CompareTag ("Player") == false) {
			return;
		}
		
		other.transform.parent = null;


	}
}

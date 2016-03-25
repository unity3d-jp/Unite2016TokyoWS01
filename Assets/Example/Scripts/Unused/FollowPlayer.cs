using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public Rigidbody followRb;

	// Update is called once per frame
	void LateUpdate () {
		this.transform.position = followRb.transform.position;
		Vector3 vel = followRb.velocity;
		if ( vel.magnitude > 0.1f ) {
			vel.Normalize();
			this.transform.forward = vel;
		}
	}


}

using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public float ratio = 30.0f;
	public float gravRatio = 3.0f;
		
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		this.transform.rotation = Quaternion.Euler(-moveVertical*ratio, 0.0f, moveHorizontal*ratio);
		Physics.gravity = this.transform.up * -1.0f * gravRatio;
	}
}

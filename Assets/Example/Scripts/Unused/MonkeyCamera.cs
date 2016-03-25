using UnityEngine;
using System.Collections;

public class MonkeyCamera : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	public Transform gravObj;
	public Vector3 firstRot;

	// Use this for initialization
	void Start () {
		offset = this.transform.position - player.transform.position;
		firstRot = this.transform.rotation.eulerAngles;
	}

	// Update is called once per frame
	void Update () {
		this.transform.rotation = gravObj.rotation;
		this.transform.Rotate(firstRot);
		Vector3 pos = gravObj.TransformPoint(offset);
		this.transform.position = player.transform.position + pos;
	
	}
}

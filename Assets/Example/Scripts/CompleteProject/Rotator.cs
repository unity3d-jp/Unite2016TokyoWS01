using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class Rotator : MonoBehaviour {

		// Update is called once per frame
		void Update () {
			this.transform.Rotate( new Vector3(0, 30, 0) * Time.deltaTime );
		}
	}
}
using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class GyroManager : MonoBehaviour {

		void Start()
		{
			Input.gyro.enabled = true;
			Input.gyro.updateInterval = 0.01f;
		}

		void Update()
		{
			Quaternion rotRH = Input.gyro.attitude;
			Quaternion rot = new Quaternion(-rotRH.x, -rotRH.z, -rotRH.y, rotRH.w) * Quaternion.Euler(90f, 0f, 0f);
			this.transform.rotation = rot;
		}
	}
}

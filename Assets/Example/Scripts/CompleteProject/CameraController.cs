using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class CameraController : MonoBehaviour {

		public GameObject player;
		private Vector3 offset;
		public bool isLookAt;

		void Start () {
			// 差を取っておく
			offset = this.transform.position - player.transform.position;
		}

		// LateUpdate は全てのUpdateが終わったら処理あれるので、今回のような処理にぴったり
		void LateUpdate () {
			if ( isLookAt ) {
				this.transform.LookAt( player.transform.position );
			} else {
				// 毎フレーム差を足しておく
				this.transform.position = player.transform.position + offset;
			}
		}
	}
}

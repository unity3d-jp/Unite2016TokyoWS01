using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class CharaController : MonoBehaviour {

		private Vector3 offset;
		private Animator anim;

		// Use this for initialization
		void Start () {
			offset = this.transform.position - target.transform.position;
			anim = GetComponent<Animator>();
		}

		public GameObject target;
		
		// Update is called once per frame
		void LateUpdate () {
			this.transform.position = target.transform.position + offset;

			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");
			Vector3 movement = new Vector3( moveHorizontal, 0.0f, moveVertical );
			float speed = movement.magnitude;
			if ( speed > 1.0f ) 
			{
				speed = 1.0f;
			}
			if ( speed > 0.1f ) {
				movement.Normalize();
				this.transform.forward = movement;
			}
				
			anim.SetFloat("Speed", speed );
		}

		public void OnCallChangeFace (string str)
		{   
		}
	}
}

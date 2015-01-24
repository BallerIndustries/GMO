using UnityEngine;
using System.Collections;

namespace Cat
{
	public class BowserHammer : MonoBehaviour {
		public int AngularVelocity;
		public Vector2 ThrowForce;

		// Use this for initialization
		void Start () {
			this.rigidbody2D.angularVelocity = AngularVelocity;
			this.rigidbody2D.AddForce (ThrowForce, ForceMode2D.Impulse);
		}
		
		// Update is called once per frame
		void Update () {

		}

		void OnBecameInvisible() {
			Destroy (this.gameObject);
		}
	}
}
using UnityEngine;
using System.Collections;

namespace BurtDev {
	public class JumpThruFromUnder : MonoBehaviour {

		public Transform PlayerTranform;
		public float ColliderSize = 0.3f;

		void Update () {
			if (PlayerTranform.position.y - transform.position.y >= ColliderSize) {
				collider2D.enabled = true;
			} else {
				collider2D.enabled = false;
			}
		}
	}
 }

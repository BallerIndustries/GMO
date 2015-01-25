using UnityEngine;
using System.Collections;
using System;

namespace Cat
{
	public class Feet : MonoBehaviour 
	{
		public bool Grounded;

		public void OnTriggerStay2D(Collider2D other)
		{
			if (other.gameObject.layer != LayerMask.NameToLayer ("Environment"))
			{
				return;
			}
			Grounded = true;
		}

		public void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.layer != LayerMask.NameToLayer ("Environment"))
			{
				return;
			}
			Grounded = false;
		}
	}
}

using UnityEngine;
using System.Collections;

namespace Cat
{
	[RequireComponent(typeof(BowserPlayerController))]
	public class BowserPlayer : MonoBehaviour {

		public float DieJumpHeight;
		public Feet Feet;
		public DefeatBowserController GameController;
		public Animator Animator;
		public ParticleSystem LandParticles;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
			{
				GameController.Lose ();
			}
		}

		public void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
			{
				GameController.Lose();
			}
			else if (other.gameObject.name == "WinAxe")
			{
				Time.timeScale = 0;
				GameController.OpenBridge ();
				Destroy (other.gameObject);
			}
		}

		public void Die()
		{
			Animator.SetTrigger ("die");
			Feet.collider2D.enabled = false;
			this.collider2D.enabled = false;
			this.rigidbody2D.velocity = new Vector2(0, 0);
			this.rigidbody2D.AddForce (new Vector2(0, DieJumpHeight), ForceMode2D.Impulse);
			this.GetComponent<BowserPlayerController>().enabled = false;
		}

		public void EmitLandParticles()
		{
			LandParticles.Emit (10);
		}
	}
}
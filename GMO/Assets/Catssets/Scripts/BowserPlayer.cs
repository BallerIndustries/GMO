using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Cat
{
	[RequireComponent(typeof(BowserPlayerController))]
	public class BowserPlayer : MonoBehaviour {

		public float DieJumpHeight;
		public Feet Feet;
		public DefeatBowserController GameController;
		public Animator Animator;
		public ParticleSystem LandParticles;

		public bool _enteringPipe;

		// Use this for initialization
		void Start () {
			_enteringPipe = false;
		}
		
		// Update is called once per frame
		void Update () {
			Animator.SetBool ("grounded", Feet.Grounded);
			Animator.SetFloat ("velocity", Mathf.Abs(this.rigidbody2D.velocity.x));
		}

		public void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
			{
				Invoke ("Lose", 2f);
				Die ();
			}
		}

		public void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
			{
				Invoke ("Lose", 2f);
				Die ();
			}
			else if (other.gameObject.name == "WinAxe")
			{
				Time.timeScale = 0;
				GameController.OpenBridge ();
				Destroy (other.gameObject);
			}
		}

		private void Lose()
		{
			GameController.Lose ();
		}

		public void Die()
		{
			GameController.CameraController.FollowPlayer = false;
			Animator.SetTrigger ("die");
			Feet.collider2D.enabled = false;
			this.collider2D.enabled = false;
			this.rigidbody2D.velocity = new Vector2(0, 0);
			this.rigidbody2D.AddForce (new Vector2(0, DieJumpHeight), ForceMode2D.Impulse);
			this.GetComponent<BowserPlayerController>().enabled = false;
		}

		public void GoInPipe()
		{
			_enteringPipe = true;
			Animator.SetTrigger ("pipe");
			this.GetComponent<BowserPlayerController>().enabled = false;
			this.rigidbody2D.Sleep();
			var sequence = DOTween.Sequence().OnComplete (() => { GameController.Win ();});
			sequence.Append(transform.DOMove (new Vector3(31, this.transform.localPosition.y, this.transform.localPosition.z), 0.5f));
			sequence.Append (transform.DOMove(new Vector3(0f, -4, 0f), 2f).SetRelative (true));
		}
	
		public void EmitLandParticles()
		{
			if (_enteringPipe)
			{
				return;
			}
			LandParticles.Emit (10);
		}
	}
}
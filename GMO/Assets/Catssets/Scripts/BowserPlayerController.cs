using UnityEngine;
using System.Collections;

namespace Cat
{
	public class BowserPlayerController : MonoBehaviour
	{
		public DefeatBowserController GameController;
		public Feet Feet;
		public int JumpHeight;
		public int AccelerationRate;
		public float Friction;
		public int MaxSpeed;

		private bool _canWin;

		public void Start()
		{
			_canWin = false;
		}

		public void Update ()
		{
			if (Input.GetKeyDown (KeyCode.Space))
			{
				if (Feet.Grounded)
				{
					GameController.AudioController.Play ("Jump");
					this.rigidbody2D.AddForce (new Vector2(0, JumpHeight), ForceMode2D.Impulse);
				}
			}

			if (Input.GetKey (KeyCode.LeftArrow))
			{
				this.rigidbody2D.AddForce (new Vector2(-AccelerationRate, 0));
				this.transform.localScale = new Vector3(-Mathf.Abs (this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
			}
			else if (Input.GetKey (KeyCode.RightArrow))
			{
				this.rigidbody2D.AddForce (new Vector2(AccelerationRate, 0));
				this.transform.localScale = new Vector3(Mathf.Abs (this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
			}
			else if (Input.GetKey (KeyCode.DownArrow))
			{
				if (_canWin)
				{
					this.GetComponent<BowserPlayer>().GoInPipe ();
				}
			}
			else
			{
				if (Feet.Grounded)
				{
					this.rigidbody2D.velocity = new Vector2(this.rigidbody2D.velocity.x * Friction, this.rigidbody2D.velocity.y);
					if (this.rigidbody2D.velocity.magnitude < 0.1f)
					{
						this.rigidbody2D.velocity = new Vector2(0, 0);
					}
				}
			}

			this.rigidbody2D.velocity = new Vector2(Mathf.Clamp(this.rigidbody2D.velocity.x, -MaxSpeed, MaxSpeed), this.rigidbody2D.velocity.y);
		}

		public void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.name == "PipeExit")
			{
				_canWin = true;
			}
			else if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
			{
				this.GetComponent<BowserPlayer>().EmitLandParticles();
			}
		}

		public void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.name == "PipeExit")
			{
				_canWin = false;
			}
		}
	}
}

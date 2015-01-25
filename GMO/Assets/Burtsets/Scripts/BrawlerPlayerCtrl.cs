using UnityEngine;
using System.Collections;

namespace BurtDev {
	public class BrawlerPlayerCtrl : MonoBehaviour {

		public float JumpForce = 20f;
		public float MoveSpeed = 20f;
		public float MaxHorizontalSpeed = 10f;
		public float MaxVerticalSpeed = 20f;
		public float StopThreshold = 0.02f;
		[Range (0f, 1f)] public float StopHorizontalSpeed = 0.8f;
		public float minIdleSpeed = 1f;
		public bool facingRight = true;
		public Animator anim;

		private bool onGround = false;
		private bool lockAnim = false;
		private int IdleAnimID, WalkAnimID, JumpAnimID;
		public bool inControl = true;

		void Start() {
			lockAnim = false;
			onGround = false;
			facingRight = true;
			inControl = true;
			IdleAnimID = Animator.StringToHash("Base Layer.shiba_idle");
			WalkAnimID = Animator.StringToHash("Base Layer.shiba_walk");
			JumpAnimID = Animator.StringToHash("Base Layer.shiba_jump");
		}

		void Update () {
			if (inControl) {
				float xAxis = Input.GetAxis("Horizontal");
				if ((Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.W))) && onGround) {
					rigidbody2D.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);

				}
				rigidbody2D.AddForce(new Vector2(xAxis * MoveSpeed * Time.deltaTime, 0f), ForceMode2D.Impulse);


				if (Mathf.Abs(xAxis) < StopThreshold) {
					rigidbody2D.AddForce(new Vector2(-Mathf.Lerp(rigidbody2D.velocity.x, 0f, 1f - StopHorizontalSpeed), 0f), ForceMode2D.Impulse);
				} 

				if (!Mathf.Approximately(0f, xAxis)) {
					if (xAxis > 0f) {
						turn(true);
					} else {
						turn(false);
					}
				} 
				Vector2 currentSpeed = rigidbody2D.velocity;
				if (currentSpeed.x > 0f) {
					currentSpeed.x = Mathf.Min(currentSpeed.x, MaxHorizontalSpeed);
				} else {
					currentSpeed.x = Mathf.Max(currentSpeed.x, -MaxHorizontalSpeed);
				}
				if (currentSpeed.y > 0f) {
					currentSpeed.y = Mathf.Min(currentSpeed.y, MaxVerticalSpeed);
				} else {
					currentSpeed.y = Mathf.Max(currentSpeed.y, -MaxVerticalSpeed);
				}
				rigidbody2D.velocity = currentSpeed;

				if (!lockAnim) {
					if (Mathf.Abs(rigidbody2D.velocity.x) > minIdleSpeed && onGround) {
						anim.Play(WalkAnimID);
					} else if (!onGround) {
						anim.Play(JumpAnimID);
					} else {
						anim.Play(IdleAnimID);
					}
				}
			}
		}

		void OnCollisionEnter2D(Collision2D _col) {
			if (_col.collider.tag == "Ground") {
				onGround = true;
			}
		}

		void OnCollisionExit2D(Collision2D _col) {
			if (_col.collider.tag == "Ground") {
				onGround = false;
			}
		}

		public void LockAnim(bool _lock) {
			lockAnim = _lock;
		}

		private void turn(bool _right) {
			facingRight = _right;
			Vector3 lScale = transform.localScale;
			if (facingRight) {
				lScale.x = Mathf.Abs(lScale.x);
			} else {
				lScale.x = -Mathf.Abs(lScale.x);
			}
			transform.localScale = lScale;
		}
	}
}

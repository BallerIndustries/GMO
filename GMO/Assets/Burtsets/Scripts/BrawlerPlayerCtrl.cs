using UnityEngine;
using System.Collections;

public class BrawlerPlayerCtrl : MonoBehaviour {

	public float JumpForce = 20f;
	public float MoveSpeed = 20f;
	public float MaxHorizontalSpeed = 10f;
	public float MaxVerticalSpeed = 20f;
	public float StopThreshold = 0.02f;
	[Range (0f, 1f)] public float StopHorizontalSpeed = 0.8f;
	private bool onGround = false;

	void Start() {
		onGround = false;
	}

	void Update () {
//		Debug.Log(Input.GetAxis("Horizontal"));

		if (Input.GetKeyDown(KeyCode.Space) && onGround) {
			rigidbody2D.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
		}
		rigidbody2D.AddForce(new Vector2(Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime, 0f), ForceMode2D.Impulse);


		if (Mathf.Abs(Input.GetAxis("Horizontal")) < StopThreshold) {
			rigidbody2D.AddForce(new Vector2(-Mathf.Lerp(rigidbody2D.velocity.x, 0f, 1f - StopHorizontalSpeed), 0f), ForceMode2D.Impulse);
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
}

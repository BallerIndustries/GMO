using UnityEngine;
using System.Collections;

namespace BurtDev {
	public class BossAI : MonoBehaviour {
		private enum OperationMode {
			Idle,
			Walk,
			RegularAttack,
			DashAttack,
			Jump
		}

		private enum MovementMode {
			Walking,
			Dashing,
			DashingSlow,
			JumpingUp,
			Falling,
			None
		}

		public Transform PlayerTransform;
		public float minIdleTime = 3f;
		public float IdleVariance = 1.5f;
		public float RegularAttackDetectDistance = 2f;
		public float maxHeightForDash = 1f;
		[Range (0f, 1f)] public float JumpChance = 0.5f;
		[Range (0f, 1f)] public float CloseDashChance = 0.3f;
		public Transform LeftLimit, RightLimit;
		public float DashDamageRange = 1.5f;
		public float DashSpeed = 10f;
		public float MoveSpeed = 2f;
		public float DashPauseTime = 0.5f;
		public float DashTime = 1.5f;
		[Range (0f, 1f)] public float DashSlow = 0.4f;
		public float AfterDashTime = 0.5f;
		public float AttackPauseTime = 0.4f;
		public float AttackTime = 0.05f;
		public float AfterAttackTime = 0.5f;
		public float minWalkTime = 1.2f;
		public float WalkTimeVariance = 1.5f;
		public LayerMask PlayerMask;
		public float AttackDistance = 2f;
		public float AttackRadius = 1.5f;

		public float JumpChargeTime = 0.5f;
		public float JumpLandTime = 0.5f;
		[Range (0f, 1f)] public float JumpXSpeed = 0.15f;
		[Range (0f, 1f)] public float JumpYSpeed = 0.3f;
		public float JumpThreshold = 0.001f;
		public Sprite IdleSprite, MoveSprite, AttackChargeSprite, AttackSprite, DashChargeSprite, DashSprite, JumpChargeSprite, JumpSprite, JumpLandSprite;
		public Transform[] JumpPointsGround;
		public Transform[] JumpPointsPlatform;
		public Transform JumpHeightMarker;

		public float StartFallSpeed = 0.05f;
		public float FallMulti = 1.15f;
		public int IdleID, AttackID, ChargeAttackID, MoveID, ChargeDashID, DashID, JumpID, ChargeJumpID, JumpLandID;

		public float deathforce = 10f;

		public AudioController aud;

		private Vector3 JumpTarget;
		private OperationMode mode = OperationMode.Idle;
		private MovementMode mMode = MovementMode.None;
		private bool onPlatform = false;
		private bool moveLeft = true;
		private SpriteRenderer sRender;
		private bool facingLeft = true;
		private float dashCurrentSpeed;
		private float fallSpeed = 0.05f;

		public Animator Anim;

		void Start () {
			sRender = GetComponent<SpriteRenderer>();
			onPlatform = false;
			facingLeft = true;
			StartCoroutine("AttackSequence");
			IdleID = Animator.StringToHash("Base.Idle");
			AttackID = Animator.StringToHash("Base.Attack");
			ChargeAttackID = Animator.StringToHash("Base.AttackCharge");
			MoveID = Animator.StringToHash("Base.Move");
			ChargeDashID = Animator.StringToHash("Base.DashCharge");
			DashID = Animator.StringToHash("Base.Dash");
			JumpID = Animator.StringToHash("Base.Jump");
			ChargeJumpID = Animator.StringToHash("Base.JumpCharge");
			JumpLandID = Animator.StringToHash("Base.JumpLand");
			aud.Play("0477");
		}

		void FixedUpdate () {
//			Debug.Log ("mMode: " + mMode);
			float moveDir = 1f;
			if (moveLeft) moveDir = -1f;
			Vector3 moveV3 = moveDir * transform.right;
			float moveX, moveY;
			switch (mMode) {
			case MovementMode.Walking:
				rigidbody2D.MovePosition(rigidbody2D.position + new Vector2(moveV3.x, moveV3.y) * MoveSpeed * Time.deltaTime);
				break;
			case MovementMode.Dashing:
				rigidbody2D.MovePosition(rigidbody2D.position + new Vector2(moveV3.x, moveV3.y) * DashSpeed * Time.deltaTime);
				if ((PlayerTransform.position - transform.position).magnitude < DashDamageRange) DoDamage();
				dashCurrentSpeed = DashSpeed;
				break;
			case MovementMode.DashingSlow:
				dashCurrentSpeed = Mathf.Lerp(dashCurrentSpeed, 0f, DashSlow);
				rigidbody2D.MovePosition(rigidbody2D.position + new Vector2(moveV3.x, moveV3.y) * dashCurrentSpeed * Time.deltaTime);
				if ((PlayerTransform.position - transform.position).magnitude < DashDamageRange) DoDamage();
				break;
			case MovementMode.JumpingUp:
//				Debug.Log("Jump Target: " + JumpTarget);
				moveX = Mathf.Lerp(rigidbody2D.position.x, JumpTarget.x, JumpXSpeed);
				moveY = Mathf.Lerp(rigidbody2D.position.y, JumpHeightMarker.position.y, JumpYSpeed);
				rigidbody2D.MovePosition(new Vector2(moveX, moveY));
				if (Mathf.Abs(JumpHeightMarker.position.y - rigidbody2D.position.y) < JumpThreshold) {
					fallSpeed = StartFallSpeed;
					mMode = MovementMode.Falling;
					collider2D.enabled = true;
				}
	//			if ((PlayerTransform.position - transform.position).magnitude < DashDamageRange) DoDamage();
				break;
			case MovementMode.Falling:
//				Debug.Log("Jump Target: " + JumpTarget);
				fallSpeed *= FallMulti;
				moveX = Mathf.Lerp(rigidbody2D.position.x, JumpTarget.x, JumpXSpeed);
				moveY = rigidbody2D.position.y - fallSpeed;
				rigidbody2D.MovePosition(new Vector2(moveX, moveY));
//				if ((PlayerTransform.position - transform.position).magnitude < DashDamageRange) DoDamage();
				break;
			default:
				// Idling do nothing
				break;
			}
		}

		private void setFacing() {
			Vector3 lScale = transform.localScale;
			if ((PlayerTransform.position.x - transform.position.x) < 0f) {
				facingLeft = true;
				lScale.x = Mathf.Abs(lScale.x);
			} else {
				facingLeft = false;
				lScale.x = -Mathf.Abs(lScale.x);
			}
			transform.localScale = lScale;
		}

		private void setFacing(bool _left) {
			Vector3 lScale = transform.localScale;
			if (_left) {
				facingLeft = true;
				lScale.x = Mathf.Abs(lScale.x);
			} else {
				facingLeft = false;
				lScale.x = -Mathf.Abs(lScale.x);
			}
			transform.localScale = lScale;
		}

		private IEnumerator AttackSequence() {
			mode = OperationMode.Idle;
			Anim.Play(IdleID);
			collider2D.enabled = true;
			mMode = MovementMode.None;
			setFacing();
			sRender.sprite = IdleSprite;
			yield return new WaitForSeconds(minIdleTime + Random.value * IdleVariance);
			mode = SelectAttack();
			switch (mode) {
			case OperationMode.RegularAttack:
				stopAllSequences();
				StartCoroutine("DoRegularAttackSequence");
				break;
			case OperationMode.DashAttack:
				stopAllSequences();
				StartCoroutine("DoDashAttackSequence");
				break;
			case OperationMode.Jump:
				stopAllSequences();
				StartCoroutine("DoJumpSequence");
				break;
			default:
				if (Mathf.Abs(transform.position.x - LeftLimit.position.x) > Mathf.Abs(transform.position.x - RightLimit.position.x)) { 
					moveLeft = true;
				} else {
					moveLeft = false;
				}
				mMode = MovementMode.Walking;
				Anim.Play(MoveID);
				sRender.sprite = MoveSprite;
				stopAllSequences();
				Invoke("stopWalkSequence", Random.value * WalkTimeVariance + minWalkTime);
				break;
			}
		}

		private void stopWalkSequence() {
			StartCoroutine("AttackSequence");
		}

		private void stopAllSequences() {
			CancelInvoke("stopWalkSequence");
			StopCoroutine("DoRegularAttackSequence");
			StopCoroutine("DoDashAttackSequence");
			StopCoroutine("DoJumpSequence");
			StopCoroutine("JumpLandSequence");
		}

		private IEnumerator DoDashAttackSequence() {
			Debug.Log("Dash mode");
			if (PlayerTransform.position.x - transform.position.x < 0f) {
				moveLeft = true;
			} else {
				moveLeft = false;
			}
			sRender.sprite = DashChargeSprite;
			Anim.Play(ChargeDashID);
			mMode = MovementMode.None;
			yield return new WaitForSeconds(DashPauseTime);
			sRender.sprite = DashSprite;
			Anim.Play(DashID);
			mMode = MovementMode.Dashing;
			yield return new WaitForSeconds(DashTime);
			mMode = MovementMode.DashingSlow;
			yield return new WaitForSeconds(AfterDashTime);
			mMode = MovementMode.None;
			StartCoroutine("AttackSequence");
		}

		private IEnumerator DoRegularAttackSequence() {
			Debug.Log("Attack mode");
			setFacing();
			Anim.Play(ChargeAttackID);
			mMode = MovementMode.None;
			sRender.sprite = AttackChargeSprite;
			yield return new WaitForSeconds(AttackPauseTime);
			Anim.Play(AttackID);
			sRender.sprite = AttackSprite;
			yield return new WaitForSeconds(AttackTime);
			Vector3 dir = transform.right;
			if (facingLeft) {
				dir *= -1;
			}
			if (Physics2D.OverlapCircle(transform.position + dir * AttackDistance, AttackRadius, PlayerMask) != null) {
				DoDamage();
			}

			yield return new WaitForSeconds(AfterAttackTime);
			StartCoroutine("AttackSequence");
		}

		private IEnumerator DoJumpSequence() {
			Debug.Log("Jump mode");
			mMode = MovementMode.None;
			if (onPlatform) {
				JumpTarget = JumpPointsGround[Random.Range(0, JumpPointsGround.Length)].position;
				onPlatform = false;
			} else {
				JumpTarget = JumpPointsPlatform[Random.Range(0, JumpPointsPlatform.Length)].position;
				onPlatform = true;
			}
			setFacing(JumpTarget.x - transform.position.x < 0f);
			Anim.Play(ChargeJumpID);
			sRender.sprite = JumpChargeSprite;
			yield return new WaitForSeconds(JumpChargeTime);
			mMode = MovementMode.JumpingUp;
			Anim.Play(JumpID);
			sRender.sprite = JumpSprite;
			collider2D.enabled = false;
		}

		private IEnumerator JumpLandSequence() {
			mMode = MovementMode.None;
			sRender.sprite = JumpLandSprite;
			yield return new WaitForSeconds(JumpLandTime);
			StartCoroutine("AttackSequence");
		}

		void OnCollisionEnter2D(Collision2D _col) {
			if (_col.collider.tag == "Ground" && mMode == MovementMode.Falling) {
				/**
				if (onPlatform) {
					rigidbody2D.gravityScale = 0f;
				} else {
					rigidbody2D.gravityScale = 1f;
				}
				**/
				Anim.Play(JumpLandID);
				mMode = MovementMode.None;
				if ((PlayerTransform.position - transform.position).magnitude < DashDamageRange) DoDamage();
				StartCoroutine("JumpLandSequence");
			}
		}

		private OperationMode SelectAttack() {
			Vector3 playerDir = PlayerTransform.position - transform.position;
			if (onPlatform) return OperationMode.Jump; 
			if (playerDir.magnitude < RegularAttackDetectDistance && (playerDir.y < maxHeightForDash)) {
				if (Random.value < CloseDashChance) {
					return OperationMode.DashAttack;
				} else {
					return OperationMode.RegularAttack;
				}
			}
			if (playerDir.y < maxHeightForDash) return OperationMode.DashAttack;
			if (Random.value < JumpChance) return OperationMode.Jump;
			return OperationMode.Walk;
		}

		private void DoDamage() {
			// do GameOver WIP
			PlayerTransform.GetComponent<BrawlerPlayerCtrl>().inControl = false;
			PlayerTransform.GetComponent<BrawlerPlayerCtrl>().playMoveSound = false;
			PlayerTransform.collider2D.enabled = false;
			PlayerTransform.rigidbody2D.fixedAngle = false;
			PlayerTransform.rigidbody2D.AddForce(deathforce * Random.insideUnitCircle.normalized, ForceMode2D.Impulse);
			PlayerTransform.rigidbody2D.AddTorque(deathforce);
//			Debug.Log("Player Hit!");
			aud.Play("0477");
			CancelInvoke("doGameOver");
			Invoke ("doGameOver", 1.2f);
		}

		private void doGameOver() {
//			aud.Play("0477");
			GameObject.FindGameObjectWithTag("GameController").GetComponent<MiniGameController>().Lose();
//			Debug.Log("Game Over");
		}
	}
}

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

		private enum ActionMode {
			Idling,
			Walking,
			Dashing,
			Charging,
			Attacking,
			Jumping
		}

		public Transform PlayerTransform;
		public float minIdleTime = 3f;
		public float IdleVariance = 1.5f;
		public float RegularAttackDetectDistance = 2f;
		public float maxHeightForDash = 1f;
		[Range (0f, 1f)] public float JumpChance = 0.5f;
		public Transform LeftLimit, RightLimit;

		public float DashSpeed = 10f;
		public float MoveSpeed = 2f;

		private OperationMode mode = OperationMode.Idle;
		private ActionMode aMode = ActionMode.Idling;
		private bool onPlatform = false;
		private bool moveLeft = true;

		void Start () {
			aMode = ActionMode.Idling;
			StartCoroutine("AttackSequence");
			onPlatform = false;
		}

		void FixedUpdate () {
			float moveDir = 1f;
			if (moveLeft) moveDir = -1f;
			Vector3 moveV3 = moveDir * transform.right;
			
			switch (aMode) {
			case ActionMode.Walking:
				rigidbody2D.MovePosition(rigidbody2D.position + new Vector2(moveV3.x, moveV3.y) * MoveSpeed * Time.deltaTime);
				break;
			case ActionMode.Dashing:
				rigidbody2D.MovePosition(rigidbody2D.position + new Vector2(moveV3.x, moveV3.y) * DashSpeed * Time.deltaTime);
				break;
			default: // Idling
				// do nothing
				break;
			}
		}

		private IEnumerator AttackSequence() {
			mode = OperationMode.Idle;
			yield return new WaitForSeconds(minIdleTime + Random.value * IdleVariance);
			mode = SelectAttack();
			switch (mode) {
			case OperationMode.RegularAttack:
				break;
			case OperationMode.DashAttack:
				break;
			case OperationMode.Jump:
				break;
			default:
				if (Mathf.Abs(transform.position.x - LeftLimit.position.x) > Mathf.Abs(transform.position.x - RightLimit.position.x)) moveLeft = true;
				aMode = ActionMode.Walking;
				break;
			}
		}

		private OperationMode SelectAttack() {
			Vector3 playerDir = PlayerTransform.position - transform.position;
			if (playerDir.magnitude < RegularAttackDetectDistance) return OperationMode.RegularAttack;
			if (playerDir.y < maxHeightForDash) return OperationMode.DashAttack;
			if (Random.value < JumpChance || onPlatform) return OperationMode.Jump;
			return OperationMode.Walk;
		}
	}
}

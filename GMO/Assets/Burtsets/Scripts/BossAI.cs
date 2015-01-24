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

		public Transform PlayerTransform;
		public float minIdleTime = 3f;
		public float IdleVariance = 1.5f;
		public float RegularAttackDetectDistance = 2f;
		public float maxHeightForDash = 1f;
		[Range (0f, 1f)] public float JumpChance = 0.5f;
		private OperationMode mode = OperationMode.Idle;
		private bool onPlatform = false;

		void Start () {
			StartCoroutine("AttackSequence");
			onPlatform = false;
		}

		void Update () {
		
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

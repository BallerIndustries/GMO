using UnityEngine;
using System.Collections;

namespace BurtDev {
	public class BrawlerPlayerAttack : MonoBehaviour {
		private enum AttackType {
			Attack1,
			Attack2,
			Attack3
		}

		public Sprite IdleSprite, Attack1Sprite, Attack2Sprite, Attack3Sprite;
		public LayerMask EnemyMask;
		public float AttackDistance = 1f;
		public float AttackRadius = 2f;
		public float Attack1LockTime = 0.1f;
		public float Attack2LockTime = 0.1f;
		public float Attack3LockTime = 0.2f;
		public float TimeTilResetAttack = 0.1f;
		[Range (0f, 1f)] public float DistToEnemyImpact = 0.5f;
		public GameObject[] ImpactPrefabs;
		public int HitPoints = 8;

		private int Attack1AnimID, Attack2AnimID, Attack3AnimID;
		private SpriteRenderer sRender;
		private BrawlerPlayerCtrl ctrl;
		private bool canAttack = true;
		private AttackType currentAttack = AttackType.Attack1;
		private Animator anim;
		private int currentHP;

		void Start () {
			canAttack = true;
			currentAttack = AttackType.Attack1;
			sRender = GetComponent<SpriteRenderer>();
			ctrl = GetComponent<BrawlerPlayerCtrl>();
			anim = ctrl.anim;
			Attack1AnimID = Animator.StringToHash("Base Layer.shiba_attack1");
			Attack2AnimID = Animator.StringToHash("Base Layer.shiba_attack2");
			Attack3AnimID = Animator.StringToHash("Base Layer.shiba_attack3");
			currentHP = HitPoints;
		}

		void Update () {
			if (Input.GetKeyDown(KeyCode.Space) && canAttack) {
				attack ();
				float nextAttackTime;
				switch (currentAttack) {
				case AttackType.Attack1:
					sRender.sprite = Attack1Sprite;
					anim.Play(Attack1AnimID);
					nextAttackTime = Attack1LockTime;
					currentAttack = AttackType.Attack2;
					canAttack = false;
					break;
				case AttackType.Attack2:
					sRender.sprite = Attack2Sprite;
					anim.Play(Attack2AnimID);
					nextAttackTime = Attack2LockTime;
					currentAttack = AttackType.Attack3;
					canAttack = false;
					break;
				default:
					sRender.sprite = Attack3Sprite;
					anim.Play(Attack3AnimID);
					nextAttackTime = Attack3LockTime;
					currentAttack = AttackType.Attack1;
					canAttack = false;
					break;
				}
				ctrl.LockAnim(true);
				CancelInvoke("enableAttack");
				Invoke("enableAttack", nextAttackTime);
			}
		}

		private void resetAttack() {
			currentAttack = AttackType.Attack1;
			ctrl.LockAnim(false);
		}

		private void enableAttack() {
			sRender.sprite = IdleSprite;
			canAttack = true;
			CancelInvoke("resetAttack");
			Invoke ("resetAttack", TimeTilResetAttack);
		}

		private void attack () {
			Vector3 attackDir;
			if (ctrl.facingRight) {
				attackDir = transform.right;
			} else {
				attackDir = -transform.right;
			}
			Collider2D bossCol = Physics2D.OverlapCircle(attackDir * AttackDistance + transform.position, AttackRadius, EnemyMask);
			if (bossCol != null){
				Vector3 flashloc = Vector3.Lerp(transform.position, bossCol.transform.position, DistToEnemyImpact);
				flashloc.z = -9f;
				Instantiate(ImpactPrefabs[Random.Range(0, ImpactPrefabs.Length)], flashloc, Quaternion.identity);
				doDamageToEnemy();
			}
		}

		private void doDamageToEnemy() {
			currentHP--;
			if (currentHP <= 0) {
//				GameObject.FindGameObjectWithTag("GameController").GetComponent<MiniGameController>().Win();
				Debug.Log("Enemy Dead!");
			}
//			Debug.Log("Hit Enemy!");
		}
	}
}

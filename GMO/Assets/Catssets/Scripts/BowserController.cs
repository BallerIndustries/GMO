using UnityEngine;
using System.Collections;

namespace Cat
{
	public class BowserController : MonoBehaviour
	{
		public Feet Feet;
		public ProjectileSpawner HammerSpawner;
		public int JumpHeight;
		public int JumpWidth;
		public float MinJumpInterval;
		public float MaxJumpInterval;
		public float MinHammerInterval;
		public float MaxHammerInterval;
		public float ThrowHammerTime;
		public int HammerCount;
		public float MinX;
		public float MaxX;

		public void Start()
		{
			Invoke ("Jump", Random.Range (MinJumpInterval, MaxJumpInterval));
			StartCoroutine("StartThrowingHammers");
		}

		private void Jump()
		{
			var jumpX = Random.Range(-JumpWidth, JumpWidth);
			if (this.transform.localPosition.x < MinX)
			{
				jumpX = Mathf.Abs (jumpX);
			}
			else if (this.transform.localPosition.x > MaxX)
			{
				jumpX = -Mathf.Abs (jumpX);
			}
			this.rigidbody2D.AddForce(new Vector2(jumpX, JumpHeight), ForceMode2D.Impulse);
			Invoke ("Jump", Random.Range (MinJumpInterval, MaxJumpInterval));
		}

		private IEnumerator StartThrowingHammers()
		{
			while(true)
			{
				for (int i = 0; i < HammerCount; ++i)
				{
					ThrowHammer ();
					yield return new WaitForSeconds(Random.Range (MinHammerInterval, MaxHammerInterval));
				}
				yield return new WaitForSeconds(ThrowHammerTime);
			}
		}

		private void ThrowHammer()
		{
			HammerSpawner.Fire ();
		}

		public void Die()
		{
			StopCoroutine ("StartThrowingHammers");
		}

		public void OnTriggerStay2D(Collider2D other)
		{
		}
	}
}
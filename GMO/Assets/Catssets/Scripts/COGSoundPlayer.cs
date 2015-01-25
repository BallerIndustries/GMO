using UnityEngine;
using System.Collections;

namespace Cat
{
	[RequireComponent(typeof(AudioSource))]
	public class COGSoundPlayer : MonoBehaviour {
		public void PlaySound()
		{
			this.GetComponent<AudioSource>().Play();
		}
	}
}
using UnityEngine;
using System.Collections;

public class PlayAudioOnStart : MonoBehaviour {

	private AudioSource BGMusic;

	void Start () {
		BGMusic = GetComponent<AudioSource>();
		BGMusic.Play();
	}
	
	public void StopMusic() {
		BGMusic.Stop();
	}
}

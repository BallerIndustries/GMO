using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {

	public MiniGameController GameController;
	public AudioController AudioController;
	public Animator Animator;

	// Use this for initialization
	void Start () {
	
	}

	public void Play()
	{
		Animator.SetTrigger("play");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space))
		{
			Retry();
		}
	}

	private void Retry()
	{
		GameController.StartRandomMiniGame();
		AudioController.Play ("Replay");
		this.gameObject.SetActive (false);
	}
}

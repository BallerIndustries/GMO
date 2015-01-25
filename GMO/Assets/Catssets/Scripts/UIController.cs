using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public AudioController AudioController;
	public TransitionFlashScreen TransitionFlashScreen	;
	public GameOverScreen GameOverScreen;
	public IntroScreen IntroScreen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Flash()
	{
		AudioController.Play ("Flash");
		TransitionFlashScreen.Flash ();
	}

	public void ShowGameOver()
	{
		AudioController.Play ("GameOver");
		GameOverScreen.gameObject.SetActive (true);
		GameOverScreen.Play ();
	}

	public void Intro()
	{
		IntroScreen.gameObject.SetActive (true);
	}
}

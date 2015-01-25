using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public AudioController AudioController;
	public TransitionFlash TransitionFlash;
	public GameOverScreen GameOverScreen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Flash()
	{
		AudioController.Play ("Flash");
		TransitionFlash.Flash ();
	}

	public void ShowGameOver()
	{
		GameOverScreen.gameObject.SetActive (true);
	}
}

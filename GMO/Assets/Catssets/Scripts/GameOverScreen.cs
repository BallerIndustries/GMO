using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {

	public MiniGameController GameController;

	// Use this for initialization
	void Start () {
	
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
		this.gameObject.SetActive (false);
	}
}

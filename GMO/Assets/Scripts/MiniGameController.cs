using UnityEngine;
using System.Collections;

public class MiniGameController : MonoBehaviour {
	public GameObject[] MiniGames;
	public TransitionFlash TransitionFlash;

	private GameObject _activeMiniGame;

	void Start ()
	{
		StartRandomMiniGame ();
	}

	private void StartRandomMiniGame()
	{
		TransitionFlash.Flash ();
		var selectedIndex = Mathf.FloorToInt (Random.value * MiniGames.Length);
		_activeMiniGame = Instantiate(MiniGames[selectedIndex]) as GameObject;
	}

	public void Win()
	{
		Destroy (_activeMiniGame);
		StartRandomMiniGame ();
	}

	public void Lose()
	{
		Destroy (_activeMiniGame);
	}
}

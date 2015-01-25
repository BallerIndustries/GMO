using UnityEngine;
using System.Collections;

public class MiniGameController : MonoBehaviour {
	public GameObject[] MiniGames;
	public UIController UIController;

	private GameObject _activeMiniGame;
	private int _activeIndex;

	void Start ()
	{
		StartRandomMiniGame ();
	}

	public void StartRandomMiniGame()
	{
		UIController.Flash ();
		int selectedIndex;
		do
		{
			selectedIndex = Mathf.FloorToInt (Random.value * MiniGames.Length);
		} while (selectedIndex == _activeIndex);
		_activeIndex = selectedIndex;
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
		UIController.ShowGameOver ();
	}
}

using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	public COGListener Shiba;
	public UIController UIController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shock()
	{
		Shiba.Shocked ();
	}

	public void Walk()
	{
		Shiba.WalkHappy();
	}

	public void Idle()
	{
		Shiba.Idle ();
	}

	public void WalkAngry()
	{
		Shiba.Walk ();
	}

	public void IdleAngry()
	{
		Shiba.IdleAngry ();
	}

	public void ShowUI()
	{
		UIController.Intro();
	}
}

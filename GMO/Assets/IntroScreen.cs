using UnityEngine;
using System.Collections;

public class IntroScreen : MonoBehaviour {

	public MiniGameController GameController;
	public UI2DSprite Arrow;
	public Transform Fight;
	public Transform Ignore;
	public Intro Intro;

	private bool _isFight;

	// Use this for initialization
	void Start () {
		Arrow.transform.localPosition = Fight.localPosition;
		_isFight = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_isFight)
			{
				GameController.StartRandomMiniGame();
				this.gameObject.SetActive (false);
				Destroy (Intro.gameObject);
			}
			else
			{
				Application.Quit ();
			}
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow) && _isFight)
		{
			Arrow.transform.localPosition = Ignore.transform.localPosition;
			_isFight = false;
		}
		else if (Input.GetKeyDown (KeyCode.UpArrow) && !_isFight)
		{
			Arrow.transform.localPosition = Fight.transform.localPosition;
			_isFight = true;
		}
	}
}

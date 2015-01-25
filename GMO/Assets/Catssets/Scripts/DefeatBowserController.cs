using UnityEngine;
using System.Collections;

namespace Cat
{
	public class DefeatBowserController : MonoBehaviour {
		public BowserPlayer Player;
		public BowserController Bowser;
		public BowserCameraController CameraController;
		public BowserBridge Bridge;

		// Use this for initialization
		public void Start () {
			CameraController.FollowPlayer = true;
		}
		
		// Update is called once per frame
		public void Update () {
		
		}

		public void Win()
		{
			// TODO]
			GameObject.FindGameObjectWithTag("GameController").GetComponent<MiniGameController>().Win();
		}

		public void Lose()
		{
			Player.Die ();
			GameObject.FindGameObjectWithTag("GameController").GetComponent<MiniGameController>().Lose();
		}

		public void OpenBridge()
		{
			Bridge.Open ();
			Bowser.Die();
		}
	}
}
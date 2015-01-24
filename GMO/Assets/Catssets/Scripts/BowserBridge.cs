using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BowserBridge : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Open()
	{
		this.transform.DOScaleX(0, 2).SetUpdate (true).OnComplete (() => 
		 	{
				Time.timeScale = 1;
			});
	}
}

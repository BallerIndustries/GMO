using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class BowserBridge : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// LOL MAKES NO SENSE
	public void Open(Action callback)
	{
		this.transform.DOScaleX(0, 2).SetUpdate (true).OnComplete (() => 
		 	{
				Time.timeScale = 1;
				callback();
			});
	}
}

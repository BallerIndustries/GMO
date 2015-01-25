using UnityEngine;
using System.Collections;

public class COGListener : MonoBehaviour {

	public Animator Animator { get; set; }
	// Use this for initialization
	void Start () {
		Animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shocked()
	{
		Animator.SetTrigger ("shock");
	}

	public void Walk()
	{
		Animator.SetTrigger ("walk");
	}

	public void Idle()
	{
		Animator.SetTrigger ("idle");
	}

	public void IdleAngry()
	{
		Animator.SetTrigger ("idle_angry");
	}

	public void WalkHappy()
	{
		Animator.SetTrigger ("walk_happy");
	}
}

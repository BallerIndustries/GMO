using UnityEngine;
using System.Collections;

public class ImpactStarScript : MonoBehaviour {

	public float SelfDestroyTime = 1f;
	
	void Start () {
		Invoke("destroySelf", SelfDestroyTime);
		GetComponent<SpriteRenderer>().sharedMaterial.color = new Color(Random.value, Random.value, Random.value);
	}

	void destroySelf() {
		Destroy(gameObject);
	}
}

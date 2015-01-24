using UnityEngine;
using System.Collections;

namespace Cat
	{
	public class ProjectileSpawner : MonoBehaviour {

		public GameObject Projectile;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void Fire()
		{
			var projectile = GameObject.Instantiate(Projectile) as GameObject;
			projectile.transform.parent = this.transform.parent.parent;
			projectile.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, projectile.transform.position.z);
		}
	}
}
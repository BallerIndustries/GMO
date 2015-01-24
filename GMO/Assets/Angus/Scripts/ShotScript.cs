using UnityEngine;
using System.Collections;

namespace Angus
{
    public class ShotScript : MonoBehaviour
    {
        public int damage;
        public bool isEnemyShot;

        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, 20);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
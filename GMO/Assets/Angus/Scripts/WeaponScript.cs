using UnityEngine;
using System.Collections;

namespace Angus
{
    public class WeaponScript : MonoBehaviour
    {
        public Transform shotPrefab;
        public float shootingRate = 0.25f;
        private float shootCooldown;
        
        void Start()
        {
            shootCooldown = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (shootCooldown > 0)
            {
                shootCooldown -= Time.deltaTime;
            }
        }

        public void Attack(bool isEnemy, Vector3 offset = new Vector3())
        {
            if (CanAttack)
            {
                shootCooldown = shootingRate;

                AudioKing.Instance.PlayAudio("clatter");

                var shotTransform = Instantiate(shotPrefab) as Transform;
                shotTransform.position = transform.position + offset;

                ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
                if (shot != null)
                {
                    shot.isEnemyShot = isEnemy;
                }

                MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
                if (move != null)
                {
                    move.direction = this.transform.right;
                }
            }
        }

        public bool CanAttack
        {
            get
            {
                return shootCooldown <= 0f;
            }
        }
    }
}
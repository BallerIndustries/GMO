using UnityEngine;
using System.Collections;

namespace Angus
{
    public class HealthScript : MonoBehaviour
    {
        public int hp = 1;
        public bool isEnemy = true;
        private SpriteRenderer spriteRenderer;

        private int showRed;
        private const int RED_DURATION = 1;

        void Start()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void Update()
        {
            if (showRed > 0)
            {
                spriteRenderer.color = Color.red;
                showRed--;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }

        }

        public void Damage(int damageCount)
        {
            hp -= damageCount;

            if (hp <= 0)
            {
                if (isEnemy)
                    Globals.BARON_DEAD = true;
                else
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<MiniGameController>().Lose();

                Destroy(gameObject);
            }
            else
            {
                AudioKing.Instance.PlayAudio("hit");
            }
        }

        void OnTriggerEnter2D(Collider2D otherCollider)
        {
            ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();

            if (shot != null)
            {
                if (shot.isEnemyShot != isEnemy)
                {
                    Damage(shot.damage);
                    Destroy(shot.gameObject);
                    showRed = RED_DURATION;
                }
            }
        }
    }
}
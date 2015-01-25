using UnityEngine;
using System.Collections;

namespace Angus
{
    public class SceneController : MonoBehaviour
    {
        public Transform baronPrefab;

        private bool stageClearPlayed = false;
        private int holds = 0;
        private const int DELAY_AMOUNT = 240;

        void Awake()
        {
            Globals.BARON_DEAD = false;
            Globals.STOP_CAMERA = false;
            Camera.main.transform.position = new Vector3(0, 0, -10);
        }

        void Update()
        {
            if (Camera.main.transform.position.x > 13 && Globals.STOP_CAMERA == false)
            {
                //1. Stop the camera
                Globals.STOP_CAMERA = true;
                
                //2. Instantiate a Baron.
                var baronTransform = Instantiate(baronPrefab) as Transform;
                baronTransform.position = new Vector3(23, 0, 0);
            }


            if (Globals.BARON_DEAD)
            {
                if (!stageClearPlayed)
                {
                    stageClearPlayed = true;
                    AudioKing.Instance.PlayAudio("stage_clear");
                }

                holds++;

                if (holds > DELAY_AMOUNT)
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<MiniGameController>().Win();
                }
            }

        }
    }
}
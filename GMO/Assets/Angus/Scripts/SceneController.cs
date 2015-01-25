using UnityEngine;
using System.Collections;

namespace Angus
{
    public class SceneController : MonoBehaviour
    {
        public Transform baronPrefab;
        private AudioSource stageClear;

        private bool stageClearPlayed = false;

        void Awake()
        {
            stageClear = GetComponent<AudioSource>();
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

            if (!stageClearPlayed && Globals.BARON_DEAD)
            {
                stageClearPlayed = true;
                stageClear.Play();
            }
        }

        void FixedUpdate()
        {   
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Angus
{
	public class AudioKing : MonoBehaviour
	{
        private AudioSource[] allAudio;

        private static AudioKing _Instance;
        public static AudioKing Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = GameObject.FindObjectOfType<AudioKing>();

                return _Instance;
            }
        }

        void Awake()
        {
            allAudio = GetComponents<AudioSource>();
        }

        public void PlayAudio(string filename)
        {   
            foreach (var audio in allAudio)
            {
                if (audio.clip.name == filename)
                    audio.Play();
            }
        }
	}
}

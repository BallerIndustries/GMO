using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

	public AudioSource[] Sounds;

	private Dictionary<string, AudioSource> _soundDictionary;
	public Dictionary<string, AudioSource> SoundDictionary
	{
		get
		{
			if (_soundDictionary == null)
			{
				_soundDictionary = new Dictionary<string, AudioSource>();
				foreach (var audioSource in Sounds)
				{
					_soundDictionary.Add(audioSource.gameObject.name, audioSource);
				}
			}
			return _soundDictionary;
		}
	}

	public void Start()
	{

	}

	public void Play(string soundName)
	{
		if (SoundDictionary.ContainsKey (soundName))
		{
			SoundDictionary[soundName].Play ();
		}
		else
		{
			Debug.Log ("Cannot play sound " + soundName);
		}
	}

	public void Stop(string soundName)
	{
		if (SoundDictionary.ContainsKey (soundName))
		{
			SoundDictionary[soundName].Stop ();
		}
		else
		{
			Debug.Log ("Cannot stop sound " + soundName);
		}
	}
}
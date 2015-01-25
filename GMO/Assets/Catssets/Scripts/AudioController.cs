using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

	public AudioSource[] Sounds;

	private Dictionary<string, AudioSource> _soundDictionary;

	public void Start()
	{
		_soundDictionary = new Dictionary<string, AudioSource>();
		foreach (var audioSource in Sounds)
		{
			_soundDictionary.Add(audioSource.gameObject.name, audioSource);
		}
	}

	public void Play(string soundName)
	{
		if (_soundDictionary.ContainsKey (soundName))
		{
			_soundDictionary[soundName].Play ();
		}
		else
		{
			Debug.Log ("Cannot play sound " + soundName);
		}
	}

	public void Stop(string soundName)
	{
		if (_soundDictionary.ContainsKey (soundName))
		{
			_soundDictionary[soundName].Stop ();
		}
		else
		{
			Debug.Log ("Cannot stop sound " + soundName);
		}
	}
}
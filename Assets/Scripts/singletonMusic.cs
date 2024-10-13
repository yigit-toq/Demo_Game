using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class singletonMusic : MonoBehaviour 
{
	private static singletonMusic obje = null;

	public AudioClip darkBeat;
	public AudioClip bestBeat;

	public AudioSource bgAudio { get; set;}

	public int hangiBeat;

	void Awake()
	{
		if (obje == null)
		{
			obje = this;
			DontDestroyOnLoad( this );
		}
		else if (this != obje)
		{
			Destroy( gameObject );
		}

		bgAudio = GetComponent<AudioSource> ();

		hangiBeat = 0;
	}

	void Update ()
	{
		if (sahneGecis.ornek.level == 1)
		{
			if(hangiBeat != 2)
			{
				hangiBeat = 1;
			}
		}

		if (sahneGecis.ornek.level == 5)
		{
			if( hangiBeat != 4)
			{
				hangiBeat = 3;
			}
		}

		if (hangiBeat == 1)
		{
			bgAudio.clip = darkBeat;
			bgAudio.Play ();
			hangiBeat = 2;
		}

		if (hangiBeat == 3)
		{
			bgAudio.clip = bestBeat;
			bgAudio.Play ();
			hangiBeat = 4;
		}
	}
}
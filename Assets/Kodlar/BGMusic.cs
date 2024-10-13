using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMusic : MonoBehaviour {

	public static GameObject ornek;

	public int hangiSahne;

	private AudioSource musicAudio;

	public AudioSource music1;
	public AudioSource music2;
	public AudioSource music3;

	void Awake () 
	{
		musicAudio = GetComponent<AudioSource> ();

		Tek_Yap ();
	}

	void Tek_Yap ()
	{
		if (ornek == null) 
		{
			ornek = gameObject;
			DontDestroyOnLoad (ornek);
		}
		else 
		{
			Destroy (gameObject);
		}
	}

	public void Music1 ()
	{
		musicAudio.clip = music1.clip;
		musicAudio.Play ();
	}

	public void Music2 ()
	{
		musicAudio.clip = music2.clip;
		musicAudio.Play ();
	}

	public void Music3 ()
	{
		musicAudio.clip = music3.clip;
		musicAudio.Play ();
	}
}

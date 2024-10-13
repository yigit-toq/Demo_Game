using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public static GameObject ornek;
	public static BGMusic bgMusic;

	public GameObject uıKod;

	void Awake () 
	{
		Tek_Yap ();

		bgMusic = FindObjectOfType<BGMusic> ();
	}

	void Update ()
	{
		if (bgMusic.hangiSahne == 0)
		{
			uıKod.SetActive (true);
		}
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
}

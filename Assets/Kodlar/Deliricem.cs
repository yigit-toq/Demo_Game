using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deliricem : MonoBehaviour {

	public static BGMusic bgMusic; 

	void Start ()
	{
		bgMusic = FindObjectOfType<BGMusic> ();

		bgMusic.hangiSahne = 0;
	}
}

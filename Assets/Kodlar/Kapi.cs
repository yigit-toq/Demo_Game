using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kapi : MonoBehaviour {

	public GameObject AnahtarGörsel;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && AnahtarGörsel.activeSelf) 
		{
			other.gameObject.SetActive (false);

			SceneManager.LoadScene (4);
		}
	}
}

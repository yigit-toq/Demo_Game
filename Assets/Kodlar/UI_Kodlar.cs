using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UI_Kodlar : MonoBehaviour {

	public static BGMusic bgMusic;

	public GameObject ayarlarEkrani;

	public Text kaliteText;

	private bool ayarlar;

	void Start ()
	{
		bgMusic = FindObjectOfType<BGMusic> ();

		QualitySettings.SetQualityLevel (3);
		kaliteText.text = "QUALiTY: MEDIUM";
	}

	void Update ()
	{
		if (bgMusic.hangiSahne != 0) 
		{
			gameObject.SetActive (false);
			ayarlarEkrani.SetActive (false);
		}
	}

	public void Play ()
	{
		SceneManager.LoadScene (2);
	}

	public void Settings ()
	{
		if (!ayarlar) 
		{
			ayarlar = true;
			ayarlarEkrani.SetActive (true);
		}
		else 
		{
			ayarlar = false;
			ayarlarEkrani.SetActive (false);
		}
	}

	public void Low ()
	{
		QualitySettings.SetQualityLevel (1);
		kaliteText.text = "QUALiTY: LOW";
	}

	public void Medium ()
	{
		QualitySettings.SetQualityLevel (3);
		kaliteText.text = "QUALiTY: MEDIUM";
	}

	public void High ()
	{
		QualitySettings.SetQualityLevel (6);
		kaliteText.text = "QUALiTY: HIGH";
	}

}

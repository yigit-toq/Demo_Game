using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sahneGecis : MonoBehaviour {

	public static sahneGecis ornek;

	[SerializeField]
	private GameObject fadePanel;

	[SerializeField]
	private Animator fadeAnim;

	public int level;

	void Awake() 
	{
		TekYap ();
	}

	void TekYap() 
	{
		if (ornek != null) {
			Destroy (gameObject);
		}
		else
		{
			ornek = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	public void LoadLevel(int bolum){
		StartCoroutine (FadeInOut (bolum));
	}

	IEnumerator FadeInOut(int bolum)
	{
		fadePanel.SetActive (true);
		SceneManager.LoadScene (bolum);
		fadeAnim.Play ("FadeIn");
		yield return new WaitForSeconds (1f);
		fadeAnim.Play ("FadeOut");
		yield return new WaitForSeconds (0.7f);
		fadePanel.SetActive (false);
	}
}

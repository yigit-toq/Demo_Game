using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Loading : MonoBehaviour {

	public Slider loadingBar;

	public void Start ()
	{
		StartCoroutine (Baslat());
	}

	public void LoadLevel (int level)
	{
		StartCoroutine (StartLoading(level));
	}

	IEnumerator StartLoading (int level)
	{
		AsyncOperation async = SceneManager.LoadSceneAsync (level);

		while (!async.isDone)
		{
			loadingBar.value = async.progress;
			yield return null;
		}
	}

	IEnumerator Baslat ()
	{
		yield return new WaitForSeconds (1);
		LoadLevel (2);
	}
}

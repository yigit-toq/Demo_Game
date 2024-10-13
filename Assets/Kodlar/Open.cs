using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Open : MonoBehaviour {

	public GameObject Loading;
	public Slider LoadingSlider;

	void Start ()
	{
		Loading.SetActive (false);
	}

	IEnumerator startLoading ()
	{
		Loading.SetActive (true);

		AsyncOperation async = SceneManager.LoadSceneAsync(1);

		while (!async.isDone) 
		{
			LoadingSlider.value = async.progress;
			yield return null; 
		}
	}
}

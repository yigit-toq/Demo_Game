using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LoadingBar : MonoBehaviour {

	private static LoadingBar loadinOrn;
	public static LoadingBar loadingCode {
		get{
			if(loadinOrn == null){
				loadinOrn = FindObjectOfType<LoadingBar> ();
			}
			return loadinOrn;
		}
	}

	public Slider progressBar;

	void Update ()
	{
		progressBar.value += 0.05f;

		if (sahneGecis.ornek.level == 0 && progressBar.value >= 1)
		{
			sahneGecis.ornek.LoadLevel (1);
			sahneGecis.ornek.level = 1;
		}

		if (sahneGecis.ornek.level == 2 && progressBar.value >= 1)
		{
			sahneGecis.ornek.LoadLevel (2);
			sahneGecis.ornek.level = 3;
		}

		if (sahneGecis.ornek.level == 4 && progressBar.value >= 1)
		{
			sahneGecis.ornek.LoadLevel (3);
			sahneGecis.ornek.level = 5;
		}

		if(sahneGecis.ornek.level == 6 && progressBar.value >= 1)
		{
			sahneGecis.ornek.LoadLevel (5);
			sahneGecis.ornek.level = 7;
		}

		if (sahneGecis.ornek.level == 8 && progressBar.value >= 1)
		{
			sahneGecis.ornek.LoadLevel (6);
			sahneGecis.ornek.level = 9;
		}
	}
}

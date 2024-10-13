using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class enter : MonoBehaviour 
{

	void Start ()
	{
		sahneGecis.ornek.level = 0;
	}

	public void Baslat ()
	{
		sahneGecis.ornek.LoadLevel (7);
	}

}

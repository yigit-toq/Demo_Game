using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hayalet : MonoBehaviour {

	private Animator ghostanimator;
	public GameObject GhostText1;
	public GameObject GhostText2;
	public GameObject GhostText3;
	public GameObject CharacterText1;
	public GameObject CharacterText2;
	public GameObject sohbet_Kutusu;
	public GameObject DuvarKapiObje;

	void Start()
	{
		ghostanimator = GetComponent<Animator> ();
	}
	void Update(){
		if(this.ghostanimator.GetCurrentAnimatorStateInfo(0).IsTag("bitiş")){
			gameObject.SetActive (false);
		}
		if (Karakter.PlayerCode.sohbetButon_Basildi) {
			sohbet_Kutusu.gameObject.SetActive (true);
			StartCoroutine (Diyalog ());
			Karakter.PlayerCode.sohbetButon_Basildi = false;
		}
	}
	IEnumerator Diyalog()
	{
		Karakter.PlayerCode.konusma_basladi = true;
		GhostText1.SetActive (true);
		yield return new WaitForSeconds (2);
		GhostText1.SetActive (false);
		CharacterText1.SetActive (true);
		yield return new WaitForSeconds (2);
		CharacterText1.SetActive (false);
		GhostText2.SetActive (true);
		yield return new WaitForSeconds (2);
		GhostText2.SetActive (false);
		CharacterText2.SetActive (true);
		yield return new WaitForSeconds (2);
		CharacterText2.SetActive (false);
		ghostanimator.SetTrigger ("Korkut");
		GhostText3.SetActive (true);
		yield return new WaitForSeconds (2);
		GhostText3.SetActive (false);
		ghostanimator.SetTrigger ("Gidiş");
		DuvarKapiObje.SetActive (false);
		sohbet_Kutusu.SetActive (false);
		Karakter.PlayerCode.konusma_basladi = false;
	}
}

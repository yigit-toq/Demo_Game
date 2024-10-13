using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tüccar : MonoBehaviour {

	public static Tüccar tuccarKod;

	public GameObject Sohbet_Kutusu;
	public GameObject TüccarKonuşma1;
	public GameObject KarakterKonuşma1;
	public GameObject TüccarKonuşma2;
	public GameObject KarakterKonuşma2;
	public GameObject TüccarKonuşma3;
	public float hiz;
	public bool koş;
	private Animator myAnimator;

	void Start () {
		myAnimator = GetComponent<Animator> ();
		koş = false;
		hiz = 3;
	}

	void Update () {
		if (koş) {
			transform.position += new Vector3 (hiz * Time.deltaTime, 0, 0);
			myAnimator.SetBool ("yürü", true);
		}
		else
		{
			myAnimator.SetBool ("yürü", false);
		}
		if(Karakter2.PlayerCode2.sohbetButon_Basildi){
			Sohbet_Kutusu.SetActive (true);
			StartCoroutine (Konusma_Zamani());
			Karakter2.PlayerCode2.sohbetButon_Basildi = false;
		}
	}
	IEnumerator Konusma_Zamani(){
		TüccarKonuşma1.SetActive (true);
		yield return new WaitForSeconds (3);
		TüccarKonuşma1.SetActive (false);
		KarakterKonuşma1.SetActive (true);
		yield return new WaitForSeconds (3);
		KarakterKonuşma1.SetActive (false);
		TüccarKonuşma2.SetActive (true);
		yield return new WaitForSeconds (3);
		TüccarKonuşma2.SetActive (false);
		KarakterKonuşma2.SetActive (true);
		yield return new WaitForSeconds (3);
		KarakterKonuşma2.SetActive (false);
		TüccarKonuşma3.SetActive (true);
		yield return new WaitForSeconds (3);
		TüccarKonuşma3.SetActive (false);
		Sohbet_Kutusu.SetActive (false);
		transform.localScale = new Vector3 (transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
		koş = true;
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "tüccarKapi"){
			koş = false;
			StartCoroutine (dükkanaGir());
		}
	}
	IEnumerator dükkanaGir(){
		yield return new WaitForSeconds (2);
		gameObject.SetActive (false);
	}
}

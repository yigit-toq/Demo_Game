using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HareketliZemin : MonoBehaviour {

	private bool Hareket;
	public Animator HzeminAnim;

	void Start () {
		Hareket = true;
	}
	void Update () {
		if (Hareket) {
			HzeminAnim.GetComponent<Animator> ().enabled = true;
		} else {
			HzeminAnim.GetComponent<Animator> ().enabled = false;
		}
	}
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			Hareket = false;
		}
	}
	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			Hareket = true;
		}
	}
}

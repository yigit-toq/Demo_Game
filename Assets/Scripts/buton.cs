using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buton : MonoBehaviour {


	private Animator myAnim;
	public Animator kapiAnim;

	void Start () {
		myAnim = GetComponent<Animator> ();
	}

	void Update () {
		
	}
	void OnCollisionEnter2D (Collision2D other) {
		if(other.gameObject.tag == "top"){
			myAnim.SetTrigger ("bas");
			StartCoroutine (kapiAcil());
		}
	}
	IEnumerator kapiAcil(){
		yield return new WaitForSeconds (2);
		kapiAnim.SetTrigger ("ac");
	}
}

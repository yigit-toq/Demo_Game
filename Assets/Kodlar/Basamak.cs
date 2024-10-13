using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basamak : MonoBehaviour {

	public BoxCollider2D karakterCollider;
	public BoxCollider2D myCollider;
	public BoxCollider2D myTrigger;

	void Start () {
		karakterCollider = GameObject.Find ("Karakter").GetComponent<BoxCollider2D> ();
		Physics2D.IgnoreCollision (myCollider,myTrigger,true);
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			Physics2D.IgnoreCollision (myCollider,karakterCollider,true);
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			Physics2D.IgnoreCollision (myCollider,karakterCollider,false);
		}
	}
}

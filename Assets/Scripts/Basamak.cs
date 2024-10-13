using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basamak : MonoBehaviour {

	private BoxCollider2D KarakterCollider;
	public BoxCollider2D BasamakCollider;
	public BoxCollider2D BasamakTrigger;

	void Start () {
		KarakterCollider = GameObject.Find("Karakter").GetComponent<BoxCollider2D>();
		Physics2D.IgnoreCollision (BasamakCollider, BasamakTrigger, true);
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name == "Karakter"){
			Physics2D.IgnoreCollision (BasamakCollider,KarakterCollider,true);
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.name == "Karakter"){
			Physics2D.IgnoreCollision (BasamakCollider,KarakterCollider,false);
		}
	}
}

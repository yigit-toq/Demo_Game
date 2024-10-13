using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kasap_Necmi2 : MonoBehaviour {

	public float hiz;
	private bool yürü;
	private Animator kasapAnim;

	void Start () {
		yürü = false;
		StartCoroutine (Kasap_Yürü());
		kasapAnim = GetComponent<Animator> ();
	}
	void FixedUpdate(){
		if (yürü) {
			kasapAnim.SetBool ("yürü",true);
			transform.position += new Vector3 (hiz * Time.deltaTime,0,0);
		} 
		else
		{
			kasapAnim.SetBool ("yürü",false);
		}
	}
	IEnumerator Kasap_Yürü(){
		yield return new WaitForSeconds (2);
		yürü = true;
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "kasap"){
			yürü = false;
		}
	}
}

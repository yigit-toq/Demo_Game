using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kadın : MonoBehaviour {

	private bool yürü;
	public float hiz;
	private Animator kadınAnim;

	void Start () {
		kadınAnim = GetComponent<Animator> ();
		yürü = true;
	}
	void Update () {
		if (yürü) {
			kadınAnim.SetBool ("yürü", true);
			transform.position += new Vector3 (hiz * Time.deltaTime, 0, 0);
		} 
		else
		{
			kadınAnim.SetBool ("yürü", false);
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "kadınEv"){
			StartCoroutine (Yon_Degistir());
		}
		if(other.gameObject.tag == "vagon"){
			StartCoroutine (Yon_Degistir2());
		}
	}
	IEnumerator Yon_Degistir (){
		yürü = false;
		yield return new WaitForSeconds (5);
		transform.localScale = new Vector3 (transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
		yield return new WaitForSeconds (5);
		hiz = 2;
		yürü = true;
	}
	IEnumerator Yon_Degistir2 (){
		yürü = false;
		yield return new WaitForSeconds (5);
		transform.localScale = new Vector3 (transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
		yield return new WaitForSeconds (5);
		hiz = -2;
		yürü = true;
	}
}

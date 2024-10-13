using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dayi : MonoBehaviour {

	private static Dayi dayi;
	public static Dayi dayiCode{
		get{
			if(dayi == null){
				dayi = GameObject.FindObjectOfType<Dayi> ();
			}
			return dayi;
		}	
	}
	public float hiz;
	public bool yürü;
	private Vector3 yon;
	public Transform rayCast;
	private RaycastHit2D hit;
	private Animator yasliAnim;
	public LayerMask rayCastMask;

	void Start () {
		yürü = false;
		yasliAnim = GetComponent<Animator> ();
		yasliAnim.speed = 1;
	}

	void Update () {
		hit = Physics2D.Raycast (rayCast.position,Vector2.right,3,rayCastMask);
		if (yürü) {
			yasliAnim.SetBool ("yürü",true);
			transform.position += new Vector3 (hiz * Time.deltaTime,0,0);
		}
		else
		{
			yasliAnim.SetBool ("yürü",false);	
		}
		if(hit.collider != null){
			Vector3 yon = transform.eulerAngles;
			yon.y = 180f;
			transform.eulerAngles = yon;
			yasliAnim.speed = 2.5f;
			hiz = -5;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "yasliDur"){
			yürü = false;
			yasliAnim.speed = 1;
			StartCoroutine(içeri_Gir ());
		}
	}
	IEnumerator içeri_Gir(){
		yield return new WaitForSeconds (2);
		gameObject.SetActive (false);
	}
}

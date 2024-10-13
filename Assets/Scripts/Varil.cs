using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Varil : MonoBehaviour {

	public float can;
	public GameObject varil;
	private Rigidbody2D rigid;
	private Animator anim;

	void Start () {
		can = 100;
		varil.SetActive (true);
		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	void Update () {
		if(can <= 0){
			varil.SetActive (false);
		}
		if(!varil.activeSelf){
			rigid.gravityScale = 0;
			rigid.mass = 0;
		}
	}
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			if(Karakter4.PlayerCode4.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("atak") || Karakter4.PlayerCode4.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("ZiplaAtak") || Karakter4.PlayerCode4.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("EğilipAtak")){
				transform.position += new Vector3 (0,35 * Time.deltaTime,0);
				can = can - 25;
			}
		}
	}
}

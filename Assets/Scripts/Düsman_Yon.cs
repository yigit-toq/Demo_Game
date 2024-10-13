using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Düsman_Yon : MonoBehaviour {

	public LayerMask Layer;
	public Transform Kurukafa;
	private Düsman KurukafaHareket;

	void Start () {
		KurukafaHareket = Kurukafa.GetComponent<Düsman> ();
		Kurukafa = transform.parent;
	}

	void Update () {
		RaycastHit2D hit = Physics2D.Raycast (transform.position,Vector2.down,1);
		if(hit.collider == null){
			Kurukafa.localScale = new Vector3 (Kurukafa.localScale.x * -1,Kurukafa.localScale.y,Kurukafa.localScale.z);
			KurukafaHareket.hiz *= -1;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour {

	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;

	private Transform Hedef;

	void Start () {
		Hedef = GameObject.Find("Karakter").transform;
	}
	void LateUpdate () {
		transform.position = new Vector2 (Mathf.Clamp(Hedef.position.x,xMin,xMax),Mathf.Clamp(Hedef.position.y,yMin,yMax));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour {

	public Transform Karakter;

	public float xMax;
	public float xMin;
	public float yMax;
	public float yMin;

	void Start () {
		Karakter = GameObject.Find ("Karakter").transform;
	}

	void LateUpdate () {
		transform.position = new Vector2 (Mathf.Clamp(Karakter.position.x,xMin,xMax),Mathf.Clamp(Karakter.position.y,yMin,yMax));
		
	}
}

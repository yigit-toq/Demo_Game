using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {

	private static Aim aimGet;
	public static Aim aimCode {
		get{
			if(aimGet == null){
				aimGet = GameObject.FindObjectOfType<Aim> ();
			}
			return aimGet;
		}
	}
	
	[Header("Nişan")]
	public Vector3 normalAim;
	public Vector3 nişanAim;
	public float aimHiz;
	public bool aim;

	private Camera kamera;

	void Start () {
		kamera = Camera.main;
	}

	void FixedUpdate () {
		Egim_Alma ();
	}
	void Egim_Alma () {
		if (Input.GetMouseButton(1) && !Silah1.gunCode.reload && !Silah1.gunCode.gunAnim.GetCurrentAnimatorStateInfo(0).IsName("run")) {
			transform.localPosition = Vector3.Lerp (transform.localPosition, nişanAim, aimHiz * Time.deltaTime);
			kamera.fieldOfView -= 80 * Time.deltaTime;
			kamera.fieldOfView = Mathf.Clamp(kamera.fieldOfView,40,60);
			aim = true;
		}
		else
		{
			transform.localPosition = Vector3.Lerp (transform.localPosition, normalAim, aimHiz * Time.deltaTime);
			kamera.fieldOfView += 80 * Time.deltaTime;
			kamera.fieldOfView = Mathf.Clamp(kamera.fieldOfView,40,60);
			aim = false;
		}
	}
}

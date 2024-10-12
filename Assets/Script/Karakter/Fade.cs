using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {
	
	private Color color;

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.gameObject.tag == "Fade")
		{
			other.GetComponent<SpriteRenderer> ().material.color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Fade")
		{
			other.GetComponent<SpriteRenderer> ().material.color = new Color (1.0f, 1.0f, 1.0f, 1f);
		}
	}
}

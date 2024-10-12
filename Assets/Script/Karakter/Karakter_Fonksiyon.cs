using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karakter_Fonksiyon : MonoBehaviour {

	public int coin;
	public int gem;

	public Text toplamCoin;
	public Text toplamGem;

	public AudioSource itemSes;

	void Start () {
		gem = 0;
		coin = 0;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Coin") {
			other.gameObject.SetActive (false);
			coin += 100;
			SkorAyarla (coin);
			itemSes.Play ();
		}

		if(other.gameObject.tag == "Gem") {
			other.gameObject.SetActive (false);
			gem += 1;
			SkorAyarla (gem);
			itemSes.Play ();
		}
	}

	void SkorAyarla(int count) {
		toplamCoin.text = coin.ToString ();
		toplamGem.text = gem.ToString ();
	}
}

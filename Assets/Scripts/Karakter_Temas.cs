using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter_Temas : MonoBehaviour
{
    public Oyun_Mekanizmasi oyunMekanizmasi { get; set; }

    void Start()
    {
        oyunMekanizmasi = FindObjectOfType<Oyun_Mekanizmasi>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Baþlangýç" && !oyunMekanizmasi.Basladi)
        {
            oyunMekanizmasi.Basla = true;
        }
    }
}

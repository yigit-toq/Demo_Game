using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter_Temas : MonoBehaviour
{
    public int skor;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            skor += 100;
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Karakter_Hareket.karakter.can -= 1;
        }
    }
}

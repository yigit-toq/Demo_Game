using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    public bool goBullet;

    public int speedBullet;

    private int speed;

    void Start ()
    {
        goBullet = true;

        Vector3 mermiYon = Karakter_Hareket.karakter.transform.localScale;

        transform.Rotate(0, 0, -90);

        if (mermiYon.x > 0)
        {
            speed = speedBullet * 1;
        }
        else
        {
            speed = speedBullet * -1;
        }

        Destroy (gameObject, 3);
    }

    void FixedUpdate ()
    {
        if (goBullet)
        {
            transform.position += new Vector3 (speed, 0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D mermi)
    {
        if (mermi.gameObject.tag == "Düşman")
        {
            Dusman.dusman.Can -= 10;
            Destroy(gameObject);
        }
    }
}

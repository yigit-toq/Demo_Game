using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dusman : MonoBehaviour
{
    private static Dusman dusmanKod;
    public static Dusman dusman
    {
        get
        {
            if (dusmanKod == null)
            {
                dusmanKod = FindObjectOfType<Dusman>();
            }
            return dusmanKod;
        }
    }

    public bool hareket;

    public bool saldir;

    public int dusmanHiz;

    public float Can;

    public float gormeMesafesi;

    private Vector3 dusmanYon;

    private RaycastHit2D biriVarmi;

    public LayerMask karakterKatman;

    public Rigidbody2D dusmanRigid { get; set; }

    void Start()
    {
        dusmanRigid = GetComponent<Rigidbody2D>();

        dusmanYon = transform.localScale;

        hareket = true;

        Can = 100;
    }

    void Update()
    {
        if (Can <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    void FixedUpdate()
    {
        if (transform.localScale.x > 0)
        {
            biriVarmi = Physics2D.Raycast(transform.position, Vector2.right, gormeMesafesi, karakterKatman);
        }
        else
        {
            biriVarmi = Physics2D.Raycast(transform.position, Vector2.left, gormeMesafesi, karakterKatman);
        }

        if (biriVarmi.collider != null)
        {
            saldir = true;
        }
        else
        {
            saldir = false;
        }

        if (hareket)
        {
            dusmanRigid.velocity = new Vector2(dusmanHiz * dusmanYon.x, dusmanRigid.velocity.y);
        }
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Sınır")
        {
            dusmanYon = transform.localScale;
            dusmanYon.x *= -1;
            transform.localScale = dusmanYon;
        }
        
    }
}

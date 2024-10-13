using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yumurta : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EditorOnly" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

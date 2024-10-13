using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Karakter_Temas : MonoBehaviour
{

    public int token = 0;

    public Text tokenText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Token") 
        {
            other.gameObject.SetActive(false);
            token += 1;
            Token_Yazdir (token);
        }
    }
    
    void Token_Yazdir (int token)
    {
        tokenText.text = token.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMechanical : MonoBehaviour
{
    public static UI_Kod kodUI;

    public GameObject sacilanPara;

    private GameObject klonObje;

    private Vector2 fareKonum;

    public float elimizdekiCoin;
    public float kazanilanCoin;
    public float verilenCoin;
    public float hedefCoin;

    void Start()
    {
        kodUI = FindObjectOfType<UI_Kod>();

        verilenCoin = 1f;
        hedefCoin = 50f;
    }

    void Update()
    {
        Coin_Kazanma ();

        elimizdekiCoin = kazanilanCoin;
    }

    void Coin_Kazanma ()
    {
        if (Input.GetMouseButtonDown(0) && !kodUI.yükselt)
        {
            kazanilanCoin += verilenCoin;
            fareKonum = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            klonObje = Instantiate(sacilanPara, fareKonum, Quaternion.identity);
            Destroy(klonObje, 2f);
        }
    }
}

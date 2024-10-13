using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UI_Kod : MonoBehaviour
{
    public static GameMechanical gameMec;

    public Text coinText;
    public Text hedefText;
    public Text verilenCoin;

    public bool yükselt;

    public float coinUI;

    public int birler;
    public int onlar;
    public int yüzler;
    public int binler;

    void Start ()
    {
        gameMec = FindObjectOfType<GameMechanical>();

        yükselt = false;

        hedefText.text = "HEDEF" + ":" + gameMec.hedefCoin.ToString();
        verilenCoin.text = "x" + gameMec.verilenCoin.ToString();
    }

    void Update ()
    {
        coinUI = gameMec.elimizdekiCoin;

        coinText.text = coinUI.ToString();
    }

    public void Upgrade_Button ()
    {
        if (coinUI >= gameMec.hedefCoin)
        {
            yükselt = true;
            gameMec.kazanilanCoin -= gameMec.hedefCoin;
            gameMec.verilenCoin = gameMec.verilenCoin * 2;
            gameMec.hedefCoin = gameMec.hedefCoin * 2;
            verilenCoin.text = "x" + gameMec.verilenCoin.ToString();
            hedefText.text = "HEDEF " + ": " + gameMec.hedefCoin.ToString();
        }

        yükselt = false;
    }
}

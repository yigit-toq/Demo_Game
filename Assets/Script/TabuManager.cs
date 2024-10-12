using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TabuManager : MonoBehaviour
{
    private TMPro.TextMeshProUGUI KelimeTMP;
    private TMPro.TextMeshProUGUI TabuTMP;

    public string[] Kelime;
    [Multiline]
    public string[] Tabu;

    private void Awake()
    {
        KelimeTMP = GameObject.Find("Kelime").GetComponent<TMPro.TextMeshProUGUI>();
        TabuTMP = GameObject.Find("Tabu").GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Start()
    {
        Kelimeler_ve_Tabular(Kelime, Tabu);
    }

    private void Kelimeler_ve_Tabular(string[] kelime, string[] tabu)
    {
        KelimeTMP.text = kelime[0];
        TabuTMP.text = tabu[0];
    }

    public void True()
    {

    }

    public void False()
    {

    }

    public void Pass()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float PlayerHeal;

    public float Skor;

    public Slider HealSlider;

    public TMPro.TextMeshProUGUI SkorText;

    public GameObject SkorObject;

    public Transform[] bulletTrans;

    private void Awake()
    {
        SkorText = SkorObject.GetComponent<TMPro.TextMeshProUGUI>();

        PlayerHeal = 100;
        Skor = 0;

        HealSlider.value = PlayerHeal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            PlayerHeal -= Random.Range(2, 5);

            HealSlider.value = PlayerHeal;

            if (PlayerHeal <= 0)
            {
                Application.Quit();
            }

            Destroy(collision.gameObject);
        }
    }
}

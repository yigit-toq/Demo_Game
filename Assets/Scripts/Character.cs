using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour
{
    public ColumnController controller;

    public int JumpPower;
    public int Score;

    private int amnskm;

    public float Gravity;

    public bool IsDead { get; set; }

    public Text skorText;
    public Text finalSkorText;

    public GameObject Tap;
    public GameObject DieScreen;

    public Animator DieScreenAnimator;

    public AudioSource FlySound;
    public AudioSource AmnskmSound;
    public AudioSource BaklavaSound;
    public AudioSource BackgroundSound;
    public Collider2D CharacterCollider { get; set; }
    public Rigidbody2D CharacterRigidbody { get; set; }

    void Start()
    {
        controller = FindObjectOfType<ColumnController>();

        CharacterRigidbody = GetComponent<Rigidbody2D>();
        CharacterCollider = GetComponent<Collider2D>();

        CharacterRigidbody.gravityScale = 0;

        Score = 0;
        amnskm = 0;

        Tap.SetActive(true);
        DieScreen.SetActive(false);

        IsDead = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsDead)
            {
                if (CharacterRigidbody.gravityScale == Gravity)
                {
                    CharacterRigidbody.velocity = Vector2.up * JumpPower;

                    FlySound.Play();
                }
                else
                {
                    Tap.SetActive(false);

                    CharacterRigidbody.gravityScale = Gravity;

                    CharacterRigidbody.velocity = Vector2.up * JumpPower;

                    controller.StartCoroutine(controller.ColumnSpawner(controller.time));

                    FlySound.Play();
                }
            }
        }

        transform.eulerAngles = new Vector3(0, 0, CharacterRigidbody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kolon" || collision.gameObject.tag == "Zemin")
        {
            if (amnskm == 0)
            {
                AmnskmSound.Play();
                amnskm++;
            }

            BackgroundSound.Stop();

            DieScreen.SetActive(true);

            DieScreenAnimator.SetTrigger("Fade");

            finalSkorText.text = Score.ToString();

            IsDead = true;
        }  
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Puan")
        {
            Score++;

            skorText.text = Score.ToString();
        }

        if (collision.gameObject.tag == "Baklava")
        {
            BaklavaSound.Play();

            Destroy(collision.gameObject);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

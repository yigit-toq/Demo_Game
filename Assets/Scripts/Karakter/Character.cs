using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
	private static Character character;
	public static Character characterScript
	{
		get
		{
			if (character == null)
			{
				character = GameObject.FindObjectOfType<Character>();
			}
			return character;
		}
	}

	public Animator CharacterAnimator { get; set; }
	public Rigidbody2D CharacterRigidbody { get; set; }

	public int speed;

	public float ZiplamaKuvveti;

	private float yatay;

	public bool attack;

	public bool die;

	public float heal;

	public float stamina;

	public Slider healSlier;
	public Slider staminaSlier;

	public GameObject gameOver;

	public GameObject dontGrass;

	[Header("Yön Ýþlemleri")]

	private Vector3 rotation;

	private bool lookingRight;

	[Header("Zemin Ýþlemleri")]

	public Transform[] TemasNoktalari;

	public LayerMask HangiZemin;

	public float TemasCapi;

	public bool zeminde;

	public bool havadaKontrol;

	[Header("Raycast Ýþlemleri")]

	public float distance;
	public float range;

	private RaycastHit2D hit;

	public LayerMask hitLayer;

	private Scene mevcutSahne;

	private int sceneNumber;

    private void Awake()
    {
		CharacterAnimator = GetComponent<Animator>();
		CharacterRigidbody = GetComponent<Rigidbody2D>();

		Time.timeScale = 1;
	}

    private void Start()
    {
		mevcutSahne = SceneManager.GetActiveScene();

		sceneNumber = mevcutSahne.buildIndex;

		lookingRight = true;
		die = false;

        if (sceneNumber == 2)
        {
			dontGrass.SetActive(true);
        }
    }

	private void Update()
	{
        if (!die)
        {
			Raycast();

			Kontroller();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && gameOver.activeSelf)
        {
			SceneManager.LoadScene(sceneNumber);
		}

		healSlier.value = heal;
		staminaSlier.value = stamina;

        if (stamina < 100 && !attack)
        {
			stamina += Time.deltaTime * 15;
        }

        if (stamina < 100 && !attack)
        {
			CharacterAnimator.SetBool("Charge", true);
        }
        else
        {
			CharacterAnimator.SetBool("Charge", false);
		}
	}

	private void FixedUpdate()
	{
		yatay = Input.GetAxisRaw("Horizontal");

		if (!die)
        {
			zeminde = Zeminde();

            if (sceneNumber != 2)
            {
				Temel_Hareketler(yatay);

				TurnDirection(yatay);
            }
            else
            {
                if (!dontGrass.activeSelf)
                {
					if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
					{
						transform.position += Vector3.zero;
					}
					else
					{
						transform.position += new Vector3(5 * Time.fixedDeltaTime, 0, 0);
					}
                }
                else
                {
					CharacterRigidbody.velocity = Vector2.zero;
                }          
            }
        }
        else
        {
			CharacterRigidbody.velocity = Vector2.zero;
        }
	}

	private void Kontroller()
    {
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
            if (distance < 12)
            {
				CharacterRigidbody.velocity = new Vector2(0, ZiplamaKuvveti);
			}

            if (sceneNumber == 2)
            {
				dontGrass.SetActive(false);
			}
		}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!attack && stamina >= 100)
            {
				CharacterAnimator.SetTrigger("Attack");

				stamina -= 100;
			}	
        }
	}

	private void Temel_Hareketler(float Yatay)
	{
		CharacterRigidbody.velocity = new Vector2(Yatay * speed, CharacterRigidbody.velocity.y);

        if (zeminde)
        {
			CharacterAnimator.SetFloat("Speed", Mathf.Abs(Yatay));
        }
	}

	private void TurnDirection(float yatay)
	{
		if (yatay > 0 && !lookingRight || yatay < 0 && lookingRight)
		{
            if (!attack)
            {
				lookingRight = !lookingRight;

				rotation = transform.eulerAngles;

				if (lookingRight)
				{
					rotation.y = 0;
				}
				else
				{
					rotation.y = 180;
				}

				transform.eulerAngles = rotation;
			}			
		}
	}

	private bool Zeminde()
	{
		if (CharacterRigidbody.velocity.y <= 0)
		{
			foreach (Transform nokta in TemasNoktalari)
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll(nokta.position, TemasCapi, HangiZemin);
				for (int i = 0; i < colliders.Length; i++)
				{
					if (colliders[i].gameObject != gameObject)
					{
						CharacterAnimator.SetBool("Fly", false);

						return true;
					}
				}
			}
		}
		CharacterAnimator.SetBool("Fly", true);

		return false;	
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (sceneNumber == 2)
            {
				heal = 0;

				CharacterAnimator.SetTrigger("Death");

				die = true;
			}
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
			SceneManager.LoadScene(sceneNumber + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Scythe"))
        {
			heal -= 10;

            if (heal <= 0)
            {
				CharacterAnimator.SetTrigger("Death");

				die = true;
			}
            else 
			{
				CharacterAnimator.SetTrigger("Damage");
			}

			if (lookingRight)
			{
				CharacterRigidbody.position -= new Vector2(3, -2.5f);
			}
			else
			{
				CharacterRigidbody.position += new Vector2(3, 2.5f);
			}
		}
    }

    private void Raycast()
	{
		hit = Physics2D.Raycast(TemasNoktalari[1].position, Vector2.down, range, hitLayer);

		distance = hit.distance;
	}

	public IEnumerator Death()
    {
		yield return new WaitForSeconds(2);

		gameOver.SetActive(true);

		Time.timeScale = 0;
    }

	public void Restart()
    {
		SceneManager.LoadScene(sceneNumber);
    }
}

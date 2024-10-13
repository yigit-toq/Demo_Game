using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player")]

    private CharacterController playerController;
    
    [SerializeField]
    private int playerSpeed;
    
    private float moveX;
    private float moveY;

    [Header("Door")]

    [SerializeField]
    private float raycastLenght;

    private RaycastHit doorHit;

    [Header ("Flashlight")]

    private bool onLantern;

    [SerializeField]
    private Light Flashlight;
    [SerializeField]
    private AudioSource clickSound;

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        onLantern = false;
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

        playerController.Move(new Vector3(moveX, 0, moveY));

        Kontroller();  
    }

    private void Kontroller()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!onLantern)
            {
                clickSound.Play();

                Flashlight.enabled = true;

                onLantern = true; ;
            }
            else
            {
                clickSound.Play();

                Flashlight.enabled = false;

                onLantern = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out doorHit, raycastLenght))
            {
                if (doorHit.transform.CompareTag("Door"))
                {
                    Door DoorScript = doorHit.transform.gameObject.GetComponent<Door>();
                    DoorScript.DoorPlay();
                }
            }
        }
    }
}

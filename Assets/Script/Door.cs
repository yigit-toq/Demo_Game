using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool inOpen;

    public float openRotation, closeRotation, speed;

    private AudioSource DoorAudioSource;

    public AudioClip closeSound;
    public AudioClip openSound;

    private void Start()
    {
        DoorAudioSource = GetComponent<AudioSource>();
    }

    public void DoorPlay()
    {
        inOpen = !inOpen;

        if (inOpen)
        {
            DoorAudioSource.Stop();
            DoorAudioSource.PlayOneShot(openSound);
        }
        else
        {
            DoorAudioSource.Stop();
            DoorAudioSource.PlayOneShot(closeSound);
        }
    }

    private void Update()
    {
        if (inOpen)
        {
            speed = 0.5f;
            Quaternion openDoor = Quaternion.Euler(0, openRotation, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, openDoor, speed * Time.deltaTime);
        }
        else
        {
            speed = 5;
            Quaternion closeDoor = Quaternion.Euler(0, closeRotation, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, closeDoor, speed * Time.deltaTime);
        }
    }
}

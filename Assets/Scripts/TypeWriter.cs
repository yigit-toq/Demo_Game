using System.Collections;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TypeWriter : MonoBehaviour
{

    public float delay;

    [Multiline]
    public string yazi;

    public AudioClip writerSound;

    private AudioSource audioSRC;

    private Text thisText;

    void Start()
    {
        audioSRC = GetComponent<AudioSource>();

        thisText = GetComponent<Text>();

        StartCoroutine(TypeWrite());
    }

    IEnumerator TypeWrite()
    {
        foreach (char i in yazi)
        {
            thisText.text += i.ToString();

            audioSRC.pitch = Random.Range(0.8f, 1.2f);

            audioSRC.PlayOneShot(writerSound);

            if (i.ToString() == ".")
            {
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }
        }
    }
}

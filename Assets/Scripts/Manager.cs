using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager ManagerScript = null;

    public AudioSource click;

    private void Awake()
    {
        if (ManagerScript == null)
        {
            ManagerScript = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }

    public void Play()
    {
        click.Play();

        SceneManager.LoadScene(1);
    }
}

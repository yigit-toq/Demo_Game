using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager ManagerScript = null;

    public GameObject[] Screen;

    public TMPro.TextMeshProUGUI[] SliderText;

    public Slider[] SettingsSlider;

    public TMPro.TMP_InputField[] InputField;
    public string[] TeamName;
    public int[] TeamScore;

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

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            SliderText[0].text = "Süre: "          + SettingsSlider[0].value * 30;
            SliderText[1].text = "Pas Hakký: "     + SettingsSlider[1].value;
            SliderText[2].text = "Tabu: "          + SettingsSlider[2].value;
            SliderText[3].text = "Kazanma Puaný: " + SettingsSlider[3].value * 25;
        }
    }

    public void StartButton()
    {
        Screen[2].SetActive(true);

        Screen[0].SetActive(false);
    }

    public void TeamChangedButton()
    {
        TeamName[0] = InputField[0].text;
        TeamName[1] = InputField[1].text;

        SceneManager.LoadScene(1);
    }

    public void SettingsButton()
    {
        Screen[1].SetActive(true);

        Screen[0].SetActive(false);
    }

    public void SettingsBack()
    {
        Screen[1].SetActive(false);

        Screen[0].SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}

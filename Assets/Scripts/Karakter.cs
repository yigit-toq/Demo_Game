using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Karakter : MonoBehaviour {

	public float can, maxCan, canDusmehizi;
	public float aclik, maxAclik, aclikDusmehizi;
	public float su, maxSu, suDusmehizi;

	public bool panelKapali;

	public RectTransform canBar, aclikBar, suBar;

	public GameObject Panel;
	public GameObject BilgiPanel;

	public FirstPersonController fps;

	void Start () 
	{
		canBar = GameObject.Find("HeartBar").GetComponent<RectTransform>();
		aclikBar = GameObject.Find("MeatBar").GetComponent<RectTransform>();
		suBar = GameObject.Find("WaterBar").GetComponent<RectTransform>();

		BilgiPanel.SetActive (false);;

		can = maxCan;
		aclik = maxAclik;
		su = maxSu;

		panelKapali = true;
	}

	void Update () 
	{
		canBar.sizeDelta = new Vector2 (can,canBar.sizeDelta.y);
		aclikBar.sizeDelta = new Vector2 (aclik,aclikBar.sizeDelta.y);
		suBar.sizeDelta = new Vector2 (su,suBar.sizeDelta.y);

		if (can <= 0)
		{
			can = 0;
			//GAME OVER
		}

		if (can > maxCan)
		{
			can = maxCan;
		}

		if (aclik <= 0)
		{
			aclik = 0;
		}

		if (aclik > 0)
		{
			aclikDusmehizi = Random.Range (0.1f,0.3f);
			aclik -= aclikDusmehizi * Time.deltaTime;
		}

		if (aclik > maxAclik)
		{
			aclik = maxAclik;
		}

		if (su <= 0)
		{
			su = 0;
		}

		if (su > 0)
		{
			suDusmehizi = Random.Range (0.2f,0.5f);
			su -= suDusmehizi * Time.deltaTime;
		}

		if (su > maxSu)
		{
			su = maxSu;
		}

		if (su <= 0 && aclik <= 0)
		{
			canDusmehizi = Random.Range (0.8f,1.2f);
			can -= canDusmehizi * 2 * Time.deltaTime;
		}

		if (su <= 0 || aclik <= 0)
		{
			canDusmehizi = Random.Range (0.8f,1.2f);
			can -= canDusmehizi * Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.Tab) && !Panel.activeSelf)
		{
			Panel.SetActive (true);
			panelKapali = false;
		}

		else if (Input.GetKeyDown (KeyCode.Tab))
		{
			Panel.SetActive (false);
			panelKapali = true;
		}

		if (!panelKapali)
		{
			fps.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;
		}
		else
		{
			fps.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
		}

		if (!Panel.activeSelf) 
		{
			BilgiPanel.SetActive (false);
		}
	}
}

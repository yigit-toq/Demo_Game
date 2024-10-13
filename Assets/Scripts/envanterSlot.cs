using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class envanterSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

	public itemler item;

	Envanter envanter;

	public int itemSayi;

	public Image itemicon;

	public Text itemmiktar;

	void Start ()
	{
		envanter = GameObject.FindGameObjectWithTag ("Envanter").GetComponent<Envanter> ();
	}

	void Update () 
	{
		item = envanter.items [itemSayi];

		if (item.itemismi != null)
		{
			itemicon.enabled = true;
			itemicon.sprite = item.itemicon;

			if (item.itemMiktar > 1) 
			{
				itemmiktar.enabled = true;
				itemmiktar.text = item.itemMiktar.ToString ();
			}
			else
			{
				itemmiktar.enabled = false;
			}
		}
		else
		{
			itemicon.enabled = false;
			itemmiktar.enabled = false;
		}
	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (item.itemismi != null)
		{
			envanter.BilgiPanelAc (item);
		}
	}
		
	public void OnPointerExit(PointerEventData data)
	{
		envanter.BilgiPanelKapat ();
	}

	public void OnPointerDown(PointerEventData data)
	{
		if (data.button.ToString () == "Left")
		{
			if (!envanter.tasimaAcik && item.itemismi != null) {
				envanter.TasimaPanelAc (item);
				envanter.items [itemSayi] = new itemler ();
			}
			else
			{
				envanter.items [itemSayi] = envanter.tasinanitem;
				envanter.TasimaPanelKapat ();
			}
		}
	}
}

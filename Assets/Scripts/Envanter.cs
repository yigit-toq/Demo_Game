using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Envanter : MonoBehaviour {
	
	public List<itemler> items;
	public itemler bilgiitem,tasinanitem;

	dataitem Dataitem;

	public GameObject Slot,bilgiPanel,tasimaPanel;

	public int slotMiktar;

	public bool bilgiPanelAcik;
	public bool tasimaAcik;

	void Start () 
	{
		Dataitem = GameObject.FindGameObjectWithTag ("dataitem").GetComponent<dataitem> ();

		for (int i = 0;i < slotMiktar;i++)
		{
			GameObject slotobj = (GameObject)Instantiate (Slot);

			slotobj.transform.SetParent (gameObject.transform);
			slotobj.GetComponent<RectTransform> ().localScale = new Vector2 (1,1);
			slotobj.GetComponent<envanterSlot> ().itemSayi = i;

			items.Add (new itemler ());
		}

		itemEkle (1, 1);
		itemEkle (2, 3);
		itemEkle (1, 1);
	}

	public void itemEkle (int id,int miktar)
	{
		for (int i = 0;i < Dataitem.items.Count;i++)
		{
			if(Dataitem.items[i].itemid == id)
			{
				itemler yeniitem = new itemler (Dataitem.items[i].itemismi,Dataitem.items[i].itembilgi,Dataitem.items[i].itemid,miktar,
				Dataitem.items[i].itemdepoMiktar,Dataitem.items[i].itemHasar,Dataitem.items[i].itemtipi);
				BosSlotitemEkle (yeniitem);
			}
		}
	}

	public void BosSlotitemEkle (itemler item)
	{
		for (int i = 0;i < items.Count;i++)
		{
			if (items[i].itemismi == null)
			{
				items[i] = item;
				break;
			}
		}
	}

	public void BilgiPanelAc (itemler item)
	{
		bilgiPanelAcik = true;
		bilgiitem = item;
		bilgiPanel.SetActive (true);
	}

	public void BilgiPanelKapat ()
	{
		bilgiPanelAcik = false;
		bilgiitem = null;
		bilgiPanel.SetActive (false);
	}

	public void TasimaPanelAc (itemler item)
	{
		tasimaAcik = true;
		tasinanitem = item;
		tasimaPanel.SetActive (true);
	}

	public void TasimaPanelKapat () 
	{
		tasimaAcik = false;
		tasinanitem = null;
		tasimaPanel.SetActive (false);
	}

	void Update () 
	{
		if (bilgiPanelAcik) 
		{
			bilgiPanel.transform.GetChild (0).gameObject.GetComponent<Text> ().text = bilgiitem.itemismi;
			bilgiPanel.transform.GetChild (1).gameObject.GetComponent<Text> ().text = bilgiitem.itembilgi;
		}

		if (tasimaAcik) 
		{
			tasimaPanel.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = tasinanitem.itemicon;
			tasimaPanel.GetComponent<RectTransform>	().position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			if (tasinanitem.itemMiktar > 1) 
			{
				tasimaPanel.transform.GetChild (1).gameObject.GetComponent<Text> ().enabled = true;
				tasimaPanel.transform.GetChild (1).gameObject.GetComponent<Text> ().text = tasinanitem.itemMiktar.ToString();
			}
			else
			{
				tasimaPanel.transform.GetChild (1).gameObject.GetComponent<Text> ().enabled = false;
			}
		}
	}
}

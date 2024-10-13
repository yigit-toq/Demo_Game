using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class itemler 
{
	public string itemismi,itembilgi;

	public int itemid, itemMiktar, itemdepoMiktar,itemHasar;

	public Sprite itemicon;

	public GameObject itemmodel;

	public itemType itemtipi;

	public enum itemType
	{
		Silah,
		Malzeme,
		Yiyecek,
		Bos
	}

	public itemler (string isim, string bilgi, int id, int miktar, int depo, int hasar, itemType tip)
	{
		itemismi = isim;
		itembilgi = bilgi;
		itemid = id;
		itemMiktar = miktar;
		itemdepoMiktar = depo;
		itemHasar = hasar;
		itemtipi = tip;

		itemicon = Resources.Load<Sprite> (id.ToString());
		itemmodel = Resources.Load<GameObject> ("Kup");
	}

	public itemler ()
	{
		
	}
}

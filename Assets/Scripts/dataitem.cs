using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataitem : MonoBehaviour {

	public List<itemler> items;

	void Awake ()
	{
		items.Add (new itemler ("", "", 0, 0, 0, 0, itemler.itemType.Bos));
		items.Add (new itemler ("Çubuk", "Çubuk ile kamp ateşi, balta ve ok gibi daha bir çok eşya geliştirebilirsiniz.", 2, 1, 8, 0, itemler.itemType.Malzeme));
		items.Add (new itemler ("Balta", "Balta kullanarak ağaç kesebilir ve hayvan avlayabilirsiniz.", 1, 1, 1, 0, itemler.itemType.Silah));
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructable : MonoBehaviour
{
	[SerializeField]
	private string displayName;
	[SerializeField]
	private string[] itens;
	private Hashtable itensMap;
	private int itensToCollect;
	private int itensCollected = 0;
	public delegate void func();
	public func TryEndGame;

	// Start is called before the first frame update
	void Start()
	{
		itensToCollect = itens.Length;
		itensMap = new Hashtable();
		foreach (string item in itens)
		{
			itensMap.Add(item, false);
		}
	}

	// Update is called once per frame
	void ReceiveItem(string itemName)
	{
		Debug.Log(itemName);
        Debug.Log(itensMap);
        bool? value = (bool?)itensMap[itemName];
		if (value != null)
		{
			if (value == false)
			{
				itensMap[itemName] = true;
				itensCollected++;
                if (itensCollected == itensToCollect) OnComplete();
				Debug.Log("item adicionado");
			}
			else
			{
				Debug.Log("item ja tava adicionado, isso nao devia acontecer");
			}
		}
		else
		{
			Debug.Log("item nao pertence a maquina");
			Inventory.RespawnItem();
		}
        Inventory.ClearItem();
		//Debug.Log(itensMap[itemName]);
	}

    private void OnComplete() {
        TryEndGame();
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }

	public bool IsCompleted()
	{
		return itensCollected == itensToCollect;
	}

	// when the GameObjects collider arrange for this GameObject to travel to the left of the screen
	void OnTriggerEnter2D(Collider2D col)
	{
		if (Inventory.Has())
			ReceiveItem(Inventory.Use());
	}

}
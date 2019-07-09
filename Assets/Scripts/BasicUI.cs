﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    private GUIStyle _style;
    public int fontSize = 12;

    private void Awake() {
        _style = new GUIStyle("box");
        _style.fontSize = fontSize;   
    }

    private void OnGUI() {
        int posX = 10;
        int posY = 10;
        int width = 200;
        int height = 60;
        int buffer = 20;

        List<Item> itemList = Managers.Inventory.GetItemList();
        if (itemList.Count == 0) {
            GUI.Box(new Rect(posX, posY, width, height), "No Items", _style);
        }

        foreach(Item item in itemList) {
            int count = Managers.Inventory.GetItemCount(item);
            Texture2D image = Resources.Load<Texture2D>("Icons/"+item.name);
            GUI.Box(new Rect(posX, posY, width, height),
                 new GUIContent("(" + count + ")", image), _style);
            posX += width + buffer;
        }

        Item equipped = Managers.Inventory.equippedItem;
        if(equipped != null) {
            posX = Screen.width - (width+buffer);
            Texture2D image = Resources.Load("Icons/"+equipped.name) as Texture2D;
            GUI.Box(new Rect(posX, posY, width, height),
                    new GUIContent("Equipped", image), _style);
        }

        posX = 10;
        posY += height + buffer;

        foreach(Item item in itemList) {
            if (GUI.Button(new Rect(posX, posY, width, height),
                "Equip " + item.name, _style)) {
                    Managers.Inventory.EquipItem(item);
            }
            posX += width + buffer;
        }
    }
}

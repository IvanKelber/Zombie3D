using System.Collections;
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
    }
}

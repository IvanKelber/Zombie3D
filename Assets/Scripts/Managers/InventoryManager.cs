using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}

    private Dictionary<Item, int> _inventory;

    public void Startup() {
        Debug.Log("Inventory Manager startup");
        status = ManagerStatus.Started;
        _inventory = new Dictionary<Item, int>();
    }

    public void DisplayInventory() {
        foreach(KeyValuePair<Item, int> item in _inventory) {
            Debug.Log("Item: " +item.Key.name + " Quantity: " + item.Value);
        }
    }

    public void AddItem(Item item) {
        int value = 0;
        if(!_inventory.TryGetValue(item, out value)) {
            _inventory[item] = 0;
        }
        _inventory[item] = value + 1;

        DisplayInventory();
    }
}

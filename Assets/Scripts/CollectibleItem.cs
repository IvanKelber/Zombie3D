using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private ItemId id;
    [SerializeField] private string name;

    private void OnTriggerEnter(Collider other) {
        Managers.Inventory.AddItem(new Item(id, name));
        Destroy(this.gameObject);
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDevice : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;
    public bool requireKey = false;

    private void OnTriggerEnter(Collider other) {
        if(requireKey && Managers.Inventory.equippedItem.id != ItemId.Key) {
            return;
        }

        foreach(GameObject target in targets) {
            target.SendMessage("Activate");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(requireKey && Managers.Inventory.equippedItem.id != ItemId.Key) {
            return;
        }

        foreach(GameObject target in targets) {
            target.SendMessage("Deactivate");
        }
    }
}

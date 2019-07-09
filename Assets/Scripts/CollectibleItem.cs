using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private ItemId id;

    private void OnTriggerEnter(Collider other) {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public ItemId id;
    public string name;

    public Item(ItemId id, string name) {
        this.id = id;
        this.name = name;
    }

    public override int GetHashCode() {
        int number = (int) id * (10 << name.Length);
        for(int i = 0; i < name.Length; i++) {
            number += ((int) name[i]) * ( 10<< i);
        } 
        Debug.Log("IEVK" + number);
        return number;
    }

    public bool Equals(Item other) {
        return other.name == this.name && other.id == this.id;
    }

    public override bool Equals(object other) {
        return this.Equals(other as Item);
    }
}

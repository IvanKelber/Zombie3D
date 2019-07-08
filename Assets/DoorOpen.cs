using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private Vector3 dPos;
    private Vector3 pos;
    private bool _open = false;
    private bool _moving = false;

    public void Operate() {
        if(_open) {
            pos = transform.position - dPos;
        } else {
            pos = transform.position + dPos;
        }
        _moving = true;
        _open = !_open;
    }

    void Update() {
        if(_moving) {
            if(Vector3.Distance(transform.position, pos) < .1f) {
                _moving = false;
            } else {
                transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
            }
        }
    }

    
}

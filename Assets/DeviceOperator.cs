using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    private float operateDistance = 1.5f;

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.J)) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, operateDistance);
            foreach(Collider hitCollider in colliders) {
                Vector3 direction = hitCollider.transform.position - transform.position;
                if(Vector3.Dot(transform.forward, direction) > 0.5f) {
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}

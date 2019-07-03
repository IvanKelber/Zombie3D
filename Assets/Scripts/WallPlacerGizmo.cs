using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class WallPlacerGizmo : MonoBehaviour
{


    private void OnDrawGizmosSelected() {
        Vector3 startPoint= new Vector3(transform.position.x - transform.localScale.x/2, transform.position.y, transform.position.z + transform.localScale.x/2);
        Vector3 endPoint = new Vector3(transform.position.x + transform.localScale.x/2, transform.position.y, transform.position.z + transform.localScale.x/2);
        endPoint = Quaternion.AngleAxis(transform.rotation.y, Vector3.up) * endPoint;
        Handles.Label(startPoint, startPoint.ToString());
        Handles.Label(endPoint, endPoint.ToString());
    }
}

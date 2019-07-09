using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCoin : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 90.0f;
    
    [RangeAttribute(0, 2* Mathf.PI)]
    private float t = 0;

    void Start() {
        transform.Rotate(Vector3.up * Random.Range(0,360), Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * Mathf.Sin(2 * t), transform.position.z);
    }
}

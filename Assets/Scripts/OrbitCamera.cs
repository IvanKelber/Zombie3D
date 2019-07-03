using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float _rotationSpeed = 1.5f;

    private float _rotY;
    private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal != 0) {
            _rotY += horizontal * _rotationSpeed; 
        }

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position - (rotation * _offset);
        transform.LookAt(target);
    }
}

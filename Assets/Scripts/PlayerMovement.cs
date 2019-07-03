using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float baseSpeed = 15.0f;
    private float speed = 15.0f;
    private float turnSpeed = 3.0f;
    private Vector3 movement;

    private bool autoRun = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = GetStrafeInput() * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        movement.x = deltaX;
        movement.z = deltaZ;
        movement = Vector3.ClampMagnitude(movement, speed); // clamp diagonal velocity
    

        movement *= Time.deltaTime;
        transform.Translate(movement);

        float turn = Input.GetAxis("Horizontal") * turnSpeed;
        transform.Rotate(turn * Vector3.up);
    

    }

    private float GetStrafeInput() {
        if(Input.GetKey(KeyCode.Q)) {
            return -1f;
        }
        if(Input.GetKey(KeyCode.E)) {
            return 1f;
        }
        return 0f;
    }
}

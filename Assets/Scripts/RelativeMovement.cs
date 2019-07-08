using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Rigidbody _body;
    private Collider _collider;
    private Vector3 _movement;
    private Animator _animator;
    private float rotationSpeed = 15.0f;
    private float movementSpeed = 9.0f;
    private bool _isJumping;

    //Physics
    public float JumpHeight = 20.0f;
    private float fallMultiplier = 10.5f;
    private float GroundDistance = 0.2f;
    public float dashSpeed = 20.0f;
    public float collisionForce = 3.0f;

    void Start() {
        _body = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        _isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        _movement = Vector3.zero;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(horizontal != 0 || vertical != 0) {
            _movement.x = horizontal * movementSpeed;
            _movement.z = vertical * movementSpeed;
            _movement = Vector3.ClampMagnitude(_movement, movementSpeed);

            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            _movement = target.TransformDirection(_movement);
            target.rotation = tmp;

            // Lerping rotation
            Quaternion direction = Quaternion.LookRotation(_movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotationSpeed * Time.deltaTime);
        }

        //update animation
        _animator.SetFloat("speed",_movement.sqrMagnitude);
        _animator.SetBool("isJumping", _isJumping);
        _animator.SetBool("isDashing", false);

        // Handle vertical movement

        bool hitGround = isGrounded();

        if(hitGround) {
            if(Input.GetButtonDown("Jump")) {
                _body.AddForce(Vector3.up * JumpHeight, ForceMode.VelocityChange);
                _isJumping = true;
            } else {
                _isJumping = false;
            }

            if(Input.GetKeyDown(KeyCode.LeftShift)) {
                _body.AddForce(this.transform.forward * dashSpeed, ForceMode.VelocityChange);
                _animator.SetBool("isDashing", true);
            }
        }

        if(_body.velocity.y < 0 || (_body.velocity.y > 0 && !Input.GetButton("Jump"))) {
            Vector3 force = Vector3.up + (Physics.gravity * fallMultiplier);
            _body.AddForce(force, ForceMode.Acceleration);
        }
    
        //end vertical movement
    }

    private bool isGrounded() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit)) {
            return hit.distance < GroundDistance + _collider.bounds.size.y/2;
        }
        return false;
    }

    private void FixedUpdate() {
        _movement *= Time.fixedDeltaTime;
        _body.MovePosition(_body.position + _movement);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Rigidbody _body;
    private float rotationSpeed = 15.0f;
    private float movementSpeed = 9.0f;

    //Physics
    public float JumpHeight = 3.0f;
    private Animator _animator;
    private Vector3 _movement;
    private float GroundDistance = 0.2f;
    [SerializeField] private Transform _groundChecker;
    public LayerMask Ground;
    private Vector3 _gravity;
    private bool _isJumping;
    public float dashSpeed = 20.0f;


    void Start() {
        _body = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _gravity = Physics.gravity * 3;
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

        bool hitGround = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        if(hitGround) {
            if(Input.GetButtonDown("Jump")) {
                _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
                _isJumping = true;
            } else {
                _isJumping = false;
            }

            if(Input.GetKeyDown(KeyCode.LeftShift)) {
                _body.AddForce(this.transform.forward * dashSpeed, ForceMode.VelocityChange);
                _animator.SetBool("isDashing", true);
            }
        }


        if(Input.GetButton("Jump")) {
            Physics.gravity = _gravity * 0.2f;
        }
        if(Input.GetButtonUp("Jump")) {
            Physics.gravity = _gravity;
        }


    
        //end vertical movement
    }

    private void FixedUpdate() {
        _movement *= Time.fixedDeltaTime;
        _body.MovePosition(_body.position + _movement);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    private CharacterController _character;
    private float rotationSpeed = 15.0f;
    private float movementSpeed = 9.0f;

    //Physics
    public float gravity = -9.8f;
    public float jumpVelocity = 35.0f;
    public float terminalVelocity = 12.0f;
    public float minFall = -1.5f;

    private float _vertSpeed;

    private ControllerColliderHit _contact;
    private Animator _animator;

    void Start() {
        _character = GetComponent<CharacterController>();
        _vertSpeed = minFall;
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(horizontal != 0 || vertical != 0) {
            movement.x = horizontal * movementSpeed;
            movement.z = vertical * movementSpeed;
            movement = Vector3.ClampMagnitude(movement, movementSpeed);

            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            // Lerping rotation
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotationSpeed * Time.deltaTime);
        }

        //update animation
        _animator.SetFloat("playerSpeed",movement.sqrMagnitude);

        // Handle vertical movement
        bool hitGround = false;
        RaycastHit hit;
        if(_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)) {
            float check = 1.8f; //Hardcoded this check cuz there was a bug and I was tired.  *shrug*
            hitGround = hit.distance <= check;
        }
        if(hitGround) {
            if(Input.GetButtonDown("Jump")) {
                _vertSpeed = jumpVelocity;
            } else {
                _vertSpeed = minFall;
            }
        } else {
            _vertSpeed += gravity * Time.deltaTime * 5;
            if(_vertSpeed > terminalVelocity) {
                _vertSpeed = terminalVelocity;
            }
            if(_character.isGrounded) {
                if(Vector3.Dot(_contact.normal, movement) < 0) {
                    movement = _contact.normal * movementSpeed;
                } else  {
                    movement += _contact.normal * movementSpeed;
                }
            }
        }
        movement.y = _vertSpeed;
        //end vertical movement

        movement *= Time.deltaTime;
        _character.Move(movement);

 
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        _contact = hit;
    }
}

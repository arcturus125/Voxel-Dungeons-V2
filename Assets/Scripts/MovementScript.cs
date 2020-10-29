using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private float xSpeed = CameraController.xSpeed; //sensitivity on x axis, default 180

    public float autojumpRaycastHeight = 1; // the height of the raycast relative to the player which if hits nothing, will autojump the player
    public float autojumpHeight = 1; // the height at which the player will autojump
    public float autojumpClip = 0.1f;
    public float jumpHeight = 12.0f; // the height the player jumps when space is pressed
    public float speed = 0.1f; //Player movement speed
    public float defaultSpeed = 0.1f; //Player movement speed
    public float runSpeed = 0.4f; //Player movement speed
    public bool canJump; //Checks whether player is on the ground
    public float RayDistance = 0.5f;

    public KeyCode runKey = KeyCode.LeftControl;


    public Rigidbody rb;
    Animator anim;

    Vector3 direction;
    Vector3 movement;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        canJump = true;

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        AutoJumpRaycast();
    }

    void Movement()
    {
        // variable initialisation
        direction = Vector3.zero;
        movement = Vector3.zero;


        // sprinting
        if(Input.GetKeyDown(runKey))
        {
            speed = runSpeed;
            anim.SetBool("isRunning", true);
        }
        if(Input.GetKeyUp(runKey))
        {
            speed = defaultSpeed;
            anim.SetBool("isRunning", false);
        }

        //movment
        anim.SetBool("isWalking", false);
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.forward;
            anim.SetBool("isWalking", true);
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += -Vector3.forward;
            anim.SetBool("isWalking", true);
            direction += -Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += -Vector3.right;
            anim.SetBool("isWalking", true);
            direction += -Vector3.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
            anim.SetBool("isWalking", true);
            direction += Vector3.right;
        }
        rb.MovePosition(transform.position + transform.TransformDirection(movement) * speed);

        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            rb.AddForce(new Vector3(0.0f, jumpHeight, 0.0f), ForceMode.Impulse);
        }

        //Rotates player with mouse
        if (!UIController.isCursorVisible)
        {
            float xRot = Input.GetAxis("Mouse X");
            if (xRot != 0)
            {
                //x += xRot * xSpeed * 0.02f;
                transform.Rotate(0, xRot * xSpeed * 0.02f, 0.0f);
            }
        }
    }
    void AutoJumpRaycast()
    {
        // DEBUG: Debug.DrawRay(transform.position, transform.TransformDirection(direction), Color.red, 5, false);

        RaycastHit FootRaycast;
        RaycastHit ArmsRaycast;

        // if the players send a raycast out from their feet and it hits the ground
        if (Physics.Raycast(transform.position + new Vector3(0,0.1f,0), transform.TransformDirection(direction), out FootRaycast, RayDistance)
                    && FootRaycast.collider.gameObject.tag == "Ground")
        {
            // draw ray if in debug mode
            if (ADMIN.Debug_Mode)
            {
                Debug.Log(" FEET HIT: " + FootRaycast.collider.gameObject.name);
                Debug.DrawRay(transform.position, transform.TransformDirection(direction), Color.red, 5, false);
            }

            // if the player send a raycast from their arms and it hits something
            if (Physics.Raycast( new Vector3(transform.position.x, transform.position.y + autojumpRaycastHeight, transform.position.z),
                    transform.TransformDirection(Vector3.forward), out ArmsRaycast, RayDistance))
            {
                // draw ray if in debug mode
                if (ADMIN.Debug_Mode)
                {
                    Debug.DrawRay( new Vector3(transform.position.x, transform.position.y + autojumpRaycastHeight, transform.position.z),transform.TransformDirection(Vector3.forward), Color.blue, 5, false);
                    Debug.Log(" ARMS HIT: '" + ArmsRaycast.collider.gameObject.name + "' ");
                }
            }
            // if the player send a raycast and it doesnt hit anything, then autojump
            else
            {
                //draw ray if in debug mode
                if (ADMIN.Debug_Mode)
                    Debug.DrawRay( new Vector3(transform.position.x, transform.position.y + autojumpRaycastHeight, transform.position.z), transform.TransformDirection(Vector3.forward), Color.green, 5, false);
                
                
                Autojump();
            }
        }
    }
    void Autojump()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + autojumpHeight, transform.position.z) + transform.TransformDirection(Vector3.forward * autojumpClip);
    }

    private void OnTriggerEnter(Collider other) //runs when player hits the ground
    {
        if (other.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }
    private void OnTriggerExit(Collider other) //runs when player leaves the ground
    {
        if (other.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float xSpeed = 180.0f; //sensitivity on x axis, default 180
    public float ySpeed = 180.0f; //sensitivity on y axis, default 180
    //float x = 0.0f;
    //float y = 0.0f;

    public float jumpHeight = 12.0f;
    public float speed = 1000.0f; //Player movement speed
    public Rigidbody rb;
    Animator anim;

    public bool canJump; //Checks whether player is on the ground
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canJump = true;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isWalking", false);
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(new Vector3(0.0f, 0.0f, speed * Time.deltaTime), ForceMode.VelocityChange);
            anim.SetBool("isWalking", true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(new Vector3(0.0f, 0.0f, -speed * Time.deltaTime), ForceMode.VelocityChange);
            anim.SetBool("isWalking", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f), ForceMode.VelocityChange);
            anim.SetBool("isWalking", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f), ForceMode.VelocityChange);
            anim.SetBool("isWalking", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            rb.AddForce(new Vector3(0.0f, jumpHeight, 0.0f), ForceMode.Impulse);
        }

        //Rotates player with mouse
        if (!UIController.isCursorVisible) {
            float xRot = Input.GetAxis("Mouse X");
            if (xRot != 0)
            {
                //x += xRot * xSpeed * 0.02f;
                transform.Rotate(0, xRot * xSpeed * 0.02f, 0.0f);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other) //Checks if player is on ground
    {
        if (other.gameObject.tag == "Ground")
        {
            canJump = true;
           // Debug.Log("True");
        }
    }

    private void OnTriggerExit(Collider other) //Checks if player is not on ground
    {
        //Debug.Log("Called");
        if (other.gameObject.tag == "Ground")
        {
            canJump = false;
            //Debug.Log("False");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Header("Needed Components")]
  Animator anim;
  Rigidbody rb;

  [Header("Player Parameters")]
  bool isGrounded = true;
  [SerializeField] float JumpForce = 500f;
  [SerializeField] float WalkSpeed = 10f;
  [SerializeField] float RunSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      isGrounded = Physics.Raycast(transform.position, transform.up*-1f, 0.2f);
      Debug.DrawRay(transform.position, transform.up*-0.2f, Color.red);
      anim.SetBool("isGrounded", isGrounded);
      anim.SetFloat("Velocity", rb.velocity.magnitude);
      if (isGrounded)
      {
        anim.SetBool("Jumped", false);
      }
      if (Input.GetButtonDown("Jump"))
      {
        anim.SetBool("Jumped", true);
        anim.SetTrigger("Jump");
        rb.AddForce(transform.up * JumpForce, ForceMode.Acceleration);
      }
      // if (Input.GetAxis("Vertical") != 0)
      // {
      //   if (Input.GetButtonDown("Sprint")) rb.AddForce(transform.forward * Input.GetAxis("Vertical") * RunSpeed, ForceMode.Acceleration);
      //   else rb.AddForce(transform.forward * Input.GetAxis("Vertical") * WalkSpeed, ForceMode.Acceleration);
      // }
      float horizontalInput = Input.GetAxisRaw("Horizontal");
      float verticalInput = Input.GetAxisRaw("Vertical");
      Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
      float forcenum = (Input.GetButtonDown("Sprint")) ? WalkSpeed : RunSpeed;
      rb.AddForce(moveDirection.normalized * forcenum * 10f, ForceMode.Force);
      SpeedControl();
      // Vector3 vel = rb.velocity;
      // vel.x = Mathf.Clamp(vel.x, -5f, 5f);
      // vel.y = Mathf.Clamp(vel.y, -5f, 5f);
      // vel.z = Mathf.Clamp(vel.z, -5f, 5f);
      // rb.velocity = vel;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > WalkSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * WalkSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}

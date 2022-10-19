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
  [SerializeField] float groundDrag = 10f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
      isGrounded = Physics.Raycast(transform.position, transform.up*-1f, 0.2f);
      rb.drag = (isGrounded) ? groundDrag : 0f;
      if (Input.GetButtonDown("Jump"))
      {
        anim.SetBool("Jumped", true);
        anim.SetTrigger("Jump");
        rb.AddForce(transform.up * JumpForce, ForceMode.Acceleration);
      }
      Move();
      SpeedControl();
      UpdateAnimParam();
    }

    private void Move()
    {
      float horizontalInput = Input.GetAxisRaw("Horizontal");
      float verticalInput = Input.GetAxisRaw("Vertical");
      Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
      float forcenum = (Input.GetButton("Sprint")) ? RunSpeed : WalkSpeed;
      rb.AddForce(moveDirection.normalized * forcenum * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float forcenum = (Input.GetButton("Sprint")) ? RunSpeed : WalkSpeed;
        if(flatVel.magnitude > forcenum)
        {
            Vector3 limitedVel = flatVel.normalized * forcenum;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void UpdateAnimParam()
    {
      anim.SetFloat("Velocity", rb.velocity.magnitude);
      if (isGrounded) anim.SetBool("Jumped", false);
      anim.SetBool("isGrounded", isGrounded);
    }
}

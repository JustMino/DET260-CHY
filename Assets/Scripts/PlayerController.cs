using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Header("Needed Components")]
  Animator anim;
  Rigidbody rb;
  [SerializeField]GameObject campiv;

  [Header("Player Parameters")]
  bool isGrounded = true;
  bool isCrouched = false;
  [SerializeField] float JumpForce = 500f;
  [SerializeField] float WalkSpeed = 10f;
  [SerializeField] float RunSpeed = 20f;
  [SerializeField] float groundDrag = 10f;
  [SerializeField] Vector3 moveDirection;
  [SerializeField] float rotationspeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        campiv = GameObject.Find("CameraPivot");
    }

    void Update()
    {
      isGrounded = Physics.Raycast(transform.position, transform.up*-1f, 0.3f);
      rb.drag = (isGrounded) ? groundDrag : 0f;
      if (Input.GetButtonDown("Jump") && isGrounded)
        rb.AddForce(transform.up * JumpForce, ForceMode.Acceleration);
      if (Input.GetButtonDown("Crouch") && isGrounded)
      {
        anim.SetTrigger("Crouch");
        anim.SetBool("Crouched", !anim.GetBool("Crouched"));
        isCrouched = !isCrouched;
      }
      Move();
      SpeedControl();
      UpdateAnimParam();
    }

    private void Move()
    {
      float horizontalInput = Input.GetAxisRaw("Horizontal");
      float verticalInput = Input.GetAxisRaw("Vertical");
      float vflip = (verticalInput < 0) ? -1f : 1f;
      float hflip = (horizontalInput < 0) ? -1f : 1f;
      moveDirection = transform.forward * verticalInput * vflip + transform.forward * horizontalInput * hflip;
      float forcenum = (Input.GetButton("Sprint")) ? RunSpeed : WalkSpeed;
      rotatePlayer(horizontalInput, verticalInput);
      rb.AddForce(moveDirection.normalized * forcenum * 10f, ForceMode.Force);
    }

    void rotatePlayer(float h, float v)
    {
      var step = Time.deltaTime * rotationspeed;
      if (h == 0 && v == 0)
      {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, campiv.transform.rotation, step);
      }
      else
      {
        Quaternion targetrot = Quaternion.Euler(new Vector3 (0f, Mathf.Atan2(h, v) * Mathf.Rad2Deg, 0f));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetrot, step);
      }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float forcenum = (Input.GetButton("Sprint")) ? RunSpeed : WalkSpeed;
        if(flatVel.magnitude > forcenum)
        {
          float crouchspd = (isCrouched) ? 0.1f : 1f;
            Vector3 limitedVel = flatVel.normalized * forcenum * crouchspd;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void UpdateAnimParam()
    {
      anim.SetFloat("Velocity", Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2f) + Mathf.Pow(rb.velocity.z, 2f)));
      anim.SetBool("isGrounded", isGrounded);
    }
}

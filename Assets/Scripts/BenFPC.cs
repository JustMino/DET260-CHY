using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenFPC : MonoBehaviour
{
  Rigidbody rb;

  Vector3 moveInput;

  float characterSpeed;

  [SerializeField] float walkSpeed = 5.0f;
  [SerializeField] float runSpeed = 8.0f;
  [SerializeField] float jumpForce = 2.0f;

  [SerializeField] bool isJumping;
  [SerializeField] bool isGrounded;

  // Start is called before the first frame update
  void Start()
  {
      rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
      if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
      {
          characterSpeed = runSpeed;
      }
      else
      {
          characterSpeed = walkSpeed;
      }

      moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

      transform.Translate(moveInput * characterSpeed * Time.deltaTime);

      if (Input.GetButton("Jump") && isGrounded)
      {
          isJumping = true;
      }
  }

  private void FixedUpdate()
  {
      if (isJumping)
      {
          rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

          isJumping = false;
      }
  }

  private void OnTriggerStay(Collider other)
  {
      isGrounded = true;
  }

  private void OnTriggerExit(Collider other)
  {
      isGrounded = false;
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Header("Movement")]
  public float moveSpeed;

  public float groundDrag;

  public float jumpForce;
  public float jumpCooldown;
  public float airMultiplier;
  bool readyToJump;

  [HideInInspector] public float walkSpeed;
  [HideInInspector] public float sprintSpeed;

  [Header("Keybinds")]
  public KeyCode jumpKey = KeyCode.Space;

  [Header("Ground Check")]
  public float playerHeight;
  public LayerMask whatIsGround;
  public bool grounded;

  public Transform orientation;

  float horizontalInput;
  float verticalInput;

  Vector3 moveDirection;

  Rigidbody rb;
  Rigidbody otherrb;
  [SerializeField] bool onTruck = false;
  [SerializeField] Vector3 vel;

  [SerializeField] Vector3 normalStruck;

  GameManager GM;
  CanvasUpdater canvasup;

  private void Start()
  {
    GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    canvasup = GameObject.Find("Canvas").GetComponent<CanvasUpdater>();
    rb = GetComponent<Rigidbody>();
    rb.freezeRotation = true;
    readyToJump = true;
  }

  private void Update()
  {
      MyInput();
      SpeedControl();
      // Time.timeScale = (Input.GetKey(KeyCode.E)) ? 0f : 1f;
  }

  private void FixedUpdate()
  {
    MovePlayer();
    vel = rb.velocity;
    if (GetComponent<HingeJoint>() != null)
    {
      if (GetComponent<HingeJoint>().connectedBody == null)
      {
        Destroy(GetComponent<HingeJoint>());
        rb.mass = 0.01f;
      }
    }
  }

  private void MyInput()
  {
      horizontalInput = Input.GetAxisRaw("Horizontal");
      verticalInput = Input.GetAxisRaw("Vertical");

      // when to jump
      if(Input.GetKey(jumpKey) && readyToJump && grounded)
      {
          readyToJump = false;

          Jump();

          Invoke(nameof(ResetJump), jumpCooldown);
      }
  }

  private void MovePlayer()
  {
      // calculate movement direction
      moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

      // on ground
      if(grounded)
      {
        if (GetComponent<HingeJoint>() != null)
        {
          GetComponent<HingeJoint>().autoConfigureConnectedAnchor = false;

          transform.Translate(moveDirection * 10f * Time.deltaTime);
          GetComponent<HingeJoint>().autoConfigureConnectedAnchor = true;
        }
      }
      // in air
      else if(!grounded)
      {
        moveDirection.z *= 10f;
        moveDirection.x *= 2.5f;
        rb.AddForce(moveDirection * moveSpeed * airMultiplier, ForceMode.Acceleration);
      }
  }

  private void SpeedControl()
  {
      Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

      // limit velocity if needed
      if(flatVel.magnitude > moveSpeed)
      {
        Vector3 limitedVel = flatVel.normalized * moveSpeed;
        rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
      }
  }

  private void Jump()
  {
    if (GetComponent<HingeJoint>() != null)
    {
      GetComponent<HingeJoint>().autoConfigureConnectedAnchor = false;
      Destroy (GetComponent<HingeJoint>());
    }
    rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);

  }
  private void ResetJump()
  {
      readyToJump = true;
  }

  void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
    {
      grounded = true;
      ContactPoint otherContact = other.contacts[0];
      normalStruck = otherContact.normal;
      if (normalStruck.y > 0.9f)
      {
        other.gameObject.GetComponent<TruckMovement>().player = gameObject;
        onTruck = true;
        otherrb = other.gameObject.GetComponent<Rigidbody>();
        var hj = gameObject.AddComponent<HingeJoint>();
        GetComponent<HingeJoint>().connectedBody = otherrb;
        rb.mass = 0.00001f;
      }
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
    {
      other.GetComponent<TruckMovement>().player = null;
      otherrb = null;
      grounded = false;
      onTruck = false;
      Destroy (GetComponent<HingeJoint>());
      rb.mass = 0.01f;
      if (other.GetComponent<TruckMovement>().floating)
      {
        canvasup.JumpMidAir();
      }
    }
  }
}

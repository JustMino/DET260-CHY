using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{
  [Header("Needed Components")]
  Rigidbody rb;

  [Header("Speed Settings")]
  [SerializeField] float baseBusSpeed = 1f;
  [SerializeField] float speedVariation = 0.2f;
  [SerializeField] float curSpeed;
  [SerializeField] Vector3 vel;

  [Header("Local Vector Settings so we can set each value specific to each Prefab")]
  [SerializeField] float lookDownThisFar = 0.5f;
  private Vector3 directionToGo;
  private Vector3 directionIsDown;

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      baseBusSpeed = Random.Range(baseBusSpeed - speedVariation, baseBusSpeed + speedVariation);
      curSpeed = baseBusSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
      if (Physics.Raycast(transform.position, transform.up * -1f, lookDownThisFar))
      {
        // Debug.Log("Touched ground");
        rb.AddForce(transform.forward * curSpeed, ForceMode.VelocityChange);
      }
      else
      {
        // Debug.Log("Not touching ground");
      }
      // rb.velocity = rb.velocity.normalized * 40f;
      vel = rb.velocity;
      vel.z = Mathf.Clamp(vel.z, 0f, 40f);
      rb.velocity = vel;
    }
}

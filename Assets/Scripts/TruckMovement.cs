using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{
  [Header("Needed Components")]
  Rigidbody rb;

  [Header("Player")]
  public GameObject player;

  [Header("Speed Settings")]
  [SerializeField] float baseBusSpeed = 1f;
  [SerializeField] float speedVariation = 0.2f;
  [SerializeField] float curSpeed;
  [SerializeField] Vector3 vel;

  [Header("Local Vector Settings so we can set each value specific to each Prefab")]
  [SerializeField] float lookDownThisFar = 0.5f;

  [Header("Particle related")]
  [SerializeField] GameObject[] particles = new GameObject[4];


    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      baseBusSpeed = Random.Range(baseBusSpeed - speedVariation, baseBusSpeed + speedVariation);
      curSpeed = baseBusSpeed;
      for (int i = 0; i < 4; i++)
      {
        particles[i] = transform.Find("FX_TireSmoke (" + i + ")").gameObject;
      }
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
        foreach (GameObject go in particles)
        {
          go.SetActive(true);
        }
        rb.AddForce(transform.forward * curSpeed, ForceMode.VelocityChange);
      }
      else
      {
        foreach (GameObject go in particles)
        {
          go.SetActive(false);
        }
      }
      // rb.velocity = rb.velocity.normalized * 40f;
      vel = rb.velocity;
      vel.z = Mathf.Clamp(vel.z, 0f, 40f);
      rb.velocity = vel;
    }

    void OnTriggerEnter(Collider other)
    {
      if (player != null)
      {
        Destroy(player.GetComponent<HingeJoint>());
        player.GetComponent<Rigidbody>().mass = 0.01f;
      }
    }
}

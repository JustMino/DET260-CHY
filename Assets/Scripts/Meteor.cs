using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
  GameObject player;
  Rigidbody rb;

  public float meteorspeed = 25f;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      rb.AddForce(Physics.gravity * meteorspeed);
    }

    void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.tag == "Terrain")
      {
        Destroy(gameObject, 1f);
      }
    }

}

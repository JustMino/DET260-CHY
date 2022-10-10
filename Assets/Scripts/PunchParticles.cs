using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchParticles : MonoBehaviour
{
  GameObject player;
    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnParticleCollision(GameObject other)
    {
      Rigidbody otherrb = other.gameObject.GetComponent<Rigidbody>();
      Vector3 dir = (other.gameObject.transform.position - player.transform.position).normalized;
      otherrb.AddForce(dir*5000f);
    }
}

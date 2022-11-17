using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerParams : MonoBehaviour
{
  Animator anim;
  PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponent<Animator>();
      player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("HorizontalInput", (int)Input.GetAxisRaw("Horizontal"));
        anim.SetInteger("VerticalInput", (int)Input.GetAxisRaw("Vertical"));
        anim.SetBool("Sprinting", (Input.GetButton("Sprint")) ? true : false);
        if (player.grounded && Input.GetButtonDown("Jump")) anim.SetTrigger("Jump");
        anim.SetFloat("TestH", Input.GetAxis("Horizontal"));
        anim.SetFloat("TestV", Input.GetAxis("Vertical"));
    }
}

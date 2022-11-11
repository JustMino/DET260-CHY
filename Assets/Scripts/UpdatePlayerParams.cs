using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerParams : MonoBehaviour
{
  Animator anim;
    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInt("HorizontalInput", Input.GetAxisRaw("Horizontal"));
        anim.SetInt("VerticalInput", Input.GetAxisRaw("Vertical"));
        anim.SetBool("Sprint", (Input.GetButton("Sprint")) ? true : false);
    }
}

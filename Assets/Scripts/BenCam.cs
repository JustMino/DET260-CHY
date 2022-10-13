using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenCam : MonoBehaviour
{
  float rotationOnX;

  [SerializeField] float mouseSensitivity = 90f;

  [SerializeField] Transform player;


  private void Update()

  {

      float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;

      float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

      rotationOnX -= mouseY;

      rotationOnX = Mathf.Clamp(rotationOnX, -90f, 90);

      transform.localEulerAngles = new Vector3(rotationOnX, 0, 0);

      player.Rotate(Vector3.up * mouseX);

  }
}

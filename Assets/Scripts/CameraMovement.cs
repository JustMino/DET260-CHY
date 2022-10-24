using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  public float sensX;
  public float sensY;

  public Transform orientation;
  float xRotation;
  float yRotation;

  [SerializeField] float damping = 10f;

  private void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  private void Update()
  {
    float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
    float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

    yRotation += mouseX;

    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, 0f, 25f);

    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(xRotation, yRotation, 0), Time.deltaTime * damping);
    orientation.rotation = Quaternion.Lerp(orientation.rotation, Quaternion.Euler(0, yRotation, 0), Time.deltaTime * damping);
  }
}

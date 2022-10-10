using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  public float sensX;
  public float sensY;

  GameManager GM;

  public Transform orientation;
  float xRotation;
  float yRotation;

  private void Start()
  {
    GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  private void Update()
  {
    if (GM.GameOver)
    {
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }
    float mouseX;
    float mouseY;
    if (GM.TimeStop)
    {
      mouseX = Input.GetAxisRaw("Mouse X") * Time.unscaledDeltaTime * sensX;
      mouseY = Input.GetAxisRaw("Mouse Y") * Time.unscaledDeltaTime * sensY;
    }
    else
    {
      mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
      mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
    }
    yRotation += mouseX;

    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    orientation.rotation = Quaternion.Euler(0, yRotation, 0);
  }
}

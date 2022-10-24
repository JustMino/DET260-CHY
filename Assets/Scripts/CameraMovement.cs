using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  public Transform orientation;

  [SerializeField] float damping = 10f;

  private void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  private void Update()
  {
    transform.rotation = Quaternion.Lerp(transform.rotation, orientation.rotation, Time.deltaTime * damping);
  }
}

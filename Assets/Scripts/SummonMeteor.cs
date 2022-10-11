using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMeteor : MonoBehaviour
{
  public Transform Target;

  public GameObject meteor;

  public float meteorcooldown = 10f;

  public float RotationSpeed = 1;

  public float CircleRadius = 1;

  public float ElevationOffset = 0;

  private Vector3 positionOffset;
  private float angle;

  void Start()
  {
    StartCoroutine(Summonmeteor());
  }

  IEnumerator Summonmeteor()
  {
    yield return new WaitForSeconds(meteorcooldown);
    Quaternion rot = Quaternion.Euler(new Vector3 (-90f, 0f, 0f));
    Instantiate(meteor, transform.position, rot);
    StartCoroutine(Summonmeteor());
  }

  private void LateUpdate()
  {
    positionOffset.Set(
      Mathf.Cos( angle ) * CircleRadius,
      ElevationOffset,
      Mathf.Sin( angle ) * CircleRadius
      );
    transform.position = Target.position + positionOffset;
    angle += Time.deltaTime * RotationSpeed;
  }
}

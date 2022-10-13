using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBobble : MonoBehaviour
{
  float startypos = -0.05f;
  float endypos = -0.1f;
  float t = 0f;

  void Update()
  {
    transform.localPosition = new Vector3(0, Mathf.Lerp(endypos, startypos, t), 0);

    t += 0.25f * Time.deltaTime;

    if (t > 1.0f)
      {
          float temp = startypos;
          startypos = endypos;
          endypos = temp;
          t = 0.0f;
      }
  }

}

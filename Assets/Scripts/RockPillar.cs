using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPillar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Rock();

        StartCoroutine(MoveRock());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Rock()
    {
      float xpos = Random.Range(-100f, 50f);
      float zpos = Random.Range(-400f, 400f);
      transform.position = new Vector3 (xpos, -25f, zpos);
      StartCoroutine(MoveRock());
    }

    IEnumerator MoveRock()
    {
      while (transform.position.y < 0f)
      {
        transform.Translate(Vector3.up * Time.deltaTime);
        // yield return new WaitForSeconds(0.05f);
      }
      yield return new WaitForSeconds(0.5f);
      while (transform.position.y > -25f)
      {
        transform.Translate(Vector3.up * Time.deltaTime * -1f);
        // yield return new WaitForSeconds(0.05f);
      }
      // Rock();
    }
}

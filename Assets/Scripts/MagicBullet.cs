using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
  [SerializeField]
  List<GameObject> childObjects = new List<GameObject>();
  bool ran = false;
  Rigidbody rb;
  [SerializeField]
  int magicdmg = 15;
  [SerializeField]
  int explosiondmg = 30;
  [SerializeField]
  LayerMask enemyLayers;


  void Start()
  {
    childObjects.Add(transform.Find("ErekiBall2").gameObject);
    childObjects.Add(transform.Find("WhityBomb").gameObject);
    rb = GetComponent<Rigidbody>();
    Destroy(gameObject, 20);
  }

  void Update()
  {
    if (ran)
    {
      rb.velocity = Vector3.zero;
    }
  }

  void OnCollisionEnter(Collision other)
  {
    if (!ran)
    {
      ran = true;
      rb.velocity = Vector3.zero;
      rb.useGravity = false;
      GetComponent<Collider>().enabled = false;
      foreach (GameObject child in childObjects)
      {
          child.SetActive(!child.activeInHierarchy);
          StartCoroutine(AfterExplosion());
          Destroy(gameObject, 2.5f);
      }
    }
    if (other.gameObject.layer == 6)
    {
      EnemyHealth enemyhealth = other.gameObject.GetComponent<EnemyHealth>();
      enemyhealth.Damage(magicdmg);
    }
  }

  IEnumerator AfterExplosion()
  {
    yield return new WaitForSeconds(2.0f);
    Collider[] hit = Physics.OverlapSphere(transform.position, 5.0f, enemyLayers);
    foreach(var hitCollider in hit)
    {
      EnemyHealth enemyhealth = hitCollider.GetComponent<EnemyHealth>();
      enemyhealth.Damage(explosiondmg);
    }
  }
}

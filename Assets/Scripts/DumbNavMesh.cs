using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DumbNavMesh : MonoBehaviour
{
  NavMeshAgent nav;
  [SerializeField]
  GameObject[] target;

  int c = 0;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(target[0].transform.position);
    }

    void Update()
    {
      nav.SetDestination(target[c].transform.position);
      if (transform.position.x == target[c].transform.position.x && transform.position.z == target[c].transform.position.z)
      {
        ChangeTarget();
      }
    }

    void ChangeTarget()
    {
      if (c == 0) c = 1;
      else c = 0;
    }
}

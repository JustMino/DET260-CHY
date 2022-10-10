using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
  NavMeshAgent nav;
  [SerializeField] Transform[] target;

  int c = 0;
  // Start is called before the first frame update
  void Start()
  {
    nav = GetComponent<NavMeshAgent>();
  }

  // Update is called once per frame
  void Update()
  {
    nav.SetDestination(target[c].position);
    if (transform.position.x == target[c].position.x && transform.position.z == target[c].position.z)
    {
      ChangeTarget();
    }
  }

  void SetDestination(Vector3 targetVector)
  {
      nav.SetDestination(targetVector);
  }
  void ChangeTarget()
  {
    if (c == 0) c = 1;
    else c = 0;
  }
}

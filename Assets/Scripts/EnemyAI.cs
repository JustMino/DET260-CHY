using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
  NavMeshAgent nav;
  [SerializeField]
  GameObject target;
  bool m_Started;

  GameObject atkPoint;
  public float minDistanceSqr = 5f;
  Vector3 hitboxsize = new Vector3 (1.0f, 1.0f, 1.0f);
  [SerializeField]
  float dis;
  [SerializeField]
  LayerMask playerMask;
  bool canattack = true;

  int hitdmg = 10;

  float atkcooldown = 5f;

  Animator anim;
    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponent<Animator>();
      target = GameObject.Find("Player");
      atkPoint = transform.Find("AttackPoint").gameObject;
      nav = GetComponent<NavMeshAgent>();
      SetDestination();
      m_Started = true;
    }

    // Update is called once per frame
    void Update()
    {
      LookAtPlayer();
      SetDestination();
      Attack();
    }

    void SetDestination()
    {
      // Vector3 targetVector = target.transform.position;
      var targetVector = target.transform.position;
      var sqrDistance = (transform.position - targetVector).sqrMagnitude;
      nav.SetDestination(targetVector);
      nav.isStopped = (sqrDistance <= minDistanceSqr);
    }

    void Attack()
    {
      dis = Vector3.Distance(target.transform.position, transform.position);
      if (canattack && dis <= 2.0f)
      {
        canattack = false;
        anim.SetTrigger("Attack");
        Collider[] hit = Physics.OverlapBox(atkPoint.transform.position, hitboxsize, Quaternion.identity, playerMask);
        foreach(var hitCollider in hit)
        {
          PlayerHealth playerhealth = hitCollider.GetComponent<PlayerHealth>();
          playerhealth.Damage(hitdmg);
        }
        StartCoroutine(atkcooldowntime());
      }
    }

    void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
          Gizmos.DrawWireCube(atkPoint.transform.position, hitboxsize);
    }

    void LookAtPlayer()
    {
      var lookPos = target.transform.position - transform.position;
      lookPos.y = 0;
      var rotation = Quaternion.LookRotation(lookPos);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
    }

    IEnumerator atkcooldowntime()
    {
      yield return new WaitForSeconds(atkcooldown);
      canattack = true;
    }
}

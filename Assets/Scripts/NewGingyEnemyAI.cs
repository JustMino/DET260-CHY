using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewGingyEnemyAI : MonoBehaviour
{
  NavMeshAgent nav;
  [SerializeField]
  GameObject target;

  public float minDistanceSqr = 5f;
  Vector3 hitboxsize = new Vector3 (1.0f, 1.0f, 1.0f);
  [SerializeField]
  float dis;
  int stuncount = 0;

  bool Stunned = false;

  Animator anim;
    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponent<Animator>();
      target = GameObject.Find("ShishiroBotan");
      nav = GetComponent<NavMeshAgent>();
      SetDestination();
    }

    // Update is called once per frame
    void Update()
    {
      if (!Stunned)
      {
        LookAtPlayer();
        SetDestination();
      }
      UpdateAnimParam();
      if (stuncount == 0)
      {
        Stunned = false;
        anim.SetBool("Stunned", false);
      }
      else
      {
        Stunned = true;
        anim.SetBool("Stunned", true);
      }
    }

    void SetDestination()
    {
      // Vector3 targetVector = target.transform.position;
      var targetVector = target.transform.position;
      var sqrDistance = (transform.position - targetVector).sqrMagnitude;
      nav.SetDestination(targetVector);
      nav.isStopped = (sqrDistance <= minDistanceSqr);
    }

    void LookAtPlayer()
    {
      var lookPos = target.transform.position - transform.position;
      lookPos.y = 0;
      var rotation = Quaternion.LookRotation(lookPos);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
    }

    private void UpdateAnimParam()
    {
      anim.SetFloat("Velocity", Mathf.Sqrt(Mathf.Pow(nav.velocity.x, 2f) + Mathf.Pow(nav.velocity.z, 2f)));
    }

    public IEnumerator Stun(float time)
    {
      stuncount++;
      yield return new WaitForSeconds(time);
      stuncount--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  [Header("Needed Components")]
  Animator anim;

  [Header("Player Parameters")]
  bool m_Started;
  bool canattack = true;
  [SerializeField] LayerMask enemylayers;

  [System.Serializable]
  public class lightparam
  {
    public int dmg = 10;
    public GameObject atkPoint;
    public Vector3 hitboxsize = new Vector3 (0.5f, 0.2f, 0.8f);
    public Vector3 t;
  }

  [System.Serializable]
  public class heavyparam
  {
    public int dmg = 15;
    public GameObject atkPoint;
    public Vector3 hitboxsize = new Vector3 (1.0f, 1.0f, 1.0f);
    public Vector3 t;
  }

  public lightparam LightParam = new lightparam();
  public heavyparam HeavyParam = new heavyparam();
  // Start is called before the first frame update
  void Start()
  {
    anim = GetComponent<Animator>();
    LightParam.atkPoint = GameObject.Find("Hitbox");
    HeavyParam.atkPoint = GameObject.Find("HeavyHitbox");
    m_Started = true;
    LightParam.t = LightParam.atkPoint.transform.position;
    HeavyParam.t = HeavyParam.atkPoint.transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Light") && canattack)
    {
      canattack = false;
      LightParam.t.x *= -1f;
      LightParam.atkPoint.transform.position = LightParam.t;
      anim.SetBool("Side(True=Right)", !anim.GetBool("Side(True=Right)"));
      anim.SetTrigger("Punch");
    }
    if(Input.GetButtonDown("Heavy") && canattack)
    {
      canattack = false;
      anim.SetTrigger("Kick");
    }
  }

  public void LightHitCheck()
  {
    Collider[] hit = Physics.OverlapBox(LightParam.atkPoint.transform.position, LightParam.hitboxsize, transform.rotation, enemylayers);
    foreach(var hitCollider in hit)
    {
      EnemyHealth enemyhealth = hitCollider.GetComponent<EnemyHealth>();
      enemyhealth.Damage(LightParam.dmg);
      if (enemyhealth.EnemyID == 1)
      {
        GingyEnemyAI ai = hitCollider.GetComponent<GingyEnemyAI>();
        ai.Stun(0.5f);
      }
      else
      {
        NewGingyEnemyAI ai = hitCollider.GetComponent<NewGingyEnemyAI>();
        ai.Stun(0.5f);
      }
    }
  }

  public void HeavyHitCheck()
  {
    Collider[] hit = Physics.OverlapBox(HeavyParam.atkPoint.transform.position, LightParam.hitboxsize, transform.rotation, enemylayers);
    foreach (var hitCollider in hit)
    {
      EnemyHealth enemyhealth = hitCollider.GetComponent<EnemyHealth>();
      enemyhealth.Damage(HeavyParam.dmg);
      if (enemyhealth.EnemyID == 1)
      {
        GingyEnemyAI ai = hitCollider.GetComponent<GingyEnemyAI>();
        ai.Stun(1f);
      }
      else
      {
        NewGingyEnemyAI ai = hitCollider.GetComponent<NewGingyEnemyAI>();
        ai.Stun(1f);
      }
    }
  }

  public void resetatkcooldown()
  {
    canattack = true;
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    if (m_Started) Gizmos.DrawWireCube(HeavyParam.atkPoint.transform.position, HeavyParam.hitboxsize);
  }
}

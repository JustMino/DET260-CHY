using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  Animator anim;
  [SerializeField]
  int curhealth;
  [SerializeField]
  int maxhealth = 150;

  public int EnemyID;
    // Start is called before the first frame update
    void Start()
    {
      EnemyID = (gameObject.name.Contains("GingyAttacks")) ? 1 : 2;
      anim = GetComponent<Animator>();
      curhealth = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
      if (curhealth <= 0)
      {
        curhealth = 0;
        Destroy(gameObject);
      }
    }

    public void Damage(int dmg)
    {
      curhealth -= dmg;
      anim.SetTrigger("Damaged");
    }
}

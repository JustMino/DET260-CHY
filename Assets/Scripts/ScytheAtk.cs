using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheAtk : MonoBehaviour
{
  Vector3 rot;
  public float rotspd = 2.5f;

  public float speed = 1.0f;
  GameObject player;
  bool goback = false;
  public GameObject scythe;
  int hitdmg = 100;
    // Start is called before the first frame update
    void Start()
    {
      scythe = GameObject.Find("Calli-Scythe");
      rot = transform.rotation.ToEulerAngles();
      player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        rot = new Vector3 (90, 0, rot.z - rotspd);
        transform.rotation = Quaternion.Euler(rot);
        if (!goback)
        {
          float dist = Vector3.Distance(player.transform.position, transform.position);
          if(dist > 25f)
          {
            goback = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
          }
        }
        else
        {
          var step = speed * Time.deltaTime;
          transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
    }

    void OnTriggerEnter(Collider other)
    {
      if(goback && other.tag == "Player")
      {
        WeaponAttack atkscript = scythe.GetComponent<WeaponAttack>();
        atkscript.thrownscythe = false;
        atkscript.swordattacking = false;
        Destroy(gameObject);
      }
      if (other.gameObject.layer == 6)
      {
        EnemyHealth enemyhealth = other.GetComponent<EnemyHealth>();
        enemyhealth.Damage(hitdmg);
      }
    }
}

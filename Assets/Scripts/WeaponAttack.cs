using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
  GameManager GM;
  public int weaponID;
  // 1 = Kiara Sword
  // 2 = Kiara Shield
  // 3 = Ina Book
  // 4 = Calli Scythe

  GameObject mainCamera;
  [SerializeField]
  LayerMask enemyLayers;
  GameObject player;

  // Kiara Sword variables
    public bool swordattacking = false;
    [SerializeField]
    int sworddmg = 15;

  // Kiara Shield variables
    bool down = false;
    Animator anim;

  // Ina Book variables
    GameObject MagicBullet;
    GameObject FirePoint;
    [SerializeField]
    float bulletforce = 50f;

  //Calli Scythe variables
    [SerializeField]
    ParticleSystem particlesys;
    [SerializeField]
    int sythedmg = 50;
    [SerializeField]
    GameObject Scythe;
    [SerializeField]
    float ScytheForce = 50f;
    MeshRenderer meshrenderer;
    public bool thrownscythe = false;
    PlayerHealth playerhealth;
    // Start is called before the first frame update
    void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        weaponID = GM.WeaponID(gameObject.name);
        player = GameObject.Find("Player");
        playerhealth = player.GetComponent<PlayerHealth>();
        FirePoint = GameObject.Find("FirePoint");
        MagicBullet = Resources.Load<GameObject>("Bullet");
        Scythe = Resources.Load<GameObject>("Scythe");
        mainCamera = GameObject.Find("Main Camera");
        if (weaponID == 1 || weaponID == 4) anim = GetComponent<Animator>();
        if (weaponID == 4) meshrenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      if (weaponID == 2) SubAttack();
      if (Input.GetButtonDown("Fire1"))
      {
        PrimAttack();
      }
      else if (Input.GetButton("Fire2"))
      {
        down = true;
        SubAttack();
      }
      if (Input.GetButtonUp("Fire2")) down = false;
      if (!thrownscythe && weaponID == 4) meshrenderer.enabled = true;
      if (weaponID == 4)
      {
        if (thrownscythe || swordattacking) GM.attacking = true;
        else GM.attacking = false;
      }
      if (weaponID == 1) GM.attacking = swordattacking;
      if (weaponID == 2) playerhealth.blocking = down;
      if (weaponID == 2 && down) particlesys.Play(true);
      else particlesys.Stop(true);
    }

    void PrimAttack()
    {
      if (weaponID == 1 && !swordattacking)
      {
        swordattacking = true;
        anim.SetTrigger("KiaraSwordSwing");
        particlesys.Play(true);
        Collider[] hit = Physics.OverlapSphere(player.transform.position, 1.5f, enemyLayers);
        foreach(var hitCollider in hit)
        {
          EnemyHealth enemyhealth = hitCollider.GetComponent<EnemyHealth>();
          enemyhealth.Damage(sworddmg);
        }
      }
      else if (weaponID == 2)
      {
        return;
      }
      else if (weaponID == 3)
      {
        GameObject tempbul;
        tempbul = Instantiate(MagicBullet, FirePoint.transform.position, mainCamera.transform.rotation);
        Rigidbody bulrb = tempbul.GetComponent<Rigidbody>();
        bulrb.AddForce(tempbul.transform.forward * bulletforce, ForceMode.Impulse);
      }
      else if (weaponID == 4 && !thrownscythe && !swordattacking)
      {
        swordattacking = true;
        anim.SetTrigger("CalliScytheSwing");
        Collider[] hit = Physics.OverlapSphere(player.transform.position, 3.5f, enemyLayers);
        particlesys.Play(true);
        foreach(var hitCollider in hit)
        {
          EnemyHealth enemyhealth = hitCollider.GetComponent<EnemyHealth>();
          enemyhealth.Damage(sythedmg);
        }
      }
    }

    void SubAttack()
    {
      if (weaponID == 1)
      {
        return;
      }
      else if (weaponID == 2)
      {
        if (down)
        {
          transform.localRotation = Quaternion.Euler(new Vector3 (-4f, -20f, 0));
          transform.localPosition = new Vector3 (-0.4f, -0.3f, 0.7f);
        }
        else
        {
          transform.localRotation = Quaternion.Euler(new Vector3 (-4f, -35f, 0));
          transform.localPosition = new Vector3 (-0.66f, -0.3f, 0.7f);
        }
      }
      else if (weaponID == 3)
      {

      }
      else if (weaponID == 4 && !thrownscythe)
      {
        thrownscythe = true;
        meshrenderer.enabled = false;
        GameObject tempbul;
        tempbul = Instantiate(Scythe, FirePoint.transform.position, mainCamera.transform.rotation);
        Rigidbody bulrb = tempbul.GetComponent<Rigidbody>();
        bulrb.AddForce(tempbul.transform.forward * ScytheForce, ForceMode.Impulse);
      }
    }


    void resetswing()
    {
      swordattacking = false;
      particlesys.Stop(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
  public GameObject[] allPlayerWeapons;
  public GameObject[] playersShotgun;
  int weaponid;
  GameManager GM;

  void OnEnable()
  {
    if (gameObject.name == "KiaraWeapon") weaponid = 1;
    else if (gameObject.name == "InaWeapon-Closed") weaponid = 2;
    else if (gameObject.name == "Calli-PickUp") weaponid = 3;
  }

   void Start()
   {
      GM = GameObject.Find("GameManager").GetComponent<GameManager>();
      // hide it right away, because we said so
      foreach(GameObject go in playersShotgun)
      {
        go.SetActive(false);
      }
   }

   void OnTriggerEnter(Collider other) {
      if (other.tag == "Player" && !GM.attacking) {
          PlayerWeaponSwitch pws = other.GetComponent<PlayerWeaponSwitch>();
          pws.Unlock(weaponid);
          GM.ChangeWeaponStats(weaponid);
          foreach(GameObject go in allPlayerWeapons)
          {
            go.SetActive(false);
          }
          // tell player to unhide it's shotgun
          foreach(GameObject go in playersShotgun)
          {
            go.SetActive(true);
          }
          Destroy(gameObject);
      }
  }
}

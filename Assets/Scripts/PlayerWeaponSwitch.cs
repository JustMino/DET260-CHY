using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSwitch : MonoBehaviour
{
  private bool KiaraUnlocked = false;
  private bool InaUnlocked = false;
  private bool CalliUnlocked = false;
  GameManager GM;

  public GameObject[] allPlayerWeapons;
  // 1 = Kiara Sword
  // 2 = Kiara Shield
  // 3 = Ina Book
  // 4 = Calli Scythe

  public enum Weapon
  {
    None,
    Kiara,
    Ina,
    Calli
  }

  public Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
      GM = GameObject.Find("GameManager").GetComponent<GameManager>();
      GM.curweapon = "None";
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetButtonDown("SwitchWeapon"))
      {
        switch (weapon)
        {
          case Weapon.None:
            if (KiaraUnlocked) ChangeWeapon(1);
            else if (InaUnlocked) ChangeWeapon(2);
            else if (CalliUnlocked) ChangeWeapon(3);
            break;
          case Weapon.Kiara:
            if (InaUnlocked) ChangeWeapon(2);
            else if (CalliUnlocked) ChangeWeapon(3);
            break;
          case Weapon.Ina:
            if (CalliUnlocked) ChangeWeapon(3);
            else if (KiaraUnlocked) ChangeWeapon(1);
            break;
          case Weapon.Calli:
            if (KiaraUnlocked) ChangeWeapon(1);
            else if (InaUnlocked) ChangeWeapon(2);
            break;
          default:
            break;
        }
      }
    }

    public void Unlock(int id)
    {
      if (id == 1)
      {
        GM.curweapon = "Kiara's Sword and Shield";
        KiaraUnlocked = true;
      }
      else if (id == 2)
      {
        GM.curweapon = "Ina's Magic Book";
        InaUnlocked = true;
      }
      else if (id == 3)
      {
        GM.curweapon = "Calli's Scythe";
        CalliUnlocked = true;
      }
    }

    public void ChangeWeapon(int weaponID)
    {
      if (!GM.attacking)
      {
        if (weaponID == 0)
        {
          weapon = Weapon.None;
          GM.curweapon = "None";
          allPlayerWeapons[0].SetActive(false);
          allPlayerWeapons[1].SetActive(false);
          allPlayerWeapons[2].SetActive(false);
          allPlayerWeapons[3].SetActive(false);
        }
        else if (weaponID == 1)
        {
          weapon = Weapon.Kiara;
          GM.curweapon = "Kiara's Sword and Shield";
          allPlayerWeapons[0].SetActive(true);
          allPlayerWeapons[1].SetActive(true);
          allPlayerWeapons[2].SetActive(false);
          allPlayerWeapons[3].SetActive(false);
        }
        else if (weaponID == 2)
        {
          weapon = Weapon.Ina;
          GM.curweapon = "Ina's Magic Book";
          allPlayerWeapons[0].SetActive(false);
          allPlayerWeapons[1].SetActive(false);
          allPlayerWeapons[2].SetActive(true);
          allPlayerWeapons[3].SetActive(false);
        }
        else if (weaponID == 3)
        {
          weapon = Weapon.Calli;
          GM.curweapon = "Calli's Scythe";
          allPlayerWeapons[0].SetActive(false);
          allPlayerWeapons[1].SetActive(false);
          allPlayerWeapons[2].SetActive(false);
          allPlayerWeapons[3].SetActive(true);
          WeaponAttack atkscript = allPlayerWeapons[3].GetComponent<WeaponAttack>();
          atkscript.swordattacking = false;
        }
    }
  }
}

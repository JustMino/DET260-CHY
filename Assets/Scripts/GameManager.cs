using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public int killcount = 0;

  public string curweapon = "";

  public bool attacking = false;
  public bool objectivedone = false;

  public bool[] weapons = {false, false, false};

  public int WeaponID(string name)
  {
    int ID = 0;
    if (name == "Kiara-Sword") ID = 1;
    else if (name == "Kiara-Shield") ID = 2;
    else if (name == "InaWeapon-Open") ID = 3;
    else if (name == "Calli-Scythe") ID = 4;

    return ID;
  }

  public bool won = false;
  public bool continued = false;

  void Update()
  {
    if (!objectivedone)
    {
      if (CheckAllTrue()) objectivedone = true;
    }
    if (killcount >= 21 && objectivedone)
    {
      won = true;
    }
  }

  public void ChangeWeaponStats(int ID)
  {
    weapons[ID-1] = true;
  }

  private bool CheckAllTrue()
  {
    for (int i = 0; i < weapons.Length; i++)
    {
      if (!weapons[i])
      {
        return false;
      }
    }
    return true;
  }

  public void ContinuePlayer()
  {
    continued = true;
  }

}

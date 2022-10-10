using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
  [SerializeField] GameObject FinishFacade;
  [SerializeField] GameObject WonMenu;
  LevelUnlocked LU;
  // Start is called before the first frame update
  void Start()
  {
    LU = GameObject.Find("LevelUnlock").GetComponent<LevelUnlocked>();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
      UnlockNextLevel();
      Time.timeScale = 0f;
      FinishFacade.SetActive(true);
      WonMenu.SetActive(true);
    }
  }

  void UnlockNextLevel()
  {
    if (LU.Lvl4unlocked) LU.Lvl5unlocked = true;
    else if (LU.Lvl3unlocked) LU.Lvl4unlocked = true;
    else if (LU.Lvl2unlocked) LU.Lvl3unlocked = true;
    else LU.Lvl2unlocked = true;
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlocked : MonoBehaviour
{
  public static LevelUnlocked Instance;
  void Awake()
  {
    if (Instance == null)
    {
      DontDestroyOnLoad(gameObject);
      Instance = this;
    }
    else if (Instance != this)
    {
      Destroy(gameObject);
    }
  }

  public bool Lvl2unlocked = false;
  public bool Lvl3unlocked = false;
  public bool Lvl4unlocked = false;
  public bool Lvl5unlocked = false;
}

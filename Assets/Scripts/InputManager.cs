using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  KeyCode[] secretcombo = {KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A, KeyCode.Return};

  [SerializeField] GameObject[] levels;

  [SerializeField] int comboplace = 0;
  [SerializeField] bool stop = false;

  [SerializeField] IEnumerator combostop;
  // Start is called before the first frame update
  void Start()
  {
    combostop = ComboStopper();
    foreach (GameObject item in levels)
    {
      item.SetActive(false);
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (!stop)
    {
      if (Input.GetKeyDown(secretcombo[comboplace]))
      {
        StopCoroutine(combostop);
        combostop = ComboStopper();
        comboplace++;
        if (comboplace < 11)
        {
          StartCoroutine(combostop);
        }
        else stop = true;
      }
    }
    else
    {
      foreach (GameObject item in levels)
      {
        item.SetActive(true);
      }
    }
  }

  IEnumerator ComboStopper()
  {
    yield return new WaitForSeconds(0.5f);
    comboplace = 0;
  }
}

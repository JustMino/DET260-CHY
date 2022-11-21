using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadInput : MonoBehaviour
{
  public TextMeshProUGUI inputpad;

  GameObject player;

  InventoryManager im;

  public string currentinput = "";
    // Start is called before the first frame update
    void Start()
    {
      im = GameObject.Find("GameManager").GetComponent<InventoryManager>();
      player = GameObject.Find("FPC");
    }

    // Update is called once per frame
    void Update()
    {
      inputpad.text = currentinput;
      if (Input.GetKeyDown(KeyCode.Escape))
      {
        player.BroadcastMessage("CloseKeypad");

      }
    }

    public void InputNum(int number)
    {
      if (number == 11) checkpass();
      else if (number == 10) currentinput = currentinput.Remove(currentinput.Length-1);
      else if (currentinput.Length < 4)
      {
        currentinput += number.ToString();
      }
    }

    void checkpass()
    {
      if (currentinput == "6317" && (currentinput.Length == 4))
      {
        StartCoroutine(correctinput());
      }
      else
      {
        StartCoroutine(resetinput());
      }
    }

    IEnumerator resetinput()
    {
      inputpad.characterSpacing = 60;
      for (int i = 0; i < 3; i++)
      {
        currentinput = "DENIED";
        yield return new WaitForSeconds(0.1f);
        currentinput = "";
        yield return new WaitForSeconds(0.1f);
      }
      inputpad.characterSpacing = 100;
    }
    IEnumerator correctinput()
    {
      inputpad.characterSpacing = 30;
      for (int i = 0; i < 3; i++)
      {
        currentinput = "ACCEPTED";
        yield return new WaitForSeconds(0.1f);
        currentinput = "";
        yield return new WaitForSeconds(0.1f);
      }
      currentinput = "ACCEPTED";
      yield return new WaitForSeconds(1.5f);
      im.GotKey4 = true;
      player.BroadcastMessage("CloseKeypad");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
  public enum CardType
  {
    Key1,
    Key2,
    Key3
  }

  InventoryManager im;

  public CardType card;

  GameObject canvas;

  void Start()
  {
    im = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    canvas = GameObject.Find("Canvas");
  }

  void OnTriggerEnter(Collider c)
  {
    if (c.tag.Equals("GameController"))
    {
      switch (card)
      {
        case CardType.Key1:
          canvas.BroadcastMessage("UpdateText", 1);
          break;
        case CardType.Key2:
          canvas.BroadcastMessage("UpdateText", 2);
          break;
        case CardType.Key3:
          canvas.BroadcastMessage("UpdateText", 3);
          break;
      }
    }
  }

  void OnTriggerStay(Collider c)
  {
    if (c.tag.Equals("GameController") && Input.GetKey(KeyCode.E))
    {
      switch (card)
      {
        case CardType.Key1:
          im.GotKey1 = true;
          canvas.BroadcastMessage("UpdateText", 4);
          Destroy(gameObject);
          break;
        case CardType.Key2:
          im.GotKey2 = true;
          canvas.BroadcastMessage("UpdateText", 4);
          Destroy(gameObject);
          break;
        case CardType.Key3:
          im.GotKey3 = true;
          canvas.BroadcastMessage("UpdateText", 4);
          Destroy(gameObject);
          break;
      }
    }
  }

  void OnTriggerExit(Collider c)
  {
    if (c.tag.Equals("GameController"))
    {
      canvas.BroadcastMessage("UpdateText", 4);
    }
  }

}

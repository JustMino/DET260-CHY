using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasUpdater : MonoBehaviour
{

  public TextMeshProUGUI popup;

  public enum InteractState
  {
    PickupKey1,
    PickupKey2,
    PickupKey3,
    PickupKey4,
    OpenDoor,
    DONE
  }

  public enum NoAccessCard
  {
    NoKey1,
    NoKey2,
    NoKey3,
    NoKey4
  }

  public InteractState textstate;

  public NoAccessCard nocard;

  public bool showtext = false;
  public bool error = false;

  InventoryManager im;

  [SerializeField] GameObject Key1Icon;
  [SerializeField] GameObject Key2Icon;
    // Start is called before the first frame update
    void Start()
    {
      im = GameObject.Find("GameManager").GetComponent<InventoryManager>();
      Key1Icon.SetActive(im.GotKey1);
      Key2Icon.SetActive(im.GotKey2);
    }

    // Update is called once per frame
    void Update()
    {
      Key1Icon.SetActive(im.GotKey1);
      Key2Icon.SetActive(im.GotKey2);
        if (!showtext) popup.text = "";
        else if (error)
        {
          switch (nocard)
          {
            case NoAccessCard.NoKey1:
              popup.text = "Level 1 Keycard required.";
              break;
            case NoAccessCard.NoKey2:
              popup.text = "Level 2 Keycard required.";
              break;
            case NoAccessCard.NoKey3:
              popup.text = "Level 3 Keycard required.";
              break;
            case NoAccessCard.NoKey4:
              popup.text = "Keypad Input required.";
              break;
          }
        }
        else
        {
          switch (textstate)
          {
            case InteractState.PickupKey1:
              popup.text = "Press E to pick up Level 1 Keycard";
              break;
            case InteractState.PickupKey2:
              popup.text = "Press E to pick up Level 2 Keycard";
              break;
            case InteractState.PickupKey3:
              popup.text = "Press E to pickup Level 3 Keycard";
              break;
            case InteractState.PickupKey4:
              popup.text = "Press E to input Keycode";
              break;
            case InteractState.OpenDoor:
              popup.text = "Press E to open Door";
              break;
            case InteractState.DONE:
              popup.text = "CONGRATULATIONS GAME COMPLETE";
              break;
          }
        }
    }

    void UpdateText(int cardtype)
    {
      showtext = true;
      if (cardtype == 1) textstate = InteractState.PickupKey1;
      if (cardtype == 2) textstate = InteractState.PickupKey2;
      if (cardtype == 3) textstate = InteractState.PickupKey3;
      if (cardtype == 4) showtext = false;
      if (cardtype == 5) textstate = InteractState.OpenDoor;
      if (cardtype == 6) textstate = InteractState.PickupKey4;
    }

    IEnumerator NoAccess(int cardtype)
    {
      if (cardtype == 1) nocard = NoAccessCard.NoKey1;
      if (cardtype == 2) nocard = NoAccessCard.NoKey2;
      if (cardtype == 3) nocard = NoAccessCard.NoKey3;
      if (cardtype == 4) nocard = NoAccessCard.NoKey4;
      error = true;
      yield return new WaitForSeconds(0.5f);
      error = false;
    }

    void GameComplete()
    {
      showtext = true;
      textstate = InteractState.DONE;
    }
}

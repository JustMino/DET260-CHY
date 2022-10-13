using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
  GameManager GM;

  TextMeshProUGUI curweapontxt;
  TextMeshProUGUI killcounttxt;
  TextMeshProUGUI healthtxt;
  TextMeshProUGUI objectivetxt;

  GameObject WinText;

  public PlayerHealth player;

  bool pressed = false;


    // Start is called before the first frame update
    void Start()
    {
      GM = GameObject.Find("GameManager").GetComponent<GameManager>();
      curweapontxt = transform.Find("CurrentWeapon").GetComponent<TextMeshProUGUI>();
      killcounttxt = transform.Find("KillCount").GetComponent<TextMeshProUGUI>();
      healthtxt = transform.Find("Health").GetComponent<TextMeshProUGUI>();
      objectivetxt = transform.Find("Objectives").GetComponent<TextMeshProUGUI>();
      WinText = transform.Find("LightGrey").gameObject;
      WinText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      curweapontxt.text = "Current Weapon: " + GM.curweapon;
      killcounttxt.text = "Enemies Killed: " + GM.killcount;
      healthtxt.text = "Health:\n" + player.curhealth + " / " + player.maxhealth;
      string temp = "Objectives:\nKill all 21 enemies:\n" + GM.killcount + " / 21";
      temp += "\n\nPick up all weapons:\n";
      temp += GM.weapons[0] ? "Kiara's Sword and Shield: 1/1\n" : "Kiara's Sword and Shield: 0/1\n";
      temp += GM.weapons[1] ? "Ina's Magic Book: 1/1\n" : "Ina's Magic Book: 0/1\n";
      temp += GM.weapons[2] ? "Calli's Scythe: 1/1\n" : "Calli's Scythe: 0/1\n";
      objectivetxt.text = temp;
      if (GM.won && !GM.continued)
      {
        WinText.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
      }
      if (GM.continued && !pressed)
      {
        pressed = true;
        Time.timeScale = 1f;
        WinText.SetActive(false);
      }
      if (pressed)
      {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
      }
    }
}

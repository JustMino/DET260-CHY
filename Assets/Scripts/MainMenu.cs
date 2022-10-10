using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


  void Start()
  {

  }

  public void StartButton()
  {
    SceneManager.LoadScene("LevelSelect", LoadSceneMode.Additive);
  }
}

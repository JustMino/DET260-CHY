using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
  [SerializeField] GameObject FinishFacade;
  [SerializeField] GameObject FailedMenu;

  GameManager GM;

  void Start()
  {
    GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    Time.timeScale = 1f;
  }

  void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.tag == "Player")
    {
      // StartCoroutine(slowtime());
      Time.timeScale = 0f;
      GM.GameOver = true;
      FinishFacade.SetActive(true);
      FailedMenu.SetActive(true);
    }
  }

  IEnumerator slowtime()
  {
    while (Time.timeScale > 0f)
    {
      Time.timeScale -= 0.1f;
      yield return new WaitForSeconds(0.01f);
    }
  }
}

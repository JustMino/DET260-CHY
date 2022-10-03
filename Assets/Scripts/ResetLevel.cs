using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.tag == "Player")
      {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
    }
}

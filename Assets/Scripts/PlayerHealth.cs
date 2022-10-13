using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
  public int curhealth;
  public int maxhealth = 100;
  public bool blocking = false;
    // Start is called before the first frame update
    void Start()
    {
      curhealth = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
      if (curhealth <= 0)
      {
        curhealth = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
    }

    public void Damage(int dmg)
    {
      if (blocking) curhealth -= (dmg/2);
      else curhealth -= dmg;
    }
}

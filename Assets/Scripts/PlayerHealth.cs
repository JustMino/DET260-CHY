using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
  public int curhealth;
  public int maxhealth = 100;
  Animator anim;
    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponent<Animator>();
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
      anim.SetTrigger("HitToBody");
      curhealth -= dmg;
    }

    public void HeadShot(int dmg)
    {
      anim.SetTrigger("HitToHead");
      curhealth -= dmg * 2;
    }
}

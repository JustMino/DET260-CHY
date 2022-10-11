using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelectMenu : MonoBehaviour
{
  LevelUnlocked LU;
  GameObject Lvl1Button;
  GameObject Lvl2Button;
  GameObject Lvl3Button;
  GameObject Lvl4Button;
  GameObject Lvl5Button;
  TextMeshProUGUI lockedmsg;

  [SerializeField] Sprite locked;
  [SerializeField] Sprite unlocked;
  // Start is called before the first frame update
  void Start()
  {
    LU = GameObject.Find("LevelUnlock").GetComponent<LevelUnlocked>();
    Lvl1Button = GameObject.Find("Lvl1");
    Lvl2Button = GameObject.Find("Lvl2");
    Lvl3Button = GameObject.Find("Lvl3");
    Lvl4Button = GameObject.Find("Lvl4");
    Lvl5Button = GameObject.Find("Lvl5");
    lockedmsg = GameObject.Find("LockedMessage").GetComponent<TextMeshProUGUI>();
    Lvl2Button.GetComponent<Image>().sprite = (LU.Lvl2unlocked) ? unlocked : locked;
    Lvl3Button.GetComponent<Image>().sprite = (LU.Lvl3unlocked) ? unlocked : locked;
    Lvl4Button.GetComponent<Image>().sprite = (LU.Lvl4unlocked) ? unlocked : locked;
    Lvl5Button.GetComponent<Image>().sprite = (LU.Lvl5unlocked) ? unlocked : locked;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(LockedMsgPopUp(1f, lockedmsg));
  }

  public void GoToLvl1()
  {
    SceneManager.LoadScene("Lvl1");
  }

  public void GoToLvl2()
  {
    if (LU.Lvl2unlocked) SceneManager.LoadScene("Lvl2");
    else StartCoroutine(LockedMsgPopUp(1f, lockedmsg));
  }

  public void GoToLvl3()
  {
    if (LU.Lvl3unlocked) SceneManager.LoadScene("Lvl3");
    else StartCoroutine(LockedMsgPopUp(1f, lockedmsg));
  }

  public void GoToLvl4()
  {
    if (LU.Lvl4unlocked) SceneManager.LoadScene("Lvl4");
    else StartCoroutine(LockedMsgPopUp(1f, lockedmsg));
  }

  public void GoToLvl5()
  {
    if (LU.Lvl4unlocked) SceneManager.LoadScene("Lvl5");
    else StartCoroutine(LockedMsgPopUp(1f, lockedmsg));
  }

  public void GoToLvlX()
  {
    SceneManager.LoadScene("LvlX");
  }

  public IEnumerator LockedMsgPopUp(float t, TextMeshProUGUI i)
  {
    i.fontSize = 0;
    while (i.fontSize < 140f)
    {
      i.fontSize += (Time.deltaTime / t)*420f;
      yield return new WaitForSeconds(0.000001f);
    }
    yield return new WaitForSeconds(0.5f);
    while (i.fontSize > 0f)
    {
      i.fontSize -= (Time.deltaTime / t)*420f;
      yield return new WaitForSeconds(0.000001f);
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasUpdater : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI midairtruck;
  public TextMeshProUGUI airtime;
  [SerializeField] GameObject Facade;
  [SerializeField] GameObject EndMenu;
  [SerializeField] GameObject FailedMenu;
  GameManager GM;
  LevelUnlocked LU;

  GameObject IndividualPoints;
  TextMeshProUGUI finalpoints;

  bool adding = false;

  void Start()
  {
    GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    LU = GameObject.Find("LevelUnlock").GetComponent<LevelUnlocked>();
    IndividualPoints = GameObject.Find("IndividualPoints");
    finalpoints = GameObject.Find("Final Points").GetComponent<TextMeshProUGUI>();
    Facade.SetActive(false);
    EndMenu.SetActive(false);
    FailedMenu.SetActive(false);
    airtime.color = new Color(airtime.color.r, airtime.color.g, airtime.color.b, 0);
    midairtruck.color = new Color(midairtruck.color.r, midairtruck.color.g, midairtruck.color.b, 0);
    IndividualPoints.SetActive(false);
    finalpoints.gameObject.SetActive(false);
  }

  void Update()
  {
    UpdateAirtimeScore();
    if (EndMenu.activeInHierarchy)
    {
      float xpos = Screen.width/2f + ((Screen.width/2f-Input.mousePosition.x)*-0.025f);
      float ypos = Screen.height/2f + ((Screen.height/2f-Input.mousePosition.y)*-0.025f);
      EndMenu.transform.position = new Vector3 (xpos, ypos, 0);
    }
    if (FailedMenu.activeInHierarchy)
    {
      float xpos = Screen.width/2f + ((Screen.width/2f-Input.mousePosition.x)*-0.025f);
      float ypos = Screen.height/2f + ((Screen.height/2f-Input.mousePosition.y)*-0.025f);
      FailedMenu.transform.position = new Vector3 (xpos, ypos, 0);
    }
  }

  void UpdateAirtimeScore()
  {
    int temp = (int)(GM.airtimescore * 100);
    float temp1 = (float)(temp/100f);
    airtime.text = "Airtime Score: " + temp1.ToString();
  }

  public void ShowAirTime()
  {
    float xpos = (Random.Range(Screen.width*0.1f, Screen.width*0.9f));
    float ypos = (Random.Range(Screen.width*0.1f, Screen.height*0.9f));
    airtime.gameObject.transform.position = new Vector3 (xpos, ypos, 0f);
    StartCoroutine(FadeTextToFullAlpha(0.1f, airtime));
  }

  public void FinishAirTime()
  {
    StartCoroutine(FadeTextToZeroAlpha(0.1f, airtime));
  }

  public void JumpMidAir()
  {
    float xpos = (Random.Range(Screen.width*0.1f, Screen.width*0.9f));
    float ypos = (Random.Range(Screen.width*0.1f, Screen.height*0.9f));
    midairtruck.gameObject.transform.position = new Vector3 (xpos, ypos, 0f);
    midairtruck.text = "Midair Jump Bonus +" + GM.midairtruckjumppoints.ToString();
    GM.totalmidairpoints += GM.midairtruckjumppoints;
    StartCoroutine(FadeInAndOut(midairtruck));
  }

  IEnumerator FadeInAndOut(TextMeshProUGUI t)
  {
    StartCoroutine(FadeTextToFullAlpha(1f, t));
    yield return new WaitForSecondsRealtime(2);
    StartCoroutine(FadeTextToZeroAlpha(1f, t));
  }

  public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
  {
    i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
    while (i.color.a < 1.0f)
    {
      i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.unscaledDeltaTime / t));
      yield return null;
    }
  }

  public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
  {
    i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
    while (i.color.a > 0.0f)
    {
      i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.unscaledDeltaTime / t));
      yield return null;
    }
  }

  public void NextLevel()
  {
    LU.Lvl2unlocked = true;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
  }

  public void RestartLevel()
  {
    GM.GameOver = false;
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void BackToMenu()
  {
    SceneManager.LoadScene("Menu");
  }

  public IEnumerator CalculatePoints()
  {
    finalpoints.gameObject.SetActive(true);
    PointAddAnim("AIR TIME: ", GM.totalairtimescore, true);
    yield return new WaitUntil(() => !adding);
    PointAddAnim("TIME BONUS: ", timescore(), true);
    yield return new WaitUntil(() => !adding);
    PointAddAnim("MIDAIR BONUS: ", GM.totalmidairpoints, true);
  }

  void PointAddAnim(string s, int p, bool t)
  {
    adding = true;
    IndividualPoints.transform.position = new Vector3 (Screen.width/2, (Screen.height/2)-140f, 0f);
    TextMeshProUGUI txt = IndividualPoints.GetComponent<TextMeshProUGUI>();
    txt.text = s + "+0";
    IndividualPoints.SetActive(true);
    GM.totalscore += p;
    StartCoroutine(CountUp(txt, s, p, t));
  }

  IEnumerator CountUp(TextMeshProUGUI txt, string s, int p, bool t)
  {
    for (int i = 0; i < 25; i++)
    {
      txt.text = (t) ? (s + "+" + p/(25-i)) : (s + p/(25-i));
      yield return new WaitForSecondsRealtime(0.015f);
    }
    yield return new WaitForSecondsRealtime(1.0f);
    if (t) StartCoroutine(MoveText());
  }

  IEnumerator MoveText()
  {
    for (int i = 0; i < 11; i++)
    {
      IndividualPoints.transform.position = new Vector3 (Screen.width/2, (Screen.height/2)-140f - (5*i), 0f);
      yield return new WaitForSecondsRealtime(0.01f);
    }
    StartCoroutine(CountUp(finalpoints, "TOTAL SCORE: ", GM.totalscore, false));
    StartCoroutine(LockedMsgPopUp(1f, IndividualPoints.GetComponent<TextMeshProUGUI>()));
    IndividualPoints.SetActive(false);
    IndividualPoints.GetComponent<TextMeshProUGUI>().fontSize = 36f;
    adding = false;
  }

  IEnumerator LockedMsgPopUp(float t, TextMeshProUGUI i)
  {
    while (i.fontSize > 0f)
    {
      i.fontSize -= (Time.deltaTime / t)*420f;
      yield return new WaitForSeconds(0.000001f);
    }
  }

  int timescore()
  {
    return (int)((60f/Time.timeSinceLevelLoad)*1000f);
  }
}

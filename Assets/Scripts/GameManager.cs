using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  void Awake()
  {
    Time.timeScale = 1f;
    if(Instance == null)
    {
      DontDestroyOnLoad(gameObject);
      Instance = this;
    }
    else if (Instance != this)
    {
      Destroy(gameObject);
    }
  }

  PlayerController player;
  CanvasUpdater canvasup;
  [Header("Scores")]
  public int totalscore = 0;
  public float airtimescore = 0;
  public int midairtruckjumppoints = 300;
  [SerializeField]bool shownairtime = false;

  [Header("Player")]
  public bool GameOver = false;
  public bool TimeStop = false;

  void Start()
  {
    player = GameObject.Find("Player").GetComponent<PlayerController>();
    canvasup = GameObject.Find("Canvas").GetComponent<CanvasUpdater>();
  }

  void Update()
  {
    if (GameOver)
    {
      canvasup.airtime.color = new Color(canvasup.airtime.color.r, canvasup.airtime.color.g, canvasup.airtime.color.b, 0);
    }
    else
    {
      if (player.grounded && shownairtime) hideAirtime();
      if (!player.grounded) airtimescore += Time.deltaTime;
      if (!shownairtime && airtimescore > 1.25f)
      {
        shownairtime = true;
        canvasup.ShowAirTime();
      }
    }
  }

  void hideAirtime()
  {
    shownairtime = false;
    totalscore += (!GameOver) ? (int)(airtimescore*100f) : 0;
    airtimescore = 0;
    canvasup.FinishAirTime();
  }



}

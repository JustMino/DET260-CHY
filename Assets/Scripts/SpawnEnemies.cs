using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
  [SerializeField]
  GameObject[] enemies;
  GameObject player;

  bool started = false;

  GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
      GM = GameObject.Find("GameManager").GetComponent<GameManager>();
      player = GameObject.Find("Player");

    }

    void Update()
    {
      if (!started && GM.continued)
      {
        started = true;
        InvokeRepeating("SpawnEnemy", 0f, 1f);
      }
    }

    void SpawnEnemy()
    {
      var targetpos = player.transform.position;
      targetpos.y = 50f;
      transform.position = targetpos;
      Instantiate(enemies[Random.Range(0,enemies.Length-1)], new Vector3 (targetpos.x + Random.Range(-30f, 30f), 0, targetpos.z + Random.Range(-30f, 30f)), Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemies : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    private void Update()
    {
        if (GameObject.Find("GameController").GetComponent<GameController>().EnemiesGeneratedFinalRound >= 20)
            CancelInvoke("ReleaseEnemy");
    }

    public void ReleaseEnemyRepeat(float delay)
    {
        InvokeRepeating("ReleaseEnemy", delay, 10.0f);
    }

    void ReleaseEnemy()
    {
        if (GameObject.Find("GameController").GetComponent<GameController>().EnemiesFinalRound <= 6)
        {
            GameObject enemy = Instantiate(enemies[Random.Range(0, 4)], new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
            enemy.GetComponent<Enemy>().FinalRound = true;
            GameObject.Find("GameController").GetComponent<GameController>().EnemiesFinalRoundInc();
        }
    }
}

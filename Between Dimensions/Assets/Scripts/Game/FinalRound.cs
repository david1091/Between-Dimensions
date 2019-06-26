using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRound : MonoBehaviour
{

    private bool round = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && !round)
        {
            float delay = 0.0f;
            round = true;
            foreach (RespawnEnemies i in transform.GetComponentsInChildren<RespawnEnemies>())
            {
                i.ReleaseEnemyRepeat(delay);
                delay += 0.25f;
            }
        }
    }
}

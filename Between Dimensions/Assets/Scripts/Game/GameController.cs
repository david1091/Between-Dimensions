using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Dimension { get; private set; }
    public int EnemiesLeft { get; private set; }
    public int EnemiesKilled { get; private set; }
    public int EnemiesFinalRound { get; private set; }
    public int EnemiesLeftFinalRound { get; private set; } = 40;
    public int EnemiesGeneratedFinalRound { get; private set; }
    public bool Alive { get; private set; } = true;

    void Start()
    {
        Dimension = 0;
    }

    public void EnemiesKilledInc()
    {
        EnemiesKilled++;
    }

    public void SetDimension(int dimension)
    {
        Dimension = dimension;
    }

    public void SetAlive(bool alive)
    {
        Alive = alive;
    }

    public void EnemiesFinalRoundInc()
    {
        EnemiesFinalRound++;
        EnemiesGeneratedFinalRound++;
    }

    public void EnemiesFinalRoundDec()
    {
        EnemiesFinalRound--;
        EnemiesLeftFinalRound--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    private readonly string enemyName;
    public float Life { get; private set; }
    public float Speed { get; private set; }
    public int Dimension { get; private set; } 
    public float Damage { get; private set; }
    public float ShootingSpeed { get; private set; }

    public EnemyStats (string enemyName)
    {
        this.enemyName = enemyName.Split('_')[0];
        SetAttributes();
    }

    void SetAttributes()
    {
        switch (enemyName)
        {
            case ("Masked"):
                Life = 10.0f;
                Speed = 4.0f;
                Damage = 1.0f;
                Dimension = 0;
                ShootingSpeed = 1.0f;
                break;
            case ("Ogre"):
                Life = 20.0f;
                Speed = 2.0f;
                Damage = 3.0f;
                Dimension = 0;
                ShootingSpeed = 0.5f;
                break;
            case ("Synthetic"):
                Life = 10.0f;
                Speed = 4.0f;
                Damage = 1.0f;
                Dimension = 1;
                ShootingSpeed = 1.0f;
                break;
            case ("ATST"):
                Life = 20.0f;
                Speed = 2.0f;
                Damage = 3.0f;
                Dimension = 1;
                ShootingSpeed = 0.5f;
                break;
        }
    }
}

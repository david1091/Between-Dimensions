using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    //Stats
    public float Life { get; private set; }
    public int Dimension { get; private set; }
    public float ShootingSpeed { get; private set; }
    public bool FinalRound = false;
    private float damage;

    //Life Bar
    [SerializeField]
    private Image LifeBar;
    [SerializeField]
    private Text lifeText;

    //Loot
    [SerializeField]
    private GameObject potion;
    [SerializeField]
    private GameObject[] weapons;

    void Start()
    {
        EnemyStats enemyStats = new EnemyStats(gameObject.name);
        SetAttributes(enemyStats);
        EnemyOpacity();
    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("ChangeDimension")) EnemyOpacity();
        transform.GetComponentInChildren<Weapon>().SetEnemyDamage(damage);
    }

    void SetAttributes(EnemyStats stats)
    {
        Life = stats.Life;
        damage = stats.Damage;
        Dimension = stats.Dimension;
        ShootingSpeed = stats.ShootingSpeed;
        //EnemyController
        GetComponent<EnemyController>().SetAttributes(stats.Speed);
        //Life bar
        transform.GetComponentInChildren<LifeBar>().SetLifeBar(Life);
    }

    void EnemyOpacity()
    {
        if (Dimension != GameObject.Find("GameController").GetComponent<GameController>().Dimension)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Bullet") && collision.GetComponent<Bullet>().Dimension == Dimension 
            && collision.GetComponent<Bullet>().PlayerBullet)
        {
            collision.GetComponent<Bullet>().Destroy();
            Life -= collision.GetComponent<Bullet>().BulletDamage;

            //Modifica la barra de vida del enemigo.
            transform.GetComponentInChildren<LifeBar>().ChangeLifeBar(Life);

            if (Life <= 0)
            {
                Loot(Random.Range(0, 12));
                if (FinalRound)
                    GameObject.Find("GameController").GetComponent<GameController>().EnemiesFinalRoundDec();
                GameObject.Find("GameController").GetComponent<GameController>().EnemiesKilledInc();
                
                Destroy(gameObject);
            }
        }
    }

    void Loot(int random)
    {
        switch (random)
        {
            case int n when (n <= 2): // Armas
                Instantiate(weapons[n], transform.position, transform.rotation);
                break;
            case int n when (n >= 3 && n <= 5): // Vida
                Instantiate(potion, transform.position, transform.rotation);
                break;
        }
    }
}

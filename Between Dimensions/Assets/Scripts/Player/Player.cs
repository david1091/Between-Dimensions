using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Añadido
    //Añadido Iván
    private AudioSource audioPlayer;

    [SerializeField]
    GameObject menuDied;

    [SerializeField]
    GameObject menuWin;
    
    [SerializeField]
    Text txt;

    [SerializeField]
    private MusicManager musicManager;

    [SerializeField]
    private AudioClip deathClip;

    //Stats
    public float Life { get; private set; }
    public float Speed { get; private set; }

    [SerializeField]
    private HealthGUI healthGUI;

    void Start()
    {
        Life = 10;
        Speed = 8.0f;

        //Añadido
        audioPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(GameObject.Find("GameController").GetComponent<GameController>().EnemiesKilled >= 35)
        {
            menuWin.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            GameObject.Find("GameController").GetComponent<GameController>().SetAlive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Bullet") && !collision.GetComponent<Bullet>().PlayerBullet)
        {
            //Añadido
            audioPlayer.Play();

            collision.GetComponent<Bullet>().Destroy();
            Life -= collision.GetComponent<Bullet>().BulletDamage;
            Life = Mathf.Clamp(Life, 0.0f, 10.0f);
            healthGUI.SetHealthGUI(Life);
            if (Life == 0)
            {
                Time.timeScale = 0;
                musicManager.changeLoop(false);
                musicManager.changeMusic(deathClip);
                menuDied.SetActive(true);
                Cursor.visible = true;
                GameObject.Find("GameController").GetComponent<GameController>().SetAlive(false);
                txt.text = "Enemy's defeated: "+ GameObject.Find("GameController").GetComponent<GameController>().EnemiesKilled;
            }
        } else if (collision.name.Contains("Potion"))
        {
            if(Life < 10)
            {
                Life += 2;
                Life = Mathf.Clamp(Life, 0.0f, 10.0f);
                healthGUI.SetHealthGUI(Life);
                Destroy(collision.gameObject);
            }
        }
    }
}

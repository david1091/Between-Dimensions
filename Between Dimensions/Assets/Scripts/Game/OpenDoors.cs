using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    [SerializeField]
    private bool doorBoss;

    public Collider2D door1;
    public Collider2D door2;

    public SpriteRenderer spriteOpenDoor1;
    public SpriteRenderer spriteOpenDoor2;

    public SpriteRenderer spriteCloseDoor1;
    public SpriteRenderer spriteCloseDoor2;

    private bool check = false;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        
    }

    void Update()
    {
        checkDoors();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//comprueba que se activa cuando el jugador entra
        {
            if (doorBoss == false)
            {
                if (check == false)
                {
                    check = true;

                    //cambia los sprites de las puertas
                    spriteCloseDoor1.enabled = false;
                    spriteCloseDoor2.enabled = false;

                    spriteOpenDoor1.enabled = true;
                    spriteOpenDoor2.enabled = true;
                }
            }
            else
            {
                if (GameObject.Find("GameController").GetComponent<GameController>().EnemiesKilled >= 15)
                {
                    Debug.Log("Has conseguido matar a toda la planta, ahora a por el final.");

                    check = true;
                    //cambia los sprites de las puertas
                    spriteCloseDoor1.enabled = false;
                    spriteCloseDoor2.enabled = false;

                    spriteOpenDoor1.enabled = true;
                    spriteOpenDoor2.enabled = true;
                }
                else
                {
                    Debug.Log("Todavia no has matado a todos.");
                }
            }
            
        }

    }
    private void checkDoors()
    {
        if(check == false)
        {
            door1.isTrigger = false;
            door2.isTrigger = false;

        }else if(check == true)
        {
            door1.isTrigger = true;
            door2.isTrigger = true;
        }
    }
}

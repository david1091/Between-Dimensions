using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    //src: https://unity3d.com/es/learn/tutorials/topics/2d-game-creation/2d-character-controllers
    private Rigidbody2D rb2d;
    private bool rotate = true;
    private Vector2 move;

    //Dash
    private bool dashing = false;
    private bool coolDoownDash = false;

    //Animation
    private Animator anim;

    //Change dimension/weapon
    private Weapon[] weapons;
    private GameObject[] maps;
    private int currentDimension;
    private GameObject currentWeapon;
    private GameObject currentMap;
    private Transform weaponParent;
    private bool changeWeapon = false;
    private GameObject newWeapon = null;


    //UI
    [SerializeField]
    private GameObject panelPause;
    [SerializeField]
    private GunGUI weaponUI;


    void Start()
    {
        Cursor.visible = false;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Establecer la dimensión y 
        currentDimension = 0;
        weapons = gameObject.GetComponentsInChildren<Weapon>();
        maps = new GameObject[2];
        maps[0] = GameObject.Find("Mapa1");
        maps[1] = GameObject.Find("Mapa2");
        currentWeapon = weapons[currentDimension].gameObject;
        currentMap = maps[currentDimension];
        weaponParent = transform.Find("Weapon");
        weapons[1].gameObject.SetActive(false);
        maps[1].SetActive(false);
    }

    void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Dash") && !dashing)
        {
            Dash();
        }
        //Animation
        anim.SetFloat("Speed", move.magnitude);
        PlayerRotate();
        Shoot();
        ChangeWeapon(newWeapon);
        ChangeDimension();
        PauseControl();
    }

    void FixedUpdate() //Physics code
    {
        if (!dashing)
        {
            rb2d.velocity = move.normalized * GetComponent<Player>().Speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Weapon"))
        {
            newWeapon = collision.gameObject;
            changeWeapon = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Weapon"))
        {
            newWeapon = null;
            changeWeapon = false;
        }
    }

    void Dash()
    {
        rb2d.velocity = move.normalized * 16.0f;
        dashing = true;
        coolDoownDash = true;
        Invoke("Dashing", 0.2f);
        Invoke("DashCooldown", 1.0f);
    }

    void PlayerRotate() //Método que rota el personaje
    {
        Vector3 mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position); //Obtiene la posición del ratón.

        if (mousePos.x < 0 && rotate) //Si la posición del ratón está a la izq. y no está rotado el gameObject(Player), lo rota.
        {
            //https://docs.unity3d.com/ScriptReference/Quaternion.html
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rotate = !rotate;
        }
        else if (mousePos.x >= 0 && !rotate)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rotate = !rotate;
        }
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire"))
        {
            currentWeapon.GetComponent<Weapon>().Shoot();
        } else if (Input.GetButtonUp("Fire"))
        {
            currentWeapon.GetComponent<Weapon>().StopShooting();
        }
    }

    void ChangeWeapon(GameObject weapon)
    {
        if (Input.GetButtonDown("ChangeWeapon") && changeWeapon
            && currentDimension == newWeapon.GetComponent<Weapon>().Dimension)
        {
            currentWeapon.GetComponent<Weapon>().StopShooting();
            currentWeapon.transform.GetComponentInChildren<BulletPooling>().enabled = false;
            currentWeapon.transform.parent = null;
            currentWeapon = weapon;
            currentWeapon.transform.SetParent(weaponParent, false);
            currentWeapon.transform.localPosition = new Vector3(0, 0, 0);
            currentWeapon.transform.GetComponentInChildren<BulletPooling>().enabled = true;
            weapons[currentDimension] = currentWeapon.GetComponent<Weapon>();
            weaponUI.SetWeaponGUI(currentWeapon.GetComponent<SpriteRenderer>().sprite);
        }
    }

    void ChangeDimension()
    {
        if (Input.GetButtonDown("ChangeDimension"))
        {
            if(currentDimension == 0)
            {
                currentDimension = 1;
            } else if (currentDimension == 1)
            {
                currentDimension = 0;
            }
            currentWeapon.GetComponent<Weapon>().StopShooting();
            currentWeapon.SetActive(false);
            currentWeapon = weapons[currentDimension].gameObject;
            currentWeapon.SetActive(true);
            currentMap.SetActive(false);
            currentMap = maps[currentDimension];
            currentMap.SetActive(true);
            weaponUI.GetComponent<GunGUI>().SetWeaponGUI(currentWeapon.GetComponent<SpriteRenderer>().sprite);
            GameObject.Find("GameController").GetComponent<GameController>().SetDimension(currentDimension);
        }
    }

    void Dashing()
    {
        dashing = false;
    }

    void DashCooldown()
    {
        coolDoownDash = false;
    }

    public void PauseControl()
    {
        if (Input.GetButtonDown("Pause"))//invoca el pause en caso de pulsar esc
        {
            if (GameObject.Find("GameController").GetComponent<GameController>().Alive)
            {
                if (Time.timeScale == 1)
                {
                    Cursor.visible = true;
                    panelPause.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    Cursor.visible = false;
                    Time.timeScale = 1;
                    panelPause.SetActive(false);
                }
            }
        }
    }
}

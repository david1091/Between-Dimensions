using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //IA
    private float speed;
    private readonly float vision = 14.0f;
    private readonly float radAttack = 4.0f;
    private bool isShooting = false;
    private Vector3 objective;
    private Vector3 target;
    private GameObject player;
    private Vector3 posIni;

    //Animation
    private Animator anim;
    private Vector3 lastPosition;

    private bool rotate = true;


    void Start()
    {
        lastPosition = transform.position;
        anim = GetComponent<Animator>();

        //Inicializar el objeto del jugador
        player = GameObject.FindGameObjectWithTag("Player");
        //Marcar posición inicial del enemigo
        posIni = transform.position;
        Time.timeScale = 1;
    }

    public void SetAttributes(float speed)
    {
        this.speed = speed;
    }

    void Update()
    {
        EnemyRotation();        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);   //seguimiento normal
        Debug.DrawLine(transform.position, target, Color.green);
        Run(transform.position);
    }

    void FixedUpdate()
    {
        target = posIni;


        //objetivo de la visión y los disparos
        objective = posIni;

        //la visión de lo que tiene entre el jugador y el enemigo (muros u otros objetos)
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            player.transform.position - transform.position,
            vision,
            1 << LayerMask.NameToLayer("Raycast"));

        //distancia entre jugador y enemigo
        float dist = Vector3.Distance(player.transform.position, transform.position);

        //Comprobaciones de rango de visión y de objetos que impidan la visión 
        if (dist < vision)
        {
            if (hit.collider.tag.Equals("Player"))
            {
                if (!isShooting)
                {
                    InvokeRepeating("Shoot", 0, GetComponent<Enemy>().ShootingSpeed);
                    isShooting = true;
                }
                objective = player.transform.position;
                if (dist > radAttack)                            //Seguir si aún no está a rango de ataque
                {
                    target = player.transform.position;
                }
                else                                      //Parar si está a rango de ataque        
                {
                    target = transform.position;
                }
            }
            else if (hit.collider.tag.Equals("Pared"))
            {
                CancelInvoke("Shoot");
                isShooting = false;
            }
        }
        else
        {
            if (isShooting)
            {
                CancelInvoke("Shoot");
                isShooting = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, vision);
        Gizmos.DrawWireSphere(transform.position, radAttack);
    }

    void Run(Vector3 currentPosition)
    { //Comprueba se el enemio está en movimiento para ejcutar la animación de correr.
        if (currentPosition == lastPosition)
        {
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", true);
        }

        lastPosition = currentPosition;
    }

    void DirectionEnemy()
    {
        Vector2 direction = new Vector2(
            objective.x - transform.position.x,
            objective.y - transform.position.y);

        transform.up = direction;
    }

    void EnemyRotation()
    {
        float weaponRotation = GetComponentInChildren<Weapon>().WeaponPos;

        if (weaponRotation > 90.0f && weaponRotation < 270.0f && rotate)
        {
            //Debug.Log(gameObject.name + " " + transform.rotation);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Find("CanvLife").rotation = Quaternion.Euler(0, 0, 0);
            rotate = !rotate;
        }
        else if ((weaponRotation <= 90 || weaponRotation >= 270) && !rotate)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Find("CanvLife").rotation = Quaternion.Euler(0, 0, 0);
            rotate = !rotate;
        }
    }

    void Shoot()
    {
        GetComponentInChildren<Weapon>().ShootEnemy();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private SpriteRenderer sRender;
    private bool playerWeapon = true;
    private bool shoot = true;
    private float damage;
    private float bulletSpeed;
    private float fireRate;
    private float coolDownTime;

    //Añadido Iván
    private AudioSource audioWeapon;
    [SerializeField]
    private AudioClip audioShoot;

    public int Dimension { private set; get; }
    public float WeaponPos { private set; get; }




    void Start()
    {
        SetAttributes(new WeaponStats(gameObject.name));
        sRender = GetComponent<SpriteRenderer>();

        //Añadido
        audioWeapon = GetComponent<AudioSource>();
        audioWeapon.clip = audioShoot;
    }

    void SetAttributes(WeaponStats stats)
    {
        damage = stats.Damage;
        bulletSpeed = stats.BulletSpeed;
        fireRate = stats.FireRate;
        coolDownTime = stats.CoolDownTime;
        Dimension = stats.Dimension;
    }


    void Update()
    {
        if (transform.parent != null)
        {
            WeaponRotation();
            GetComponent<PolygonCollider2D>().enabled = false;
            if (!transform.parent.parent.name.Equals("Player"))
                playerWeapon = false;
        } else
        {
            sRender.sortingOrder = 0;
            GetComponent<PolygonCollider2D>().enabled = true;
        }
        //Debug.Log();
    }

    void WeaponRotation()
    {
        Vector3 pos;
        Vector3 dir;
        if (!playerWeapon)
        {
            pos = gameObject.transform.position;
            dir = GameObject.FindGameObjectWithTag("Player").transform.position - pos;
            
        } else { 
            pos = Camera.main.WorldToScreenPoint(transform.position);
            dir = Input.mousePosition - pos;
        }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        WeaponPos = transform.rotation.eulerAngles.z;

        if (dir.x < 0)
        {
            transform.Rotate(-180f, 0, 0);
        }

        if(Mathf.Atan2(dir.y, dir.x) > 0)
        {
            sRender.sortingOrder = 0;
        } else
        {
            sRender.sortingOrder = 2;
        }
    }

    void Fire()
    {
        GameObject bullet = transform.GetComponentInChildren<BulletPooling>().GetBullets();
        if (bullet == null) return;
        Transform bulletDict = gameObject.GetComponentInChildren<BulletPooling>().transform;
        bullet.transform.position = bulletDict.transform.position;
        bullet.transform.rotation = bulletDict.transform.rotation;
        bullet.GetComponent<Bullet>().SetAttributes(bulletSpeed, damage, Dimension, playerWeapon);
        bullet.SetActive(true);

        //Añadido 
        audioWeapon.Play();
    }

    public void Shoot()
    {
        if (gameObject.name.Split('_')[0].Equals("Pistol"))
        {
            if (shoot)
            {
                Fire();
                Invoke("CoolDownShoot", coolDownTime);
                shoot = false;
            }
        } else
        {
            InvokeRepeating("Fire", 0.1f, fireRate);
        }
    }

    void CoolDownShoot()
    {
        shoot = true;
        CancelInvoke("CoolDownShoot");
    }

    public void StopShooting()
    {
        CancelInvoke("Fire");
    }

    public void ShootEnemy()
    {
        Fire();
    }
    public void SetEnemyDamage(float damage)
    {
        this.damage = damage;
    }
}

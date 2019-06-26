using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed;
    public float BulletDamage { private set; get; }
    public float Dimension { private set; get; }
    public bool PlayerBullet { private set; get; }

    void OnEnable()
    {
        //Invoke("Destroy", 2f);
        //transform.Rotate(0, 0, -90f);
        //GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        //Debug.Log(PlayerBullet);
    }

    public void SetAttributes(float bulletSpeed, float bulletDamage, float dimension, bool playerBullet)
    {
        this.bulletSpeed = bulletSpeed;
        BulletDamage = bulletDamage;
        Dimension = dimension;
        PlayerBullet = playerBullet;
    }

    void Update()
    {
        transform.Translate(bulletSpeed * Time.deltaTime, 0, 0);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.tag.Equals("Player") || collision.tag.Equals("Weapon") || collision.tag.Equals("Bullet") || 
            collision.tag.Equals("Enemy") || collision.tag.Equals("Potion") || collision.tag.Equals("Warp")))
            Destroy();
    }
}

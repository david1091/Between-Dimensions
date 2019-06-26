using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    private List<GameObject> bullets;
    private readonly int bulletAmount = 10;

    void Awake()
    {
        if (transform.parent.parent != null)
            this.enabled = true;
        else
            this.enabled = false;
    }

    void OnEnable()
    {
        bullets = new List<GameObject>();
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);
            bullets.Add(obj);
        }
    }

    void OnDisable()
    {
        if(bullets != null)
            for (int i = 0; i < bullets.Count; i++)
            {
                Destroy(bullets[i]);
            }
    }

    public GameObject GetBullets()
    {
        for (int i = 0; i < bullets.Count; i++) {
            if (bullets[i] != null) { 
                if (!bullets[i].activeInHierarchy)
                    return bullets[i];
                }
            }
        GameObject obj = (GameObject)Instantiate(bullet);
        obj.SetActive(false);
        bullets.Add(obj);
        return obj;
    }
}

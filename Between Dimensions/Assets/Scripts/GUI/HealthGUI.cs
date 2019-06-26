using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGUI : MonoBehaviour
{
    private Image image;
    private Sprite[] imgHealth;

    void Start()
    {
        image = GetComponent<Image>();
        imgHealth = new Sprite[11];
        imagesArray();
    }
    void imagesArray() {

        imgHealth[0] = Resources.Load<Sprite>("Sprites/Health/5");
        imgHealth[1] = Resources.Load<Sprite>("Sprites/Health/4_5");
        imgHealth[2] = Resources.Load<Sprite>("Sprites/Health/4");
        imgHealth[3] = Resources.Load<Sprite>("Sprites/Health/3_5");
        imgHealth[4] = Resources.Load<Sprite>("Sprites/Health/3");
        imgHealth[5] = Resources.Load<Sprite>("Sprites/Health/2_5");
        imgHealth[6] = Resources.Load<Sprite>("Sprites/Health/2");
        imgHealth[7] = Resources.Load<Sprite>("Sprites/Health/1_5");
        imgHealth[8] = Resources.Load<Sprite>("Sprites/Health/1");
        imgHealth[9] = Resources.Load<Sprite>("Sprites/Health/0_5");
        imgHealth[10] = Resources.Load<Sprite>("Sprites/Health/0");
    }

    public void SetHealthGUI(float health)
    {

        switch (health)
        {
            case 10:
                image.sprite = imgHealth[0];
                break;
            case 9:
                image.sprite = imgHealth[1];
                break;
            case 8:
                image.sprite = imgHealth[2];
                break;
            case 7:
                image.sprite = imgHealth[3];
                break;
            case 6:
                image.sprite = imgHealth[4];
                break;
            case 5:
                image.sprite = imgHealth[5];
                break;
            case 4:
                image.sprite = imgHealth[6];
                break;
            case 3:
                image.sprite = imgHealth[7];
                break;
            case 2:
                image.sprite = imgHealth[8];
                break;
            case 1:
                image.sprite = imgHealth[9];
                break;
            case 0:
                image.sprite = imgHealth[10];
                break;
        }
    }
}


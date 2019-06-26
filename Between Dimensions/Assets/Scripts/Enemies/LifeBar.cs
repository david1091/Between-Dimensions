using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    private Image bar;
    private Text barText;
    private float initLife;

    public void SetLifeBar(float life)
    {
        bar = transform.Find("LifeBackGround").Find("LifeBar").GetComponent<Image>();
        barText = transform.Find("Text").GetComponent<Text>();
        initLife = life;
        bar.fillAmount = 1;
        barText.text = life.ToString() + "/" + initLife.ToString();
    }

    public void ChangeLifeBar(float Life)
    {
        //Modificación canvas barra de vida
        bar.fillAmount = Life / initLife;
        //Modificación números barra de vida
        barText.text = Life.ToString() + "/" + initLife.ToString();
    }
}

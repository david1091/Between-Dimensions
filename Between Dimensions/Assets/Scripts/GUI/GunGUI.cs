using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunGUI : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void SetWeaponGUI(Sprite weapon)
    {
        image.sprite = weapon;
    }
}

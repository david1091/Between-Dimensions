using System;
using System.Collections;
using System.Collections.Generic;

public class WeaponStats
{
    private readonly string weaponName;
    public float Damage { get; private set; }
    public float BulletSpeed { get; private set; }
    public float FireRate { get; private set; }
    public float CoolDownTime { get; private set; }
    public int Dimension { get; private set; }

    public WeaponStats (string weaponName)
    {
        this.weaponName = weaponName.Split('_')[0];
        Dimension = Int32.Parse(weaponName.Split('_')[1]);
        SetAttributes();
    }

    void SetAttributes()
    {
        switch (weaponName)
        {
            case ("Pistol"):
                Damage = 3;
                BulletSpeed = 12.0f;
                FireRate = 0.0f;
                CoolDownTime = 0.30f;
                break;
            case ("AR"):
                Damage = 1;
                BulletSpeed = 18.0f;
                FireRate = 0.15f;
                CoolDownTime = 0f;
                break;
            case ("Shotgun"):
                Damage = 5;
                BulletSpeed = 20.0f;
                FireRate = 0f;
                CoolDownTime = 0.5f;
                break;
            case ("LMG"):
                Damage = 2;
                BulletSpeed = 15.0f;
                FireRate = 0.20f;
                CoolDownTime = 0f;
                break;
            case ("BlasterAT"):
                Damage = 2;
                BulletSpeed = 15.0f;
                FireRate = 0.20f;
                CoolDownTime = 0f;
                break;
        }
    }
}

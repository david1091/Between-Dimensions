using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotarFondo : MonoBehaviour
{
    [SerializeField]
    RectTransform back;
     
    static float i = 0;
    float ini;

    // Start is called before the first frame update
    void Start()
    {
        ini = 1;
    }

    void Update()
    {
        ini = ini - Time.deltaTime * 100;

        if (ini <= 0)
        {
            i++;
            back.GetComponent<RectTransform>().rotation =  Quaternion.Euler(0, 0, i);
            ini = 1;
        }
    }
}

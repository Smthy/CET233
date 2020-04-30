using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class brain : MonoBehaviour
{
    public bool isInfected;

    public int itemsTotal;
    public int itemsOn;
    public int itemsOn2;
    public int itemsOn3;
    public int itemsOn4;

    public GameObject sensor;
    public GameObject sensor2;
    public GameObject sensor3;
    public GameObject sensor4;

    public Text infected;
    // Start is called before the first frame update
    void Start()
    {
        isInfected = true;
        itemsTotal = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        count();

       if (itemsTotal == 4)
        {
            Debug.Log("NO LONGER INFECTED");
            isInfected = false;
            infected.text = "Not Infected 4/4";
            infected.color = Color.green;
        }
    }

    private void count()
    {
        itemsOn = sensor.GetComponent<Sensor>().itemsOn;
        itemsOn2 = sensor2.GetComponent<Sensor>().itemsOn;
        itemsOn3 = sensor3.GetComponent<Sensor>().itemsOn;
        itemsOn4 = sensor4.GetComponent<Sensor>().itemsOn;

        itemsTotal = itemsOn + itemsOn2 + itemsOn3 + itemsOn4;

        switch (itemsTotal)
        {
            case 1:
                infected.text = "Infected 1/4";
                break;
            case 2:
                infected.text = "Infected 2/4";
                break;
            case 3:
                infected.text = "Infected 3/4";
                break;

        }


    }
}

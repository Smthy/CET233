using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public Light spot;
    public int itemsOn;
    private bool hasItem = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("item") && hasItem == false)
        {
            Debug.Log("IN SENSOR");
            spot.color = Color.green;
            itemsOn += 1;
            hasItem = true;
        }
    }

    private void OnTriggerExit()
    {
        Debug.Log("Out of Sensor");
        spot.color = Color.red;
        hasItem = false;
        itemsOn -= 1;
    }
}

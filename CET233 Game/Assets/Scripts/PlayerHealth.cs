using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float pHealth = 100f;
    public GameObject player, pCamera;

    void Start()
    {

    }

    void Update()
    {
        if (pHealth >= 100)
        {
            pHealth = 100f;
        }

        if (pHealth <= 0)
        {
            pHealth = 0;
        }

        Debug.Log(pHealth);
    }

    public void PlayerTakeDamage(float amount)
    {
        pHealth -= amount;

        if (pHealth <= 0f)
        {
            Destroy(this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealthSupplies"))
        {
            if (pHealth >= 100f)
            {
                pHealth = 100f;
            }
            else
            {
                Destroy(other.gameObject);
                pHealth += 25f;
            }
            
            Destroy(other.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float impactDamage = 75f;
    public GameObject player;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth playerH = player.GetComponent<PlayerHealth>();
            if (playerH != null)
            {
                playerH.PlayerTakeDamage(impactDamage);
            }
        }

    }
}

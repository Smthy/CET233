using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupAble : MonoBehaviour
{
    public Text PickUpText;
    private bool canPickUp = false;
    public bool isPickedUp = false;

    void Start()
    {
        PickUpText.enabled = false;
    }


    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            isPickedUp = true;
            PickUpText.enabled = false;
            Destroy(gameObject);
            Debug.Log("Keycard Picked Up");
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpText.enabled = true;
            Debug.Log("Player in range");
            canPickUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickUpText.enabled = false;
        Debug.Log("Player out of range range");
        canPickUp = false;
    }
}

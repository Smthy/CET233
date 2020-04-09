using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedDoor : MonoBehaviour
{
    public Text PressButtonText;
    public Text doorLockedText;
    private bool canOpenDoor = false;

    public GameObject keycard;
    public PickupAble KeyCardScript;
    private bool isPickedUp = false;
    void Start()
    {
        doorLockedText.enabled = false;
        PressButtonText.enabled = false;
        KeyCardScript = keycard.GetComponent<PickupAble>();
    }


    void Update()
    {
        isPickedUp = KeyCardScript.isPickedUp;

        if (isPickedUp == true && canOpenDoor == true &&Input.GetKeyDown(KeyCode.E))
        {
            PressButtonText.enabled = false;
            Destroy(gameObject);
            Debug.Log("Door Open");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPickedUp == true && other.CompareTag("Player"))
        {
            PressButtonText.enabled = true;
            Debug.Log("Player in range");
            canOpenDoor = true;
        }
        else
        { 
            Debug.Log("Door is Locked");
            doorLockedText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PressButtonText.enabled = false;
        Debug.Log("Player out of range range");
        canOpenDoor = false;
        doorLockedText.enabled = false;
    }
}

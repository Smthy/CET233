using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockedDoor : MonoBehaviour
{
    public Text PressButtonText;
    private bool canOpenDoor;

    void Start()
    {
        PressButtonText.enabled = false;
    }


    void Update()
    {
        if (canOpenDoor == true &&  Input.GetKeyDown(KeyCode.E))
        {
            PressButtonText.enabled = false;
            Destroy(gameObject);
            Debug.Log("Door Open");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PressButtonText.enabled = true;
            Debug.Log("Player in range");
            canOpenDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PressButtonText.enabled = false;
        Debug.Log("Player out of range range");
        canOpenDoor = false;
    }
}

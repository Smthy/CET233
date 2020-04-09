using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorways : MonoBehaviour
{
    public MeshCollider doorCol;
    public MeshRenderer doorRen;

        
    void Start()
    {
        doorCol.enabled = true;
        doorRen.enabled = true;
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Alien"))
        {
            doorCol.enabled = false;
            doorRen.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Alien"))
        {
            doorCol.enabled = true;
            doorRen.enabled = true;
        }
    }
}


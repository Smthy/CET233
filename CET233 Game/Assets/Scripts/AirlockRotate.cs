using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirlockRotate : MonoBehaviour
{
    public int rotationDirection;
    public int rotationStep;
    public GameObject airlock;    

    private Vector3 currentRotation, targetRotation;

    
    public void rotateObject()
    {
        currentRotation = gameObject.transform.eulerAngles;
        targetRotation.y = (currentRotation.y + (90 * rotationDirection));
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {       
        currentRotation.y += (rotationStep * rotationDirection);
        gameObject.transform.eulerAngles = currentRotation;        
        yield return new WaitForSeconds(0);

        if(currentRotation.y > targetRotation.y)
        {
            StartCoroutine(Rotate());
        }       
    }     
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour
{
    public Light spotlight;
    private bool canSwitch = true;
    
    void Start()
    {
        StartCoroutine(randomFlashing()); 
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.F) && canSwitch == true)
        {
            if(spotlight.enabled)
            {
                spotlight.enabled = false;
            }
            else
            {
                spotlight.enabled = true;
            }
        }
    }


    IEnumerator randomFlashing()
    {
        yield return new WaitForSeconds(150f);
        int i = 0;
        i = Random.Range(0, 8);
        Debug.Log(i);
        
        if(i == 2)
        {
            if (spotlight.enabled == true)
            {
                canSwitch = false;
                spotlight.enabled = false;
                yield return new WaitForSeconds(0.15f);
                spotlight.enabled = true;
                yield return new WaitForSeconds(0.15f);
                spotlight.enabled = false;
                yield return new WaitForSeconds(0.25f);
                spotlight.enabled = true;
                yield return new WaitForSeconds(0.15f);
                spotlight.enabled = false;
                yield return new WaitForSeconds(0.25f);
                spotlight.enabled = true;
                yield return new WaitForSeconds(0.15f);
                spotlight.enabled = false;
                yield return new WaitForSeconds(150f);
                canSwitch = true;
                StartCoroutine(randomFlashing());
            }
            else
            {
                yield return new WaitForSeconds(150f);
                StartCoroutine(randomFlashing());
            }
        }
        else
        {
            yield return new WaitForSeconds(150f);
            StartCoroutine(randomFlashing());
        }
    }
}

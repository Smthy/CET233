using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    //Movement
    public float speed = 8f;
    public float gravity = -9.81f;

    //Gravity
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;  
    private Vector3 vel;
    private bool isGrounded;

    //Jump
    public float jumpHeight = 3f;

    //Crouch
    private Vector3 crouchScale = new Vector3(1.1f, 0.5f, 1.1f);
    private Vector3 playerScale;
    bool crouching;

    
    private bool isAtLadder = false; //Joe- paired with trigger collision to reverse gravity and allow player to rise out of vent
    public bool playerIsInfected = true;
    private bool playerInsideScanner;
    public GameObject ScannerText;
    public Light airlockLight;
    public GameObject airlock;
    public AirlockRotate airlockScript;


    private void Start()
    {
        playerScale = transform.localScale;
        crouching = false;
    }

    void Update()
    {
        Movement();
        Gravity();
        Crouch();
        Sprint();
        //Jump();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    /*
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            vel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    */

    void Gravity() //joe- added an if statment that determins if the player is at the ladder
    {
        if (isAtLadder == false)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && vel.y < 0)
            {
                vel.y = -2f;
            }

            vel.y += gravity * Time.deltaTime;
            controller.Move(vel * Time.deltaTime);
        }
        else // joe- if they are at a ladder and hold spcae, reverse gravity to simulate climbing
        {        
            vel.y = 4f;           
            vel.y += gravity * Time.deltaTime;
            controller.Move(vel * Time.deltaTime);
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching = true;
            transform.localScale = crouchScale;
            speed = 4f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouching = false;
            transform.localScale = playerScale;
            speed = 8f;
        }
    }

    void Sprint()
    {
        if(crouching == false && Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 12;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 8;
        }
    }



    //joe-trigger collision check while are at ladder and holding space
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Vent"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && other.CompareTag("Vent"))
            {
                isAtLadder = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isAtLadder = false;
            }
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Scanner")) // turn on 3d text when player enters scanner room
        { //determin if player is infected or not
            Debug.Log("PlayerEnterScanner");
            ScannerText.GetComponent<TextMesh>().color = new Color32(255, 170, 0, 225);
            ScannerText.GetComponent<TextMesh>().text = "Scanning occupant";
            ScannerText.SetActive(true);
            playerInsideScanner = true;
            StartCoroutine(scanPlayer());
        }
    }

    private void OnTriggerExit(Collider other)//joe- when they leave collision, resume gravity
    {
        if(other.CompareTag("Vent"))
        {
            isAtLadder = false;
        }


        if(other.CompareTag("Scanner")) //turn off 3d text when player leaves scanner room
        {
            Debug.Log("playerLeftScanner");
            playerInsideScanner = false;
            ScannerText.SetActive(false);
            airlockLight.color = Color.white;
        }        
    }


    IEnumerator scanPlayer()
    {
        yield return new WaitForSeconds(2f);


        if (playerIsInfected == true) //script if player is infected
        {
            airlockLight.color = Color.red;
            if (playerInsideScanner == true)
            {                
                ScannerText.GetComponent<TextMesh>().color = Color.red;
                ScannerText.GetComponent<TextMesh>().text = "Contamination detected";
            }


            if (playerInsideScanner == true)
            {
                yield return new WaitForSeconds(1.5f);
                ScannerText.GetComponent<TextMesh>().text = " Inhabitant";
            }


            if (playerInsideScanner == true)
            {
                yield return new WaitForSeconds(1.5f);
                ScannerText.GetComponent<TextMesh>().text = "please make your way to...";
            }


            if (playerInsideScanner == true)
            {
                yield return new WaitForSeconds(1.5f);
                ScannerText.GetComponent<TextMesh>().text = "the Isolation Center.";
            }

            if (playerInsideScanner == true)
            {
                yield return new WaitForSeconds(1.5f);
                ScannerText.GetComponent<TextMesh>().text = "Currently located in...";
            }

            if (playerInsideScanner == true)
            {
                yield return new WaitForSeconds(1.5f);
                ScannerText.GetComponent<TextMesh>().text = " the Cafeteria.";
            }

            if (playerInsideScanner == true)
            {
                yield return new WaitForSeconds(1.5f);
                ScannerText.GetComponent<TextMesh>().text = "Exit Scanning area";
            }
        }
        


        if(playerIsInfected == false) //script if player is cured
        {
            airlockLight.color = Color.green;
            airlock.GetComponent<Collider>().enabled = !airlock.GetComponent<Collider>().enabled;
            ScannerText.GetComponent<TextMesh>().color = Color.green;
            ScannerText.GetComponent<TextMesh>().text = "Inhabitant is not contaminated.";           
            yield return new WaitForSeconds(1.5f);
            ScannerText.GetComponent<TextMesh>().text = "Opening airlock valve...";           
            yield return new WaitForSeconds(1.5f);
            ScannerText.GetComponent<TextMesh>().text = "please follow the pathway...";        
            yield return new WaitForSeconds(1.5f);
            ScannerText.GetComponent<TextMesh>().text = "to the Escape Shuttle.";
            airlockScript.rotateObject();
            
        }
    }


}
